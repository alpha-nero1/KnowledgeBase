def insert_char_at(word, char, i):
    return word[i:] + char + word[:i]


def get_unique_permutations(str, perms_dict = {}):
    if (str == None): return None

    perms = []
    # The algo won't work without this, because when we hit the bottom of the stack
    # the array let's the logic at the bottom bubble to the top.
    if (len(str) == 0):
        perms.append('') # Base case.
        return perms

    # Get the first char.
    first = str[0]
    # Get the remainder of the string.
    remainder = str[1:]
    
    other_perms = get_unique_permutations(remainder, perms_dict)
   
    for word in other_perms:
        # For each word returned by the remainder perms
        # loop the range of the word and add to the perms
        # an insertion at i using our original first char.
        for i in range(0, len(word) + 1):
            perm = insert_char_at(word, first, i)
            # Only insert if the perm has not been generated before. (aspect of 8.8)
            if (perms_dict.get(perm) == None): 
                perms_dict[perm] = perm
                perms.append(perm)

    return perms


print('all perms of cat = \n', get_unique_permutations('cat'))
print('all perms of catfish = \n', get_unique_permutations('catfish'))
print('all perms of aaaaaaa = \n', get_unique_permutations('aaaaaaa'))
print('all perms of paaaaan = \n', get_unique_permutations('paaaaan'))