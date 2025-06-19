import re

def count_words_in_book(word, book):
    count = 0
    # Use \b for whole words only...
    test_str = r'\b' + re.escape(word) + r'\b'
    for line in book:
        count += len(re.findall(test_str, line))
    return count

def query_words_in_book(words, book):
    results = {}
    for word in words:
        # Don't worry about double counting results.
        if (results.get(word)): continue
        results[word] = count_words_in_book(word, book)

    return results


book = [
    'lorum ipsum dicsum pipsum mipsum',
    'ligsum lorun dicusm pipsum sipsum',
    'super cool ligmas pigmas digmas',
    'christmas is smigmas ligmas ma digmas'
]

print(query_words_in_book(['lorum', 'lorum', 'cool', 'ligmas'], book))