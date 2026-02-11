# Master AI Agentic Engineering -  build autonomous AI Agents

## Lesson Summaries
Week 1
1.4 - Building chat bot that responds to CV questions.
1.5 - Building the AI reasoning loop.

Week 2 (All about openai agents sdk)
2.2 - simple function tools (just use decorator), Calling agents as tools (.as_tool func), using Handoffs
2.3 - Models other than openai, structured outputs & guardrails.
2.4 - Deep research model for models that search the internet using openai hosted tools like `WebSearchTool`
2/deep_research - Python modules project implementation of the deep research program displaying via gradio UI.

Week 3 (All about CrewAi)
3.1 - using crew to set up a team of agents to simulate a debate about a topic - very cool.
3.2 - using crew to do financial research on a company including Serper tool use to search google
3.3 - stock_picker; actually using a heirarchical strategy (manager agent) to assign the right task to the right agents, included custom tool definition and use.
3.4. - stock picker making use of memory (long & short term!)
3.5. - Coder agent - generate python and execute that python to continue the flow, pretty simple crew config and just produces an output file.
3.6. - Organise an AI engineering team to create a working program! - Tasks can return callbacks to create new tasks, this is how we can recursievly call tasks
    - Also can use crew guardrails to make checks along the task chain

Week 4 (LangGraph)
4.1. - Demonstrates basic use of langgraph with no AI involved.
4.2. - LangGraph and LangChain used together with memory.
4.3. - Arm an agent with playwright to use headless or headfull web search.
4.4. - Create a personal assistant with a plethora of tools.


Week 5 (Autogen)
5.1 - Simple agent chat with AutoGen AgentChat
5.2 - Using structured outputs with AutoGen AgentChat and adapting tools from langchain to use in AgentChat.
5.3 - How to use AutoGen core and demonstrating its agent and agentchat agnostic use.
5.4 - Create a playpen of distributed agents and interact with eachother from all over the globe and no matter what language they are written in.
5.5 - Agent creator - agents create other agents 

Week 6 (MCP)
6.1 - Particulars of MCP


### MCP
- `SSE connection` type is over https and uses streaming for responses. it is necessary when the MCP server you are connecting to is remote.
- `Stdio` is very common and is simple input and output, if the MCP server is local, this is prefered.

### Autogen Core Concepts
0.4 is a from the ground rewrite of 0.2 focused on agent visibility.

What is autogen core? an agent interaction framework agnostic of platforms or products. Similar to langgraph
where langraph can be used without langchain but often used together.

- Runtime agent: handles lifecycle and communication across process boundaries, consisting of:
    - Host service: connected to worker runtimes, handling message delivery and sessions for direct messages.
    - Worker runtime: advertises agents to host service and handles executing their code. 


### LangGraph Core Concepts
Agent workflows are represented as graphs (dependency chain sort of thing).
State - Represents the status of the entire application.
Nodes - a function represents an agentic logic, they receive the current state as input, do something and return updated state.
Edges - a python func that determines which node to be executed next based on the state.
Essentially, nodes do the work and edges choose what to do next.

LangGraph is more of a state flow manager using python functions, it does not necessarily have anything to do with AI
even though AI is a common use case.

### CrewAI Core Concepts
Agent - smallest autonomous unit of work, has a role, goal and a backstory lol - has memory and tools > little ai
Task - specific assignment to be carried out, with description, expected output and an agent attached to it.
Crew - aggregate of Agents & Tasks; can operate either in sequential mode or heriarchical mode.
> Bit more opinionated than agents sdk.
> Uses a yaml configuration! You create an agent based on a predefined config.
> Crewai projects cannot be done in notebooks because each crew setup is a whole uv project. `crewai create crew my_crew`


## Notes
2026-01-23
Created deep_research project, very cool, asks for clarifying questions, great that gradle can facilitate html. perhaps there is a POC in that for facilitating agent chat.

Introduces Crew AI - you should use CrewAI when you need autonomous problem solving, creative collaboration or exploratory tasks.

2026-01-22
Handoffs differ from tools in that they hand off the entire job to another AI if it realises another AI is better equipped for the job.
Do we have access to view traces if we are just using th chat gpt agent but interacting with our own MCP server?


2026-01-21
Bit confusing the difference between 2.2 & 2.3 was not super clear.
Once the lesson is done, rename it with a more descriptive name.

