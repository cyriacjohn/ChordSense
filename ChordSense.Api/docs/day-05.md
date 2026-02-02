## 🟢 DAY 5 — Flask AI Service Foundation

### 🎯 Objective
Create a standalone **Python AI microservice** that:
- Runs independently from the ASP.NET Core API
- Exposes REST endpoints for AI computation
- Acts purely as a computation layer (no orchestration)

---

### 🧱 Architecture Introduced
Client (Postman / Frontend)
↓
ASP.NET Core API (Port 5294)
↓
Python Flask AI Service (Port 5001)

---

### 🧠 Concepts Learned

- Microservices separation (API vs AI)
- Flask application lifecycle
- RESTful POST endpoints
- JSON vs multipart/form-data handling
- Why Python is preferred for AI workloads

---

### ✅ Outcome
- Flask service running on port `5001`
- Endpoints verified via Postman
- Placeholder responses (`status: ok`)
- Clear separation between orchestration and computation

---
