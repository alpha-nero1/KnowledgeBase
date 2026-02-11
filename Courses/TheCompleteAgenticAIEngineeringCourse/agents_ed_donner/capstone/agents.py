from typing import Annotated
from typing_extensions import TypedDict
from langgraph.graph import StateGraph, START, END
from langgraph.graph.message import add_messages
from dotenv import load_dotenv
from langgraph.prebuilt import ToolNode
from langchain_openai import ChatOpenAI
from langgraph.checkpoint.memory import MemorySaver
from langchain_core.messages import AIMessage, HumanMessage, SystemMessage
from typing import List, Any, Optional, Dict
from pydantic import BaseModel, Field
from sidekick_tools import playwright_tools, other_tools
import uuid
import asyncio
from datetime import datetime

load_dotenv(override=True)

class Agent():
    def __init__(self, name, model = 'gpt-4o-mini', role = '', goal = '', backstory = ''):
        self.__name = name;
        self.__model_name = model;
        self.__init_system_message()

    async def setup(self):
        self.__model = ChatOpenAI(model=self.__model_name)
        print('Call setup')

    async def run(self):
        print('Run llm')

    def __init_system_message(self, role, goal, backstory):
        self.__system_message = f"You are a {role}, your goal is to {goal}, your backstory is that {backstory}";
        
    