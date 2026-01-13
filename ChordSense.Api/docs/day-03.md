# DAY 3 — Flask AI Microservice + Multi-Service Architecture

## What I Built Today

-Created Python Flask microservice to simulate AI processing
-Implemented:
POST /analyze/lyrics
POST /analyze/audio
-Enabled file uploads for audio
-Ran AI service on new port 5001
-Successfully tested audio upload using Postman

## Key Technical Notes

-Flask service skeleton:

from flask import Flask, request, jsonify

app = Flask(__name__)

@app.post("/analyze/lyrics")
def analyze_lyrics():
    data = request.json
    return jsonify({"status": "ok"})

@app.post("/analyze/audio")
def analyze_audio():
    file = request.files.get("file")
    return jsonify({"status": "ok"})

if __name__ == "__main__":
    print("AI Service running on port 5001")
    app.run(host="0.0.0.0", port=5001, debug=True)

## Testing

-Via Postman:
-POST /analyze/lyrics with JSON → 200 OK
-POST /analyze/audio with form-data file → 200 OK

-Confirmed Flask console output:

-Running on http://127.0.0.1:5001

## Challenges Faced

-Flask routes required leading / (error: URL rule must start with a slash)
-Postman audio upload required form-data + file field
-Needed to enable Python in VS + PowerShell terminal

## What I Learned Today

-Microservice patterns (API gateway → AI worker)
-MIME file uploads (multipart/form-data)
-Cross-tool testing workflow (ASP.NET + Flask + Postman)
-Flask auto-reloading with debug=True

## Architecture Shift

- Today defined the future system architecture:
 Mobile / Web / Desktop clients
        ↓
ASP.NET Core (API Gateway)
        ↓
Python AI Workers (Lyric & Audio)

-This follows modern ML deployment design.

## Next Planned Steps

-Implement real ML inference logic (audio + lyrics)
-Establish HTTP communication between .NET → Flask
-Begin documentation + GitHub repo structure
-Install audio processing dependencies (librosa, pydub, torchaudio)