import json
from operator import truediv
from flask import Flask, request, jsonify

app = Flask(__name__)

@app.post("/analyze/lyrics")
def analyze_lyrics():
    data = request.json or {}
    text = data.get("text", "")

    return jsonify({
        "status": "ok",
        "detected_language": "unknown",
        "sentiment": "neutral",
        "suggested_scale": "C major"       
        })

if __name__ == "__main__":
    print("Lyrics AI runing on port 5001")
    app.run(host="0.0.0.0",port=5001,debug=True)
