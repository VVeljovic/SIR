from fastapi import FastAPI
import requests
import pickle
import subprocess
import time
from collections import deque

RABBITMQ_URL = "http://localhost:15672"
RABBITMQ_USER = "user"
RABBITMQ_PASS = "password"

CONSUMER_THROUGHPUT_PER_REPLICA = 113 
WINDOW_SIZE = 12  

app = FastAPI(title="ML Autoscaler Predictor")

with open("model_v2.pkl", "rb") as f:
    bundle = pickle.load(f)
    model = bundle["model"]
    scaler = bundle["scaler"]

_history = deque(maxlen=WINDOW_SIZE)


def get_queue_depth_value():
    r = requests.get(
        f"{RABBITMQ_URL}/api/queues/%2F/sensor-data",
        auth=(RABBITMQ_USER, RABBITMQ_PASS)
    )
    return r.json()["messages"]


def get_current_consumers():
    result = subprocess.run(
        ["kubectl", "get", "deployment", "consumer-deployment",
         "-o", "jsonpath={.spec.replicas}"],
        capture_output=True, text=True
    )
    try:
        return int(result.stdout.strip())
    except:
        return 0


@app.get("/health")
def health():
    return {"status": "running"}


@app.get("/queue-depth")
def get_queue_depth():
    return {"queue_depth": get_queue_depth_value()}


@app.get("/predict")
def predict():
    current_depth = get_queue_depth_value()
    now = time.time()
    _history.append((now, current_depth))

    if len(_history) < 2:
        net_rate = 0.0
    else:
        oldest_time, oldest_depth = _history[0]
        net_rate = (current_depth - oldest_depth) / (now - oldest_time)

    current_consumers = get_current_consumers()
    production_rate = max(0.0, net_rate + current_consumers * CONSUMER_THROUGHPUT_PER_REPLICA)

    x = scaler.transform([[production_rate]])
    raw = float(model.predict(x)[0])
    replicas = max(1, min(10, int(round(raw))))

    return {
        "recommended_replicas": replicas,
        "queue_depth": current_depth,
        "net_rate_per_sec": round(net_rate, 2),
        "estimated_production_rate": round(production_rate, 2),
        "current_consumers": current_consumers,
        "window_readings": len(_history)
    }


@app.post("/scale")
def scale():
    pred = predict()
    replicas = pred["recommended_replicas"]

    result = subprocess.run(
        ["kubectl", "scale", "deployment", "consumer-deployment", f"--replicas={replicas}"],
        capture_output=True, text=True
    )

    return {
        "scaled_to": replicas,
        "queue_depth": pred["queue_depth"],
        "estimated_production_rate": pred["estimated_production_rate"],
        "kubectl_output": result.stdout.strip() or result.stderr.strip()
    }
