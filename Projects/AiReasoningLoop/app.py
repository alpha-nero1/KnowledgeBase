import os
import gradio as gr
from dotenv import load_dotenv
from loop import loop

# Load environment variables from .env if present
load_dotenv(override=True)

def respond(message: str, history: list[dict]):
    """Generator that yields real-time updates during reasoning."""
    # Build message list from history + new message
    messages = []
    for turn in history:
        role = "assistant" if turn.get("role") == "assistant" else "user"
        content = turn.get("content", "")
        messages.append({"role": role, "content": content})
    
    # Add the user's new message
    messages.append({"role": "user", "content": message})
    
    # Accumulate all updates for display
    accumulated = ""
    
    try:
        # Initial message
        accumulated = "🚀 Starting reasoning loop...\n\n"
        yield accumulated
        
        # Get the loop generator
        loop_gen = loop(messages, stream_callback=True)
        
        # Iterate through all updates
        for update in loop_gen:
            accumulated += update
            yield accumulated
        
        # Get the final result (loop returns after yielding all updates)
        try:
            result = next(loop_gen)
        except StopIteration as e:
            result = e.value
        
        # Yield final result
        if result:
            accumulated += f"\n✅ **Final Response:**\n{result}"
            yield accumulated
    except Exception as e:
        yield f"❌ Error during reasoning loop: {e}"


def build_ui() -> gr.Blocks:
    with gr.Blocks(title="AI Reasoning Loop Chat", theme=gr.themes.Soft()) as demo:
        gr.Markdown("""
        # 🧠 AI Reasoning Loop Chat
        Watch the agent think in real-time as it creates and completes todos.
        """)
        
        with gr.Row():
            with gr.Column(scale=3):
                chat = gr.ChatInterface(
                    fn=respond,
                    examples=[
                        "Plan a 3-step morning routine and mark each as done.",
                        "Create a todo list for learning Python basics, then mark them complete.",
                        "Help me organize a small birthday party with 4 tasks.",
                        "Build a simple Python web scraper - break it down into steps.",
                    ],
                    title=None,
                )
            
            with gr.Column(scale=1):
                gr.Markdown("### 🎯 Quick Planning Templates")
                
                with gr.Accordion("Daily Routine", open=False):
                    daily_btn = gr.Button("Morning Routine (3 steps)", size="sm")
                    daily_btn.click(
                        lambda: "Plan a 3-step morning routine and mark each as done.",
                        None,
                        chat.textbox
                    )
                
                with gr.Accordion("Learning Plan", open=False):
                    learn_btn = gr.Button("Python Basics (5 topics)", size="sm")
                    learn_btn.click(
                        lambda: "Create a 5-step learning plan for Python basics and mark each complete.",
                        None,
                        chat.textbox
                    )
                
                with gr.Accordion("Project Planning", open=False):
                    project_input = gr.Textbox(
                        label="Project Name",
                        placeholder="e.g., Build a blog",
                        lines=1
                    )
                    steps_slider = gr.Slider(
                        minimum=3,
                        maximum=10,
                        value=5,
                        step=1,
                        label="Number of Steps"
                    )
                    project_btn = gr.Button("Generate Plan", size="sm", variant="primary")
                    project_btn.click(
                        lambda proj, steps: f"Break down '{proj}' into {int(steps)} actionable steps and mark each as complete.",
                        [project_input, steps_slider],
                        chat.textbox
                    )
                
                gr.Markdown("""
                ### 💡 Tips
                - Be specific about what you want to plan
                - The agent will create todos and mark them off
                - Watch the real-time updates as it works
                """)
    
    return demo


if __name__ == "__main__":
    ui = build_ui()
    ui.launch()

