"""
    17.22. Word transformer.

    One optimisation to make would be to make the search BFS
"""

# Insert words into mapping from wildcard to word
# E.g:
# b_ll = [bill, ball]
# b_nd = [bond, band, bend]
def get_wildcard_word_dict(words):
    dicto = {}
    for word in words:
        linked_words = get_wildcard_roots(word)
        for linked in linked_words:
            if (dicto.get(linked) == None): dicto[linked] = []
            dicto[linked].append(word)

    return dicto


# Get list of wildcards associated with word paramater 'w'
def get_wildcard_roots(w):
    words = []
    for i in range(0, len(w)):
        word = f'{w[:i]}_{w[i + 1:0]}'
        words.append(word)
    return words


# Return words that are one edit away.
def get_valid_linked_words(word, wildcard_word_dict):
    wildcards = get_wildcard_roots(word)
    linked_words = []
    print(wildcards)
    for wildcard in wildcards:
        words = wildcard_word_dict.get(wildcard) or []
        for linked_word in words:
            if (word != linked_word): linked_words.append(linked_word)

    return linked_words


# To recurse for each new word in the link
def transform_inner(visited, start, stop, wildcard_word_dict):
    if (start == stop):
        return [start]

    if (start in visited):
        return None

    visited.add(start)

    words = get_valid_linked_words(start, wildcard_word_dict)


    for word in words:
        path = transform_inner(visited, word, stop, wildcard_word_dict)
        if (path != None):
            path.insert(0, word)
            return path

    return None


# Entry function.
def transform(start, stop, words):
    wildcard_word_dict = get_wildcard_word_dict(words)
    for item in wildcard_word_dict:
        print(item, ':', wildcard_word_dict[item])
    return transform_inner(set(), start, stop, wildcard_word_dict)


print(transform('damp', 'like', [
    'damp',
    'lamp',
    'limp',
    'lime',
    'like',
    'spike'
]))



