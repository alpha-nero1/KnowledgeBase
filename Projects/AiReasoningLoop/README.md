# AI Reasoning Loop

A minimal Gradio chat interface for experimenting with agentic reasoning loops. Uses OpenAI if available, otherwise a local mock responder.

## TODO
Need to make the gradio UI update in realtime and the planning input should be dynamic

## Prerequisites
- Python 3.11+
- `uv` (https://github.com/astral-sh/uv) installed

## Setup
```bash
cd /Users/alessandroalberga/Documents/Development/KnowledgeBase/Projects/AiReasoningLoop
uv sync
```

Optional: set OpenAI credentials and model
```bash
export OPENAI_API_KEY=...   # your key
export OPENAI_MODEL=gpt-4o-mini
```
You can also put these in `.env`.

## Run the app
```bash
uv run python app.py
```
This launches a local Gradio UI in your browser.

## Notes
- If `OPENAI_API_KEY` is not set or an error occurs, the app falls back to a local mock responder.
- The Gradio entry point is in [Projects/AiReasoningLoop/app.py](Projects/AiReasoningLoop/app.py). The original tooling loop lives in [Projects/AiReasoningLoop/main.py](Projects/AiReasoningLoop/main.py).
