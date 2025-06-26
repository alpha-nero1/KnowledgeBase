import pathlib, rich, chromadb
from llama_index.core import (
    SimpleDirectoryReader, VectorStoreIndex,
    Settings
)
from llama_index.embeddings.huggingface import HuggingFaceEmbedding
from llama_index.llms.ollama import Ollama
from llama_index.vector_stores.chroma import ChromaVectorStore
from llama_index.core import StorageContext
from chromadb import PersistentClient

# put your PDFs here
PDF_DIR = "docs"

# on-disk vector store
CHROMA_DIR = "chroma_db"

"""
    Build the LLM Service.
"""
def build_service():
    # 1. LLM Setup
    llm = Ollama(model="mistral", temperature=0)

    # 2. Embeddings (tiny, CPU friendly model)
    embed = HuggingFaceEmbedding(model_name="sentence-transformers/all-MiniLM-L6-v2")

    # 3. Global settings
    Settings.llm = llm
    Settings.embed_model = embed

def build_or_load_index():
    chroma_client = PersistentClient(path=CHROMA_DIR)
    vector_store = ChromaVectorStore(
        collection_name="pdf_docs",
        client=chroma_client,
    )
    if pathlib.Path(CHROMA_DIR).exists():
        storage_context = StorageContext.from_defaults(vector_store=vector_store)
        return VectorStoreIndex.from_vector_store(vector_store, storage_context=storage_context)

    reader = SimpleDirectoryReader(PDF_DIR, recursive=True)
    docs = reader.load_data()

    index = VectorStoreIndex.from_documents(
        docs,
        storage_context=StorageContext.from_defaults(vector_store=vector_store),
        show_progress=True,
    )
    index.storage_context.persist()
    return index

def main():
    build_service()
    index = build_or_load_index()
    qa = index.as_query_engine(streaming=True)

    console = rich.console.Console()
    console.print("[bold green]Ask me anything about your PDFs! (type 'exit' to quit)[/]")

    while True:
        prompt = console.input("[bold cyan]> [/]")
        if (prompt.lower() in { "exit", "quit" }):
            break
        # Stream the tokens
        for chunk in qa.query(prompt):
            console.print(chunk, end="", soft_wrap=True)
        # Newline
        console.print()

if __name__ == "__main__":
    main()