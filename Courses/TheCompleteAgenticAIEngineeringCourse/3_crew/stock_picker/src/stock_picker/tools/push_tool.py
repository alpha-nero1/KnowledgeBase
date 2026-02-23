from crewai.tools import BaseTool
from typing import Type
from pydantic import BaseModel, Field

"""
    This is how we create our own tools that the agents can use.
"""

class PushToolInputSchema(BaseModel):
    """ A message to be sent to the user """
    message: str = Field(..., description="The message to be sent to the user.")

class PushTool(BaseTool):
    name: str = "Send a push notification"
    description: str = (
        "This tool is used to send a push notification to the user"
    )
    args_schema: Type[BaseModel] = PushToolInputSchema

    def _run(self, message: str) -> str:
        # Implementation goes here
        return '{"sendMessage": "ok"}'
