from flask import Flask, request, jsonify
import librosa
import numpy as np
import tempfile
import os
import soundfile as sf

app = Flask(__name__)

def analyze_audio_logic():
    file = request.files.get("file")

    if file is None:
        return jsonify({"error": "No file received"}), 400

    temp_path = None

    try:
        # Save temporary file
        with tempfile.NamedTemporaryFile(delete=False, suffix=".wav") as temp:
            file.save(temp.name)
            temp_path = temp.name

        # Load audio
        y, sr = librosa.load(temp_path, sr=None,mono=True)

        # Tempo detection
        tempo, _ = librosa.beat.beat_track(y=y, sr=sr)

        # Chroma features for key detection
        chroma = librosa.feature.chroma_stft(y=y, sr=sr)
        chroma_mean = np.mean(chroma, axis=1)

        # Key mapping
        notes = ['C', 'C#', 'D', 'D#', 'E', 'F',
                 'F#', 'G', 'G#', 'A', 'A#', 'B']
        key = notes[int(np.argmax(chroma_mean))]

        return jsonify({
            "status": "ok",
            "tempo_bpm": round(float(tempo), 2),
            "key": key,
            "sample_rate": sr
        })

    except Exception as e:
        return jsonify({"error": str(e)}), 500

    finally:
        if temp_path and os.path.exists(temp_path):
            os.remove(temp_path)


if __name__ == "__main__":
    print("Audio AI running with DSP on port 5001")
    app.run(host="0.0.0.0", port=5001, debug=True)