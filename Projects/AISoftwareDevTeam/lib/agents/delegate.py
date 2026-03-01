from .worker import Worker

def create_instruction(role, goal, backstory, date):
    return f"You are a {role}, your goal is to {goal}. {backstory}. The current date is {date}"

def workers_as_tools(workers):
    if not workers:
        return []
    return [worker.expose_as_tool() for worker in workers]

class Delegate(Worker):
    def __init__(
        self, 
        role, 
        goal,
        backstory,
        date = '23/02/2026',
        model = 'gpt-4o-mini', 
        workers = []
    ): 
        super().__init__(role, goal, backstory, date, model, workers_as_tools(workers))

    def __str__(self):
        return f'{{"role": {self.__role}, "type": "Delegate", "goal": {self.__goal}, "backstory": {self.__backstory}}}'