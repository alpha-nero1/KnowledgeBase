import os
from typing import List, Tuple

from dotenv import load_dotenv
from openai import OpenAI


load_dotenv()

def get_system_prompt(target_language, language_level = 'A1', target_language_level = 'A2'):
    return f'''
# ROLE
Act as an expert linguistic coach and polyglot specializing in {target_language} acquisition for English speakers.

# CONTEXT
I am a {language_level} learner. My goal is to achieve {target_language_level} fluency. I want to focus on high-frequency, natural-sounding vocabulary that locals actually use, rather than textbook-style words.
    '''

def get_task(topic, target_language, word_type='mixed'):
    word_type_instruction = f'Focus exclusively on **{word_type}** (e.g. if verbs: only verbs, if nouns: only nouns).' if word_type.lower() != 'mixed' else 'Include a balanced mix of word types: verbs, nouns, adjectives, adverbs, and common phrases.'
    return f'''
# TASK
Please curate a list of 25 essential {word_type} related to {topic}. {word_type_instruction}

For each entry, you MUST provide:
- The word/phrase in {target_language}.
- A literal translation AND the natural English equivalent.
- Deep Context Example A: A long, descriptive sentence (15+ words) that sets a scene.
- Deep Context Example B: A long, descriptive sentence (15+ words) that shows the word in a different emotional or functional tone.
- A brief note on "Nuance" (e.g., is it formal? is it regional? is it easy to confuse with another word?).
- A simple mnemonic or memory trick to help me remember it.
- A short history of the word's origins.

# FORMAT
Present this as a Markdown table for easy reading. Below the table, write a 3-paragraph "mini-story" or dialogue that uses ALL the words from the list so I can see them in a connected context.
    ''';


class VocabAgent:
    def __init__(self, target_language, language_level, target_language_level) -> None:
        api_key = os.getenv("OPENAI_API_KEY")
        if not api_key:
            raise ValueError(
                "Missing OPENAI_API_KEY. Copy .env.example to .env and set your API key."
            )

        self.target_language = target_language
        self.word_type = 'mixed'
        self.model = os.getenv("OPENAI_MODEL", "gpt-4.1-mini")
        self.client = OpenAI(api_key=api_key)
        self.system_prompt = get_system_prompt(target_language, language_level, target_language_level)

    def update_settings(self, target_language: str, language_level: str, target_language_level: str, word_type: str = 'mixed') -> None:
        self.target_language = target_language
        self.word_type = word_type
        self.system_prompt = get_system_prompt(target_language, language_level, target_language_level)

    def task_chat(self, topic: str, history: List[Tuple[str, str]]) -> str:
        message = get_task(topic, self.target_language, self.word_type)
        input_messages = [{"role": "system", "content": self.system_prompt}]

        for user_text, assistant_text in history:
            input_messages.append({"role": "user", "content": user_text})
            input_messages.append({"role": "assistant", "content": assistant_text})

        input_messages.append({"role": "user", "content": message})

        response = self.client.responses.create(
            model=self.model,
            input=input_messages,
            temperature=0.7,
        )
        return response.output_text.strip()

    def chat(self, message: str, history: List[Tuple[str, str]]) -> str:
        input_messages = [{"role": "system", "content": self.system_prompt}]

        for user_text, assistant_text in history:
            input_messages.append({"role": "user", "content": user_text})
            input_messages.append({"role": "assistant", "content": assistant_text})

        input_messages.append({"role": "user", "content": message})

        response = self.client.responses.create(
            model=self.model,
            input=input_messages,
            temperature=0.7,
        )
        return response.output_text.strip()
