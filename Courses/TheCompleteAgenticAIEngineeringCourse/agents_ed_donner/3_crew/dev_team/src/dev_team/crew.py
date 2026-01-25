from crewai import Agent, Crew, Process, Task
from crewai.project import CrewBase, agent, crew, task
from crewai.agents.agent_builder.base_agent import BaseAgent
from typing import List
# If you want to run a snippet of code before or after the crew starts,
# you can use the @before_kickoff and @after_kickoff decorators
# https://docs.crewai.com/concepts/crews#example-crew-class-with-decorators

@CrewBase
class DevTeam():
    agents: List[BaseAgent]
    tasks: List[Task]

    @agent
    def dev_lead(self) -> Agent:
        return Agent(
            config=self.agents_config['dev_lead'], 
            verbose=True
        )
    
    @agent
    def backend_dev(self) -> Agent:
        return Agent(
            config=self.agents_config['backend_dev'], 
            verbose=True,
            allow_code_execution=True,
            # Ensures it runs inside a docker container.
            code_execution_mode="safe",
            max_execution_time=240,
            max_retry_limit=5
        )
    
    @agent
    def frontend_dev(self) -> Agent:
        return Agent(
            config=self.agents_config['frontend_dev'], 
            verbose=True,
        )
    
    @agent
    def test_dev(self) -> Agent:
        return Agent(
            config=self.agents_config['test_dev'], 
            verbose=True,
            allow_code_execution=True,
            # Ensures it runs inside a docker container.
            code_execution_mode="safe",
            max_execution_time=240,
            max_retry_limit=5
        )

    @task
    def design_task(self) -> Task:
        return Task(
            config=self.tasks_config['design_task'], 
        )
    
    @task
    def clarification_request_task(self) -> Task:
        return Task(
            config=self.tasks_config['clarification_request_task'],
        )
    
    @task
    def clarification_response_task(self) -> Task:
        return Task(
            config=self.tasks_config['clarification_response_task'],
        )
    
    @task
    def backend_task(self) -> Task:
        return Task(
            config=self.tasks_config['backend_task'], 
        )
    
    @task
    def frontend_task(self) -> Task:
        return Task(
            config=self.tasks_config['frontend_task'], 
        )
    
    @task
    def test_task(self) -> Task:
        return Task(
            config=self.tasks_config['test_task'], 
        )

    @crew
    def crew(self) -> Crew:
        return Crew(
            agents=self.agents,
            tasks=self.tasks,
            process=Process.sequential,
            verbose=True,
        )
