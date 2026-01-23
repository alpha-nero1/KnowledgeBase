from crewai import Agent, Crew, Process, Task
from crewai.project import CrewBase, agent, crew, task
from crewai.agents.agent_builder.base_agent import BaseAgent
from typing import List

@CrewBase
class Debate():
    """Debate crew"""

    agents: List[BaseAgent]
    tasks: List[Task]

    @agent
    def debater(self) -> Agent: return Agent(config=self.agents_config['debater'], verbose=True)

    @agent
    def judge(self) -> Agent: return Agent(config=self.agents_config['judge'], verbose=True)

    @task
    def propose(self) -> Task: return Task(config=self.tasks_config['propose'])

    @task
    def oppose(self) -> Task: return Task(config=self.tasks_config['oppose'])

    @task
    def decide(self) -> Task: return Task(config=self.tasks_config['decide'])

    @crew
    def crew(self) -> Crew: return Crew(
        agents=self.agents,
        tasks=self.tasks,
        # Here we decide wether the process should be sequential OR heirarchical
        process=Process.sequential,
        verbose=True,
    )
