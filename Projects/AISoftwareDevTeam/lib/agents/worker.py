from agents import Agent, Runner, ModelSettings


def get_agent_instruction(role, goal, backstory, date, has_tools=False):
    base_instruction = (
        f"You are a {role}, your goal is to {goal}. {backstory}. "
        f"The current date is {date}"
    )
    if not has_tools:
        return base_instruction

    return (
        f"{base_instruction}. "
        "You MUST use your available tools to create or edit files whenever the task requests code changes. "
        "Do not only describe code in plain text. Write actual files using the tools. "
        "All project artifacts must be written under the output folder. "
        "Before finalizing, use tools to read back the updated files and verify they contain the requested changes."
    )


def get_agent_name(role):
    return f'{role} Agent'


def get_tool_name(role):
    base_name = get_agent_name(role)
    sanitized = "".join(
        char if (char.isalnum() or char in {"_", "-"}) else "_"
        for char in base_name
    )
    while "__" in sanitized:
        sanitized = sanitized.replace("__", "_")
    return sanitized.strip("_")


def get_tool_description(role, goal, backstory):
    return f"A tool for {goal}, calls a {get_agent_name(role)} who {backstory}"


class Worker():
    __role = None
    __goal = None
    __backstory = None
    __agent = None

    def __init__(
        self, 
        role, 
        goal,
        backstory,
        date = '23/02/2026',
        model = 'gpt-4o-mini', 
        tools = None
    ):
        self.__role = role
        self.__goal = goal
        self.__backstory = backstory
        tools = tools or []
        model_settings = ModelSettings(tool_choice='required') if tools else ModelSettings()

        self.__agent = Agent(
            name=get_agent_name(role),
            instructions=get_agent_instruction(
                self.__role,
                self.__goal,
                self.__backstory,
                date,
                has_tools=bool(tools),
            ),
            tools=tools,
            model=model,
            model_settings=model_settings,
        )

    def run(self, prompt):
        return Runner.run(self.__agent, prompt)
    
    def expose_as_tool(self):
        return self.__agent.as_tool(
            tool_name=get_tool_name(self.__role), 
            tool_description=get_tool_description(self.__role, self.__goal, self.__backstory)
        )
    
    def __str__(self):
        return f'{{"role": {self.__role}, "type": "Worker", "goal": {self.__goal}, "backstory": {self.__backtory}}}'