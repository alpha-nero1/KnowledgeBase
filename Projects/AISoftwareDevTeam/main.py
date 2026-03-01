from dotenv import load_dotenv
import gradio as gr
import gradio_client.utils as gradio_client_utils
from agents import set_trace_processors, trace
from lib.agents.agents import team_leader
from lib.datastore import migrate, read_logs
from lib.logger import LogTracer, make_trace_id

load_dotenv(override=True)


def _patch_gradio_schema_parser():
	original_get_type = gradio_client_utils.get_type

	def safe_get_type(schema):
		if isinstance(schema, bool):
			return "Any"
		return original_get_type(schema)

	gradio_client_utils.get_type = safe_get_type


_patch_gradio_schema_parser()

"""
    Run the copilot-esque input loop for code iteration.
"""
def build_logs_rows():
	return [list(row) for row in read_logs()]


def _extract_response_text(response):
	if hasattr(response, "final_output"):
		return str(response.final_output)
	return str(response)


async def handle_chat(user_message, chat_history):
	if not user_message:
		return "", chat_history

	with trace("team_leader_run", trace_id=make_trace_id("teamleader")):
		response = await team_leader.run(user_message)
	response_text = _extract_response_text(response)
	chat_history = chat_history + [
		{"role": "user", "content": user_message},
		{"role": "assistant", "content": response_text},
	]
	return "", chat_history


def refresh_logs():
	rows = build_logs_rows()
	if not rows:
		return [["", "", "", "No logs yet."]]
	return rows


def build_ui():
	with gr.Blocks(title="AI Software Dev Team") as demo:
		gr.Markdown("# AI Software Dev Team")
		gr.Markdown('## Example prompts')
		gr.Markdown('`Help me write a program in python that helps me to calculate when my mortgage will be paid off`')
		with gr.Row():
			with gr.Column(scale=2):
				chatbot = gr.Chatbot(height=420, type="messages")
				user_input = gr.Textbox(label="Prompt", placeholder="Ask the team...")
				user_input.submit(handle_chat, [user_input, chatbot], [user_input, chatbot], show_api=False)
			with gr.Column(scale=1):
				logs_table = gr.Dataframe(
					headers=["Datetime", "Name", "Type", "Message"],
					value=refresh_logs(),
					row_count=(10, "dynamic"),
					col_count=(4, "fixed"),
					label="Logs"
				)
				refresh_btn = gr.Button("Refresh logs")
				refresh_btn.click(refresh_logs, outputs=logs_table, show_api=False)
				timer = gr.Timer(5)
				timer.tick(refresh_logs, outputs=logs_table, show_api=False)

	return demo


if __name__ == "__main__":
	migrate()
	set_trace_processors([LogTracer()])
	ui = build_ui()
	ui.launch(share=True, show_api=False)

