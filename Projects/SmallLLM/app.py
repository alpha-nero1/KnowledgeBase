import rich
import sys
from llama_index.llms.ollama import Ollama
import requests
from llama_index.core.prompts import Prompt

# running tinnyllama because I don't have a stupid nvidia GPU
DEFAULT_MODEL = "tinyllama" # use "mistral" when you do lol

console = rich.console.Console()


def __get_model_name():
    if len(sys.argv) > 1:
        return sys.argv[1]
    
    return DEFAULT_MODEL


def __is_ollama_live(model_name):
    try:
        requests.get("http://localhost:11434/api/tags", timeout=5)
        console.print(f'[bold green]✅ Ollama ({model_name}) is up[/]')
        return True
    except Exception as e:
        console.print('[bold red]❌ Ollama connection failed:[/]', e)
        return False


"""
    Main application entrypoint.
"""
def main():
    model_name = __get_model_name();
    with console.status(f'[bold blue]Starting {model_name}...[/]', spinner="dots"):
        if (not __is_ollama_live(model_name)):
            return;
        llm = Ollama(
            model=model_name,
            base_url="http://localhost:11434",
            request_timeout=300.0
        )

    console.print("[bold green]How can I help? (type 'exit' to quit)[/]")

    while True:
        prompt = console.input("[bold cyan]> [/]")
        if (prompt.lower() in { "exit", "quit" }):
            break
        if (len(prompt) == 0):
            continue

        # Stream the tokens
        # What is the "43a Rofe Street Tenant Agreement" document about?
        #try:
        with console.status('[bold blue]Thinking...[/]', spinner="dots"):
            stream = llm.stream(Prompt(prompt))

        for chunk in stream:
            console.print(chunk, end="", soft_wrap=True)
        console.print()

    console.print('[bold blue]Chat soon![/]')


if __name__ == "__main__":
    main()

