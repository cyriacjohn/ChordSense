## 🟡 DAY 7 — NLP Intelligence (Lyrics Analysis)

### 🎯 Objective
Transform audio uploads into musical intelligence:
- Tempo (BPM)
- Musical key
- Audio metadata

### 🧠 Libraries Used

| Library | Purpose |
|------|--------|
| `librosa` | Audio DSP |
| `numpy` | Numerical computation |
| `tempfile` | Safe temp file handling |
| `os` | File cleanup |

🧩 Audio DSP Pipeline
1. File Upload
file = request.files.get("file")

2. Temporary File Storage
with tempfile.NamedTemporaryFile(delete=False) as temp:


Required for librosa

Prevents memory overload

Enables safe cleanup

3. Audio Loading
y, sr = librosa.load(path, mono=True)

Variable	Meaning
y	Audio waveform
sr	Sample rate
4. Tempo Detection
tempo, _ = librosa.beat.beat_track(y=y, sr=sr)


Beat tracking algorithm

Returns BPM

5. Key Detection (Chroma)
chroma = librosa.feature.chroma_stft(y=y, sr=sr)
key = notes[np.argmax(np.mean(chroma, axis=1))]


Uses pitch-class energy

Maps dominant frequency to musical key

6. Cleanup & Error Handling
finally:
    os.remove(temp_path)


Prevents disk leaks

Production-safe resource management