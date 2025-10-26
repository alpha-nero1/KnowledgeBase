"""
    17.25. Word Rectangle
"""

# Our dictionary of words input.
words_input = [
    'hello',
    'helix',
    'aspro',
    'trees',
    'smoke',
    'cigar',
    'purse',
    'hairs'
]

_end = '_end_'


# Simple helper function that makes a trie in only several lines of code.
def make_trie(words):
    root = dict()
    for word in words:
        current_dict = root
        for letter in word:
            current_dict = current_dict.setdefault(letter, {})
        current_dict[_end] = _end
    return root


# See if the trie contains the given word.
def trie_has(trie, word):
    dicto = dict()
    for char in word:
        dicto = trie.get(char)
        if (dicto == None): return False
    return True


# Rectangle class for the algorithm.
class Rectangle():
    height = None
    length = None
    matrix = []

    def __init__(self, length):
        self.length = length
        self.height = 0

    @staticmethod
    def from_letters(length, letters):
        rect = Rectangle(length)
        rect.height = len(letters)
        rect.matrix = letters
        return rect

    def get_letter(self, i, j): return self.matrix[i][j]

    def get_column_word(self, col):
        word = ''
        for row in self.matrix:
            word += row[col]
        return word

    def is_complete(self, l, h, word_group):
        if (self.height == h):
            # Check each solumn is a word in the dict.
            for i in range(0, l):
                col_word = word_group.get_column_word(i)
                if (not word_group.contains_word(col_word)): return False

            return True
        return False

    def is_partial_ok(self, l, trie):
        if (self.height == 0): return True
        for i in range(0, l):
            col = self.get_column_word(i)
            if (not trie_has(trie, col)): return False
        return True


# Word group helper class.
class WordGroup():
    lookup = set()
    group = []

    @staticmethod
    def create_word_groups(lst):
        group_list = []
        max_len = 0

        for word in lst:
            if (len(word) > max_len): max_len = len(word)

        for _ in range(0, max_len):
            group_list.append(WordGroup())

        for word in lst:
            index = len(word) - 1
            group_list[index].add(word)

        return group_list

    def contains_word(self, word): return self.lookup.has(word)

    def add_word(self, word):
        self.group.append(word)
        self.lookup.add(word)


# Makes a a partial rectangle.
def make_partial_rectangle(l, h, rect, group_list, trie_list):
    if (rect.height == h):
        if (rect.is_complete(l, h, group_list[h - 1])):
            return rect
        return None

    if (not rect.is_partial_ok(l, trie_list[h - 1])):
        return None

    for i in range(0, len(group_list[i - 1])):
        org_plus = rect.append(group_list[i - 1].words[i])

        rect = make_partial_rectangle(l, h, org_plus)
        if (rect != None): return rect
    return None


# Makes a rectangle
def make_rectangle(length, height, group_list, trie_list):
    if (group_list[length - 1] == None or group_list[height - 1] == None): return None

    # Create a trie for word length
    if (trie_list[height - 1] == None):
        words = group_list[height - 1].group
        trie_list[height - 1] = make_trie(words)

    return make_partial_rectangle(length, height, Rectangle(length), group_list, trie_list)


# Entry function.
def max_rectangle(words):
    sorted_words = sorted(words)
    longest_word = sorted_words[1:]
    length_longest = len(longest_word)
    group_list = WordGroup.create_word_groups(words)
    trie_list = [None * length_longest]

    for z in range(length_longest, 0, -1):
        for i in range(1, length_longest):
            if (z % i == 0):
                j = z / i
                if (j <= length_longest):
                    # Create a rectangle of length i and height j, i * j = z
                    rectangle = make_rectangle(i, j, group_list, trie_list)
                    if (rectangle != None): return rectangle

    return None


max_rectangle(words_input)