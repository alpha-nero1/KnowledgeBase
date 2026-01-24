from crewai import Agent, Crew, Process, Task
from crewai.project import CrewBase, agent, crew, task
from crewai.agents.agent_builder.base_agent import BaseAgent
from typing import List

@CrewBase
class Coder():

    agents: List[BaseAgent]
    tasks: List[Task]

    @agent
    def coder(self) -> Agent:
        return Agent(
            config=self.agents_config['coder'],
            verbose=True,
            allow_code_execution=True,
            # Ensures it runs inside a docker container.
            code_execution_mode="safe",
            max_execution_time=60,
            max_retry_limit=5
        )

    @task
    def code(self) -> Task:
        return Task(
            config=self.tasks_config['code'],
        )

    @crew
    def crew(self) -> Crew:
        return Crew(
            agents=self.agents,
            tasks=self.tasks, 
            process=Process.sequential,
            verbose=True,
        )
