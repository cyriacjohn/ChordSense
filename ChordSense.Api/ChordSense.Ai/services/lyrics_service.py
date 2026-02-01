import json
from flask import Flask, request, jsonify
from langdetect import detect
from vaderSentiment.vaderSentiment import SentimentIntensityAnalyzer

app = Flask(__name__)
analyzer = SentimentIntensityAnalyzer()

MOOD_KEYWORDS = {
"happy": ["happy", "joy", "sunshine", "smile", "laugh", "bright"],
"sad": ["sad", "cry", "lonely", "tears", "pain", "hurt"],
     "angry": ["rage", "hate", "fight", "kill", "furious", "mad"],
     "romantic": ["love", "kiss", "heart", "darling", "baby", "sweet"],
     "chill": ["chill", "slow", "vibe", "relax", "calm", "smooth"]
 }

def detect_mood(text: str):
    text_lower = text.lower()
    for mood, keywords in MOOD_KEYWORDS.items():
        if any(word in text_lower for word in keywords):
            return mood
    return "unknown"


def analyze_lyrics_logic():
    data = request.json or {}
    text = data.get("text") or data.get("lyrics", "")

    if not text.strip():
        return jsonify({
            "error": "No lyrics provided"}), 400

    #Language detection
    lang = detect(text)

    #Sentiment detection
    sentiment_score = analyzer.polarity_scores(text)
    compound = sentiment_score['compound']

    if compound >= 0.05:
        sentiment = "positive"
    elif compound <= -0.05:
        sentiment = "negative"
    else:
        sentiment = "neutral"

    mood = detect_mood(text)

    return jsonify({
        "status": "ok",
        "language": lang,
        "sentiment": sentiment,
        "mood": mood
        })

if __name__ == "__main__":
    print("Lyrics AI runing on port 5001 (with NLP)")
    app.run(host="0.0.0.0",port=5001,debug=True)
