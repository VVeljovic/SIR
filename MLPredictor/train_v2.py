import pandas as pd
import numpy as np
from sklearn.linear_model import LinearRegression
from sklearn.preprocessing import StandardScaler
from sklearn.metrics import mean_absolute_error, r2_score
import pickle

df = pd.read_csv("queue_data_v2.csv")
print(f"Dataset: {len(df)} redova")
print(df.to_string(index=False))

X = df[["production_rate"]].values
y = df["min_consumers"].values

scaler = StandardScaler()
X_scaled = scaler.fit_transform(X)

model = LinearRegression()
model.fit(X_scaled, y)

y_pred = model.predict(X_scaled)
print(f"\nR2 score:  {r2_score(y, y_pred):.4f}")
print(f"MAE:       {mean_absolute_error(y, y_pred):.4f}")

for rate in [50, 100, 150, 200, 300, 400, 500]:
    x_in = scaler.transform([[rate]])
    pred = max(1, min(10, int(round(float(model.predict(x_in)[0])))))
    print(f"  rate={rate:5.0f} msg/s -> {pred} consumer(a)")

with open("model_v2.pkl", "wb") as f:
    pickle.dump({"model": model, "scaler": scaler}, f)

print("\nModel saved: model_v2.pkl")


