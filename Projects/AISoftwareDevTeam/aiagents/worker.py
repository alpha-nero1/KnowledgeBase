from agents import Agent, Runner

def get_agent_instruction(role, goal, backstory, date):
    return f"You are a {role}, your goal is to {goal}. {backstory}. The current date is {date}"

def get_agent_name(role):
    return f'{role} Agent'

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
        tools = []
    ):
        self.__role = role
        self.__goal = goal
        self.__backstory = backstory

        self.__agent = Agent(
            name=get_agent_name(role),
            instructions=get_agent_instruction(self.__role, self.__goal, self.__backstory, date),
            tools=tools,
            model=model
        )

    def run(self, prompt):
        return Runner.run(self.__agent, prompt)
    
    def expose_as_tool(self):
        return self.__agent.as_tool(
            tool_name=get_agent_name(self.__role), 
            tool_description=get_tool_description(self.__role, self.__goal, self.__backstory)
        )
    
    def __str__(self):
        return f'{{"role": {self.__role}, "type": "Worker", "goal": {self.__goal}, "backstory": {self.__backtory}}}'