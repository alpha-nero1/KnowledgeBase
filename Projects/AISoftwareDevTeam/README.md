# AI Software Development Team

This is a capstone project of the "TheCompleteAgenticAIEngineeringCourse" and demonstrates a heirarchical agentic workflow using the agents SDK.

In this project we set up a development team with a:
1. Team leader
2. Front end dev
3. Back end dev
4. Tester

The software development team takes a prompt and works out the solution, executing code and debugging untill workable software is produced. This essentially simulates copilot.

## How to run
`uv init` - during proj creation.
`uv sync`
`uv run python main.py`

## Work log
27/02/2026 - Fixed up the uv setup and was able to ask the AI flow to create a program but I am just not seeing any logs show up in the log widget in the UI, is it not logging? is it not doing anything? because I can see that the output folder is empty.

## Difference between venv and uv
Venv is old and limited, uv is new and all encompassing including package management and workspace management automatically.
- .toml = Tom's obvious, minimal language.

## Test prompt
Build me a program that given a range of relevant inputs from the terminal, the program will calculate in exactly how many years your mortgage will be paid off.
