import requests
import csv
import time
import subprocess
from statistics import mean

RABBITMQ_URL = "http://localhost:15672"
RABBITMQ_USER = "user"
RABBITMQ_PASS = "password"

def get_queue_depth():
    try:
        r = requests.get(f"{RABBITMQ_URL}/api/queues/%2F/sensor-data", auth=(RABBITMQ_USER, RABBITMQ_PASS))
        return r.json().get("messages", 0)
    except:
        return 0

def set_producers(replicas, delay_ms):
    subprocess.run(["kubectl", "scale", "deployment", "producer-deployment", f"--replicas={replicas}"], capture_output=True)
    yaml = f"""apiVersion: v1
kind: ConfigMap
metadata:
  name: stream-config
data:
  appsettings.json: |
    {{
      "AppSettings" : {{
        "StreamDelay": {delay_ms}
      }}
    }}"""
    subprocess.run(["kubectl", "apply", "-f", "-"], input=yaml.encode(), capture_output=True)
    subprocess.run(["kubectl", "rollout", "restart", "deployment", "producer-deployment"], capture_output=True)
    time.sleep(10)

def set_consumers(replicas):
    subprocess.run(["kubectl", "scale", "deployment", "consumer-deployment", f"--replicas={replicas}"], capture_output=True)
    time.sleep(10)

def measure_rate(duration=30):
    d0 = get_queue_depth()
    time.sleep(duration)
    d1 = get_queue_depth()
    return (d1 - d0) / duration, mean([d0, d1])

scenarios = [
    (1,  10),
    (2,  10),
    (3,  10),
    (4,  10),
    (6,  10),
    (8,  10),
    (10, 10),
]

consumer_options = [1, 2, 3, 4, 5, 6, 8, 10]

print("Pocinjem realno prikupljanje podataka...")
set_consumers(0)

with open("queue_data_v2.csv", "w", newline="") as f:
    writer = csv.writer(f)
    writer.writerow(["production_rate", "queue_depth", "min_consumers"])

    for prod_replicas, delay in scenarios:
        print(f"\n>>> Scenario: {prod_replicas} producera, delay={delay}ms")

        set_producers(prod_replicas, delay)
        set_consumers(0)
        production_rate, base_depth = measure_rate(20)
        print(f"    Production rate: {production_rate:.1f} msg/s, depth: {base_depth:.0f}")

        min_consumers = 10
        found = False

        for n in consumer_options:
            print(f"    Testiranje {n} consumer(a)...", end=" ", flush=True)
            set_consumers(n)
            rate, depth = measure_rate(30)
            print(f"rate={rate:.1f} msg/s", flush=True)

            if rate <= 5:  
                min_consumers = n
                found = True
                print(f"    -> Stabilan pri {n} consumer(a)")
                break

        if not found:
            print(f"    -> Nije stabilizovano ni sa {consumer_options[-1]} consumer(a)")

        writer.writerow([round(production_rate, 2), round(base_depth, 0), min_consumers])
        f.flush()

# Cleanup
set_consumers(0)
set_producers(0, 100)
print("\nGotovo! Podaci upisani u queue_data_v2.csv")
