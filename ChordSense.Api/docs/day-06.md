## 🟡 DAY 6 — NLP Intelligence (Lyrics Analysis)

### 🎯 Objective
Add **real NLP intelligence** to the AI service:
- Language detection
- Sentiment analysis
- Mood inference

---

### 🧠 Libraries Used

| Library | Purpose |
|------|--------|
| `langdetect` | Language detection |
| `vaderSentiment` | Sentiment analysis |
| `flask` | Request handling |

---

### 🧩 NLP Processing Flow

1. Receive JSON payload
2. Validate lyrics content
3. Detect language
4. Analyze sentiment polarity
5. Infer mood using keyword heuristics
6. Return structured JSON response

---

### NLP Concepts Learned

#### Language Detection
```python
lang = detect(text)

#### Sentiment Analysis
```python
scores = analyzer.polarity_scores(text)
compound = scores["compound"]

#### Mood Detection

Rule-based keyword matching

Deterministic and explainable

Designed for later ML replacement