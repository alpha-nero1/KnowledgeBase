from dotenv import load_dotenv
import gradio as gr
from lib.agents import team_leader
from lib.datastore import migrate, read_logs

load_dotenv(override=True)

"""
    Run the copilot-esque input loop for code iteration.
"""
def build_logs_rows():
	return [list(row) for row in read_logs()]


def handle_chat(user_message, chat_history):
	if not user_message:
		return "", chat_history

	response = team_leader.run(user_message)
	chat_history = chat_history + [(user_message, str(response))]
	return "", chat_history


def refresh_logs():
	rows = build_logs_rows()
	if not rows:
		return [["", "", "", "No logs yet."]]
	return rows


def build_ui():
	with gr.Blocks(title="AI Software Dev Team") as demo:
		gr.Markdown("# AI Software Dev Team")
		with gr.Row():
			with gr.Column(scale=2):
				chatbot = gr.Chatbot(height=420)
				user_input = gr.Textbox(label="Prompt", placeholder="Ask the team...")
				user_input.submit(handle_chat, [user_input, chatbot], [user_input, chatbot])
			with gr.Column(scale=1):
				logs_table = gr.Dataframe(
					headers=["Datetime", "Name", "Type", "Message"],
					value=refresh_logs(),
					row_count=(10, "dynamic"),
					col_count=(4, "fixed"),
					label="Logs"
				)
				refresh_btn = gr.Button("Refresh logs")
				refresh_btn.click(refresh_logs, outputs=logs_table)
				timer = gr.Timer(5)
				timer.tick(refresh_logs, outputs=logs_table)

	return demo


if __name__ == "__main__":
	migrate()
	ui = build_ui()
	ui.launch(share=True)

