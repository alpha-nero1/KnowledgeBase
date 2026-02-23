import gradio as gr
from openai import AsyncOpenAI
from dotenv import load_dotenv

load_dotenv(override=True)

client = AsyncOpenAI()


"""
    Generate clarifying questions based on the user's initial query.

    This is just a simple method.
"""
async def generate_clarifying_questions(query: str) -> list[str]:
    response = await client.chat.completions.create(
        model="gpt-4o",
        messages=[
            {
                "role": "system",
                "content": """You are a research assistant helping to clarify user research requests. 
Generate 3-5 focused clarifying questions to help produce better research results. Questions should cover:
- Scope and depth (broad overview vs deep dive)
- Target audience (technical vs general audience)
- Specific aspects of interest
- Time period or recency requirements
- Output format preferences

Return ONLY the questions, one per line, numbered."""
            },
            {
                "role": "user",
                "content": f"Initial research query: {query}\n\nWhat clarifying questions should I ask?"
            }
        ],
        temperature=0.7
    )
    questions_text = response.choices[0].message.content
    questions = [q.strip() for q in questions_text.strip().split('\n') if q.strip()]
    return questions


"""
    Handle the initial query and show clarifying questions.
"""
async def process_initial_query(query: str):
    if not query or not query.strip():
        return "Please enter a research topic.", gr.update(visible=False), ""
    
    questions = await generate_clarifying_questions(query)
    questions_html = "<div style='padding: 15px; background-color: #f0f7ff; border-radius: 8px; margin: 10px 0;'>"
    questions_html += "<h3 style='margin-top: 0; color: #1e40af;'>💡 Let's refine your research</h3>"
    questions_html += "<p style='color: #1e3a8a;'>Please answer these questions to help us produce better results:</p>"
    questions_html += "<ol style='color: #1e3a8a;'>"
    for q in questions:
        # Remove numbering if it exists
        clean_q = q.lstrip('0123456789. ')
        questions_html += f"<li style='color: #1e3a8a;'>{clean_q}</li>"
    questions_html += "</ol>"
    questions_html += "<p style='color: #64748b; font-size: 0.9em; margin-bottom: 0;'>You can skip this step by clicking 'Start Research' without answering.</p>"
    questions_html += "</div>"
    
    # gr.update asks for input from the user!
    return questions_html, gr.update(visible=True), ""