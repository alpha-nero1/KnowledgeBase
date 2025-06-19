"""
    17.13. Re-Space

    (Since Python will ignore string literals that are not assigned to a variable, you can add a multiline string (triple quotes) in your code, and place your comment inside it)
"""

import re

document = (
    """
    hellotheredrspaceyhowareyoutoday.doyouseeanycloudsin
    theskyorisitallbluesky.alsoareyouhungryforlunch
    today?ihearthatnuggetsareonspecial
    """
)

def re_space(doc):
    special_chars = [',', '.', '!', '?']
    dictionary = [
        'hello', 'there', 'dr', 'how', 'are', 'you', 'do', 'clouds',
        'see', 'in', 'also', 'panino', 'lunch', 'special', 'today', 'hear', 'getting',
        'they', 'on', 'for', 'sky', 'any', 'or', 'it', 'blue', 'all', 'is', 'nuggets', 'that',
        'hungry'
    ]
    # This will ensure that larger words are tackled first and words like 'in' or 'i'
    # break the larger words in the document!
    dictionary = sorted(dictionary, key=len, reverse=True)

    # list concatenation!
    full_dictionary = special_chars + dictionary
    doc = doc.strip()

    for word in full_dictionary:
        special_word = word == '.' or word == '?'
        transformed_word = re.escape(word) if special_word else word
        # Perform a lookbehind to not pick up the char in replacement
        reg = '(?<=[a-z])(' + transformed_word + ')'
        rep = (word + ' ') if special_word else (' ' + word)
        doc = re.sub(r'' + reg, rep, doc, flags=re.MULTILINE)

    return doc


print(re_space(document))