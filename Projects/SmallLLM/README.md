# 👾 Projects / SmallLLM
*⚠️The code in this project is NOT best practice; for demonstration purposes ONLY*

## Project setup
1. Python environment
```
-- Start env
python -m venv .venv
.venv\Scripts\activate  # activate it (Windows)
```

2. Install dependencies
```
pip install llama-index llama-index-llms-ollama  llama-index-embeddings-huggingface chromadb rich sentence-transformers
```

3. Install pdf dependencies
```
pip install pypdf pdfplumber unstructured pillow
```

4. Install and setup Ollama
- Install the sdk https://ollama.com/download/windows
- Open the cmd and run `ollama pull mistral` followed by `ollama serve` (HEADS UP THIS DOWNLOADS A 4GB FILE)

https://chatgpt.com/c/685d2671-4c48-8005-bda2-802e46221afd