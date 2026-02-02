# %% [markdown]
# ## Welcome to Week 4, Day 4
# 
# This is the start of an AWESOME project! Really simple and very effective.

# %%
import sys
# Add the venv to Python path if needed
venv_path = '/Users/alessandroalberga/Documents/Development/KnowledgeBase/Courses/TheCompleteAgenticAIEngineeringCourse/agents_ed_donner/.venv/lib/python3.12/site-packages'
if venv_path not in sys.path:
    sys.path.insert(0, venv_path)
    
# Verify we can import the modules
try:
    from langgraph.graph import StateGraph, START, END
    print("✓ Successfully imported from langgraph")
except ImportError as e:
    print(f"✗ Failed to import: {e}")
    print(f"Python executable: {sys.executable}")
    print(f"Python version: {sys.version}")
    
from typing import Annotated
from typing_extensions import TypedDict
from langgraph.graph import StateGraph, START
from langgraph.graph.message import add_messages
from dotenv import load_dotenv
from IPython.display import Image, display
import gradio as gr
from langgraph.prebuilt import ToolNode, tools_condition
import requests
import os
from langchain.agents import Tool

from langchain_openai import ChatOpenAI
from langgraph.checkpoint.memory import MemorySaver

# %%
load_dotenv(override=True)

# %% [markdown]
# ### Asynchronous LangGraph
# 
# To run a tool:  
# Sync: `tool.run(inputs)`  
# Async: `await tool.arun(inputs)`
# 
# To invoke the graph:  
# Sync: `graph.invoke(state)`  
# Async: `await graph.ainvoke(state)`

# %%
class State(TypedDict):    
    messages: Annotated[list, add_messages]


graph_builder = StateGraph(State)

# %%
pushover_token = os.getenv("PUSHOVER_TOKEN")
pushover_user = os.getenv("PUSHOVER_USER")
pushover_url = "https://api.pushover.net/1/messages.json"

def push(text: str):
    """Send a push notification to the user"""
    requests.post(pushover_url, data = {"token": pushover_token, "user": pushover_user, "message": text})

tool_push = Tool(
        name="send_push_notification",
        func=push,
        description="useful for when you want to send a push notification"
    )

# %% [markdown]
# ## Extra installation step - if you don't have Node and Playwright on your computer
# 
# Next, you need to install NodeJS and Playwright on your computer if you don't already have them. Please see instructions here:
# 
# [Node and Playwright setup](../setup/SETUP-node.md)

# %% [markdown]
# ## And now - after Installing Playwright, a heads up for Windows PC Users:
# 
# While executing the next few cells, you might hit a problem with the Playwright browser raising a NotImplementedError.
# 
# This should work when we move to python modules, but it can cause problems in Windows in a notebook.
# 
# If you it this error and would like to run the notebook, you need to make a small change which seems quite hacky! You need to do this AFTER installing Playwright (prior cells)
# 
# 1. Right click in `.venv` in the File Explorer on the left and select "Find in folder"
# 2. Search for `asyncio.set_event_loop_policy(WindowsSelectorEventLoopPolicy())`  
# 3. That code should be found in a line of code in a file called `kernelapp.py`
# 4. Comment out the entire else clause that this line is a part of - see the fragment below. Be sure to have the "pass" statement after the ImportError line.
# 5. Restart the kernel by pressing the "Restart" button above
# 
# ```python
#         if sys.platform.startswith("win") and sys.version_info >= (3, 8):
#             import asyncio
#  
#             try:
#                 from asyncio import WindowsProactorEventLoopPolicy, WindowsSelectorEventLoopPolicy
#             except ImportError:
#                 pass
#                 # not affected
#            # else:
#             #    if type(asyncio.get_event_loop_policy()) is WindowsProactorEventLoopPolicy:
#                     # WindowsProactorEventLoopPolicy is not compatible with tornado 6
#                     # fallback to the pre-3.8 default of Selector
#                     # asyncio.set_event_loop_policy(WindowsSelectorEventLoopPolicy())
# ```
# 
# Thank you to student Nicolas for finding this, and to Kalyan, Yaki, Zibin and Bhaskar for confirming that this worked for them! And to Vladislav for the extra pointers.
# 
# As an alternative, you can just move to a Python module (which we do anyway in Day 5)

# %%
# Introducing nest_asyncio
# Python async code only allows for one "event loop" processing aynchronous events.
# The `nest_asyncio` library patches this, and is used for special situations, if you need to run a nested event loop.

import nest_asyncio
nest_asyncio.apply()

# %% [markdown]
# ### The LangChain community
# 
# One of the remarkable things about LangChain is the rich community around it.
# 
# Check this out:
# 

# %%
from langchain_community.agent_toolkits import PlayWrightBrowserToolkit
from langchain_community.tools.playwright.utils import create_async_playwright_browser

# If you get a NotImplementedError here or later, see the Heads Up at the top of the notebook

async_browser =  create_async_playwright_browser(headless=False)  # headful mode
toolkit = PlayWrightBrowserToolkit.from_browser(async_browser=async_browser)
tools = toolkit.get_tools()

# %%
for tool in tools:
    print(f"{tool.name}={tool}")

# %%
tool_dict = {tool.name:tool for tool in tools}

navigate_tool = tool_dict.get("navigate_browser")
extract_text_tool = tool_dict.get("extract_text")

# Fantastic can use playwrite to get the text from a rendered website!!
await navigate_tool.arun({"url": "https://www.cnn.com"})
text = await extract_text_tool.arun({})

# %%
import textwrap
print(textwrap.fill(text))

# %%
all_tools = tools + [tool_push]

# %%
llm = ChatOpenAI(model="gpt-4o-mini")
llm_with_tools = llm.bind_tools(all_tools)


def chatbot(state: State):
    return {"messages": [llm_with_tools.invoke(state["messages"])]}


# %%

graph_builder = StateGraph(State)
graph_builder.add_node("chatbot", chatbot)
graph_builder.add_node("tools", ToolNode(tools=all_tools))
graph_builder.add_conditional_edges( "chatbot", tools_condition, "tools")
graph_builder.add_edge("tools", "chatbot")
graph_builder.add_edge(START, "chatbot")

memory = MemorySaver()
graph = graph_builder.compile(checkpointer=memory)
display(Image(graph.get_graph().draw_mermaid_png()))

# %%
config = {"configurable": {"thread_id": "10"}}

"""
    Facilitate chat with our graph using ainvoke!
"""
async def chat(user_input: str, history):
    result = await graph.ainvoke({"messages": [{"role": "user", "content": user_input}]}, config=config)
    return result["messages"][-1].content


gr.ChatInterface(chat, type="messages").launch()

# %%



