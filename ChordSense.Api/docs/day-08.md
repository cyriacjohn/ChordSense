# Day 8 Development Log  
## Persistence, AI Integration & Data Modeling

---

## Day 8 Objective

Transform ChordSense from a **stateless AI API** into a **stateful backend system** by:

- Persisting AI analysis results in a database
- Integrating Python AI services with ASP.NET Core
- Designing flexible data models for evolving AI outputs
- Exposing history APIs for retrieval and analysis

Day 8 marks the shift from *“AI demo”* → *“real backend product”*.

---

## High-Level Architecture

Client (Postman / UI)
↓
ASP.NET Core API (Orchestration + Persistence)
↓
Python Flask AI Service (Stateless Intelligence)
↓
SQL Server (Persistent Storage)

**Core principle:**
> Python computes.  
> ASP.NET orchestrates and stores.

---

## Database Design — `AnalysisResult`

A **single unified table** is used to store both lyrics and audio analysis.

### Why one table?
- Faster MVP development
- Simpler queries
- Flexible schema for evolving AI models
- Avoids premature normalization

Lyrics Analysis Flow
Endpoint
POST /api/analysis/lyrics

Flow

Validate lyrics input

Call Flask NLP endpoint

Deserialize response into DTO

Map response fields into AnalysisResult

Persist to SQL Server

Return AI response to client

Audio Analysis Flow
Endpoint
POST /api/analysis/audio

Flow

Accept audio file (multipart/form-data)

Forward file stream to Flask

Receive DSP analysis JSON

Deserialize using DTO with explicit JSON mapping

Persist musical attributes + raw JSON

Return parsed AI response

JSON Mapping (Critical Learning)

Flask returns snake_case JSON.

ASP.NET uses System.Text.Json, which:

Is case-insensitive

Does NOT convert underscores automatically

Solution

Use explicit mapping via JsonPropertyName: