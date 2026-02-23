from worker import Worker
from leader import Leader

frontend_dev = Worker(
    role='Front End Developer',
    goal='Write concise, reusable, readible and professional front end code',
    backstory='You are a senior front end engineer, you specialise in writing concise, easy to read and high quality gradio UIs'
)

"""
    Responsible for making suggestions on wether the front end code is sufficient
"""
frontend_dev_reviewer = Worker(

)

backend_dev = Worker(
    role='Back End Developer',
    goal='Write concise, reusable, readible and professional back end python code',
    backstory='You are a senior backend python engineer, you specialise in writing concise, easy to read and high quality python code'
)

"""
    Responsible for making suggestions on wether the back end code is sufficient
"""
backend_dev_reviewer = Worker(

)

"""
    The tester actually writes intgration tests and 
"""
software_tester = Worker(
  role='',
  goal='',
  backstory=''
)

"""
    Product manager to research approaches, suggest how the product should behave.
"""
product_manager = Worker(
    role='',
    goal='',
    backstory=''
)


team_leader = Leader(
    role='Team Leader',
    goal='To lead and manage a team of software development workers to acheive the goal set out by the user',
    backstory='You are a driven software team leader who has many years of experience leading multi-functional teams and is great at collecting requirements and orchestrating workers to get a task done',
    workers=[
        frontend_dev, 
        frontend_dev_reviewer,
        backend_dev, 
        backend_dev_reviewer,
        software_tester, 
        product_manager
    ]
)
