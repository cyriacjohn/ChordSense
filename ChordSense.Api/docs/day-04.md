# Day 4 — .NET ↔ Flask Microservice Integration

## 🎯 Objective

Enable the ASP.NET Web API to communicate with the Python Flask AI service in order to forward lyric analysis requests and return AI-generated responses to the client.

This transitions the system into a true **API Gateway + AI Microservice architecture**.

---

## 🧱 Achievements (Day 4)

✔ Added `HttpClientFactory` in .NET  
✔ Created distributed RPC from C# → Python  
✔ Forwarded JSON payloads to Flask AI service  
✔ Received JSON back from Flask into .NET  
✔ Returned final response to the client (Postman)  
✔ Verified full pipeline with manual testing  
✔ Updated request DTO for lyrics  
✔ Cleaned route structure and fixed token misuse  
✔ Resolved Python syntax issues  
✔ Documented data flow and integration behavior

---

## 🧩 Technical Breakdown

### **1. API Gateway Role (ASP.NET)**

- Validates incoming requests
- Serializes JSON payload
- Forwards request to Flask AI
- Waits for response asynchronously
- Returns normalized JSON to client

Controller snippet:

```csharp
var payload = new { text = request.Lyrics };
var response = await _http.PostAsync(flaskUrl, content);
var result = await response.Content.ReadAsStringAsync();

---

### Data-Flow

Client (Postman)
     ↓
ASP.NET API (5294)
     ↓  JSON POST
Flask AI Service (5001)
     ↓  JSON RESPONSE
ASP.NET Normalizes
     ↓
Client Receives Final JSON