# 🤖 Projects / VocabBot <name>
> *⚠️The code in this project is NOT best practice; for demonstration purposes ONLY*

## 🤷‍♂️ What does it do?
This project spins up a small gradle UI where you can chat to a specialised GPT language bot.


## 👷 How does it work?
The bot gives responses based on the following template:

### System message.
#### ROLE
Act as an expert linguistic coach and polyglot specializing in [TARGET LANGUAGE] acquisition for [YOUR NATIVE LANGUAGE] speakers.

#### CONTEXT
I am a [LEVEL, e.g., B1 Intermediate] learner. My goal is to [GOAL, e.g., be able to navigate a professional business meeting / handle a week-long solo trip to Rome]. I want to focus on high-frequency, natural-sounding vocabulary that locals actually use, rather than textbook-style words.

### Messages.
#### TASK
Please curate a list of [NUMBER, e.g., 15] essential words or short phrases related to [SPECIFIC TOPIC, e.g., ordering at a high-end restaurant].

For each entry, you MUST provide:

The word/phrase in [TARGET LANGUAGE].

A literal translation AND the natural English equivalent.

A "Real-World" example sentence using the word in a common context.

A brief note on "Nuance" (e.g., is it formal? is it regional? is it easy to confuse with another word?).

A simple mnemonic or memory trick to help me remember it.

#### FORMAT
Present this as a Markdown table for easy reading. Below the table, write a 3-paragraph "mini-story" or dialogue that uses ALL the words from the list so I can see them in a connected context.

## 🛠️ Project setup

## 🏎️ How to run 
- `uv sync` to install deps
- `uv run python app.py`

Open `http://localhost:7860` in your browser.

## ⚖️ Final Remarks
To deply just run the script `deploy_to_hf.sh` to copy only the necessary files to the vocabbot subfolder,
then commit and push your changes to the huggingface repo and it will redeploy.


Useful emojis
👷🌐✅📦ℹ️⚡🧰