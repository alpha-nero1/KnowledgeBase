import gradio as gr

from lib.vocab_agent import VocabAgent


agent = VocabAgent('German', 'A1', 'A2')


def respond(
    message: str,
    history: list[tuple[str, str]],
    target_language: str,
    current_level: str,
    target_level: str,
    word_type: str,
) -> str:
    agent.update_settings(target_language, current_level, target_level, word_type)
    if message.startswith("CHAT -"):
        return agent.chat(message, history)
    return agent.task_chat(message, history)


demo = gr.ChatInterface(
    fn=respond,
    title="VocabBot Chat",
    description="Ask for definitions, examples, synonyms, antonyms, and memory tricks. Start your message with 'CHAT -' in order to break from topic task chat",
    additional_inputs=[
        gr.Textbox(value="German", label="Target Language", placeholder="e.g. Spanish, French, Japanese..."),
        gr.Textbox(value="A1", label="Current Level", placeholder="e.g. A1, B1, C1..."),
        gr.Textbox(value="A2", label="Target Level", placeholder="e.g. A2, B2, C2..."),
        gr.Dropdown(
            value="mixed",
            label="Word Type",
            choices=["mixed", "verbs", "nouns", "adjectives", "adverbs", "phrases"],
        ),
    ],
    additional_inputs_accordion=gr.Accordion(label="Settings", open=True),
)


if __name__ == "__main__":
    demo.launch(server_name="0.0.0.0", server_port=7860)
