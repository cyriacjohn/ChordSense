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