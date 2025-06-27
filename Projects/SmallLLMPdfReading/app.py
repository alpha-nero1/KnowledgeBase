import pathlib, rich
from llama_index.core import (
    VectorStoreIndex,
    Settings
)
from llama_index.embeddings.huggingface import HuggingFaceEmbedding
from llama_index.llms.ollama import Ollama
import requests
from llama_index.readers.file import PDFReader
from pathlib import Path
import sys

# put your PDFs here
PDF_DIR = "docs"

# running tinnyllama because I don't have a stupid nvidia GPU
DEFAULT_MODEL = "tinyllama" # use "mistral" when you do lol

console = rich.console.Console()


def __get_model_name():
    if len(sys.argv) > 1:
        return sys.argv[1]
    
    return DEFAULT_MODEL


def __get_pdf_docs():
    # Create the reader
    pdf_reader = PDFReader()

    # Load all PDF files in the directory
    documents = []
    for pdf_file in Path(PDF_DIR).glob("*.pdf"):
        docs = pdf_reader.load_data(file=pdf_file)
        documents.extend(docs)
    return documents


def __is_ollama_live(model_name):
    try:
        requests.get("http://localhost:11434/api/tags", timeout=5)
        console.print(f'[bold green]✅ Ollama ({model_name}) is up[/]')
        return True
    except Exception as e:
        console.print('[bold red]❌ Ollama connection failed:[/]', e)
        return False
    

def __docs_exist():
    if not pathlib.Path(PDF_DIR).exists():
        console.print(f"[bold red]❌ Directory '{PDF_DIR}' does not exist.[/]")
        return False
    return True


def __build_service(model):
    Settings.llm = Ollama(
        model=model,
        base_url="http://localhost:11434",
        request_timeout=600.0
    )

    Settings.embed_model = HuggingFaceEmbedding(
        model_name="sentence-transformers/all-MiniLM-L6-v2",
        cache_folder=".hf_cache"
    )


def build_or_load_index():
    docs = __get_pdf_docs()

    try:
        index = VectorStoreIndex.from_documents(docs)
    except Exception as e:
        print("💥 Indexing failed:", type(e).__name__, e)
        raise

    print("✅ Index built successfully!")
    return index


"""
    Main application entrypoint.
"""
def main():
    model_name = __get_model_name();
    with console.status(f'[bold blue]Starting {model_name}...[/]', spinner="dots"):
        if (
            not __is_ollama_live(model_name)
            or not __docs_exist()
        ):
            return
        
        __build_service(model_name)
        index = build_or_load_index()
        qa = index.as_query_engine(streaming=True)

    console.print("[bold green]Ask me anything about your PDFs! (type 'exit' to quit)[/]")

    while True:
        prompt = console.input("[bold cyan]> [/]")
        if (prompt.lower() in { "exit", "quit" }):
            break
        if (len(prompt) == 0):
            continue

        # Stream the tokens
        with console.status('[bold blue]Thinking...[/]', spinner="dots"):
            stream = qa.query(prompt)

        for chunk in stream.response_gen:
            console.print(chunk, end="", soft_wrap=True)
        console.print()
        
    console.print('[bold blue]Chat soon![/]')


if __name__ == "__main__":
    main()

