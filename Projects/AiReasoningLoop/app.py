import os
import gradio as gr
from dotenv import load_dotenv
from loop import loop

# Load environment variables from .env if present
load_dotenv(override=True)

def respond(message: str, history: list[dict]) -> str:
    # Build message list from history + new message
    messages = []
    for turn in history:
        role = "assistant" if turn.get("role") == "assistant" else "user"
        content = turn.get("content", "")
        messages.append({"role": role, "content": content})
    
    # Add the user's new message
    messages.append({"role": "user", "content": message})
    
    try:
        # Call the reasoning loop, which handles tool calls and returns the final response
        result = loop(messages)
        return result or "No response generated."
    except Exception as e:
        return f"Error during reasoning loop: {e}"


def build_ui() -> gr.Blocks:
    with gr.Blocks(title="AI Reasoning Loop Chat") as demo:
        gr.Markdown("""
        # AI Reasoning Loop Chat
        Chat with an agentic reasoning loop that can create and complete todos.
        The agent will break down your requests, create todo items, and work through them.
        """)
        chat = gr.ChatInterface(
            fn=respond,
            examples=[
                "Plan a 3-step morning routine and mark each as done.",
                "Create a todo list for learning Python basics, then mark them complete.",
            ],
        )
    return demo


if __name__ == "__main__":
    ui = build_ui()
    ui.launch()

