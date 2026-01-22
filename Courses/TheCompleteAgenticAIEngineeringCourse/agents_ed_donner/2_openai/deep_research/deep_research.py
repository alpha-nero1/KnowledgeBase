"""
    User interface entrypoint using gradio.
"""

import gradio as gr
from dotenv import load_dotenv
from research_manager import ResearchManager
from openai import AsyncOpenAI
from clarify import process_initial_query

load_dotenv(override=True)

client = AsyncOpenAI()

"""
    Actual runner tapping into our research manager class.

    This callback function is a 'generator' meaning it will show
    interim results as well as the final output.
"""
async def run(query: str, clarifications: str):
    # Combine original query with clarifications if provided
    enhanced_query = query
    if clarifications and clarifications.strip():
        enhanced_query = f"{query}\n\nAdditional context and requirements:\n{clarifications}"
    
    async for chunk in ResearchManager().run(enhanced_query):
        yield chunk


with gr.Blocks(theme=gr.themes.Default(primary_hue="sky")) as ui:
    gr.Markdown("# 🔬 Deep Research Assistant")
    gr.Markdown("*Ask clarifying questions for better, more targeted research results*")
    
    with gr.Row():
        query_textbox = gr.Textbox(
            label="What topic would you like to research?", 
            placeholder="e.g., 'Impact of AI on healthcare'",
            lines=2
        )
    
    submit_button = gr.Button("Submit Query", variant="primary", size="lg")
    
    questions_html = gr.HTML(visible=True)
    
    clarifications_group = gr.Group(visible=False)
    with clarifications_group:
        clarifications_textbox = gr.Textbox(
            label="Your answers to the clarifying questions (optional)",
            placeholder="Feel free to answer any or all of the questions above...",
            lines=5
        )
        with gr.Row():
            run_button = gr.Button("Start Research", variant="primary", size="lg")
            skip_button = gr.Button("Skip & Start Research", variant="secondary")
    
    report = gr.Markdown(label="Research Report", visible=True)
    
    # Event handlers - automatically trigger when user submits query
    submit_button.click(
        fn=process_initial_query,
        inputs=query_textbox,
        outputs=[questions_html, clarifications_group, report]
    )
    
    query_textbox.submit(
        fn=process_initial_query,
        inputs=query_textbox,
        outputs=[questions_html, clarifications_group, report]
    )
    
    run_button.click(
        fn=run,
        inputs=[query_textbox, clarifications_textbox],
        outputs=report
    )
    
    skip_button.click(
        fn=lambda q: run(q, ""),
        inputs=query_textbox,
        outputs=report
    )

ui.launch(inbrowser=True)

