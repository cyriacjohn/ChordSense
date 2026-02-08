from flask import Flask, request, jsonify
from services.lyrics_service import analyze_lyrics_logic
from services.audio_service import analyze_audio_logic

app = Flask(__name__)

@app.post("/analyze/lyrics")
def analyze_lyrics():
    return analyze_lyrics_logic()

@app.post("/analyze/audio")
def analyze_audio():
    return analyze_audio_logic()

if __name__ == "__main__":
    print("AI Service running on port 5001")
    app.run(host="0.0.0.0", port=5001, debug=False)