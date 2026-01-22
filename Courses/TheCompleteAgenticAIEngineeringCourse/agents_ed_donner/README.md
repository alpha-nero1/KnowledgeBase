# Master AI Agentic Engineering -  build autonomous AI Agents

## Lesson Summaries
Week 1
1.4 - Building chat bot that responds to CV questions.
1.5 - Building the AI reasoning loop.

Week 2
2.2 - simple function tools (just use decorator), Calling agents as tools (.as_tool func), using Handoffs
2.3 - Models other than openai, structured outputs & guardrails.
2.4 - Deep research model for models that search the internet using openai hosted tools like `WebSearchTool`
2/deep_research - Python modules project implementation of the deep research program displaying via gradio UI.

Week 3
3.1 - 


## Notes
2026-01-23
Created deep_research project, very cool, asks for clarifying questions, great that gradle can facilitate html. perhaps there is a POC in that for facilitating agent chat.

Introduces Crew AI - you should use CrewAI when you need autonomous problem solving, creative collaboration or exploratory tasks.

### CrewAI Core Concepts
Agent - smallest autonomous unit of work, has a role, goal and a backstory lol - has memory and tools > little ai
Task - specific assignment to be carried out, with description, expected output and an agent attached to it.
Crew - aggregate of Agents & Tasks; can operate either in sequential mode or heriarchical mode.
> Bit more opinionated than agents sdk.
> Uses a yaml configuration! You create an agent based on a predefined config.
> Crewai projects cannot be done in notebooks because each crew setup is a whole uv project. `crewai create crew my_crew`


2026-01-22
Handoffs differ from tools in that they hand off the entire job to another AI if it realises another AI is better equipped for the job.
Do we have access to view traces if we are just using th chat gpt agent but interacting with our own MCP server?


2026-01-21
Bit confusing the difference between 2.2 & 2.3 was not super clear.
Once the lesson is done, rename it with a more descriptive name.

