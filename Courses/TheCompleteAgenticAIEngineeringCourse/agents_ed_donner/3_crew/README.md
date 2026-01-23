# Crew
Quite cool for chaining together agents and tools to get a complex job done.

## How it works
- `main.py` is the entrypoint for running the crew
- `crew.py` is where the crew is defined based on your config/yml files.

- Create a new crew with `uv tool install crewai` & `crewair create crew <crewname>`
- Then when the crew is ready run `cd <crewname> && crewai run`