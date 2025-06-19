# Given two strings, write a method to decide if string a is a permutation of string s.

def build_char_count_store(value):
  char_count_store = {}
  for char in value:
    if (char in char_count_store):
      # Increment the count
      char_count_store[char] = char_count_store[char] + 1
    else:
      # Set initial.
      char_count_store[char] = 1
  return char_count_store

def check_is_permutation(first_value, second_value):
  if (first_value and second_value):
    if (len(first_value) == len(second_value)):
      # Okay now we know we need the char stores.
      first_char_store = build_char_count_store(first_value)
      second_char_store = build_char_count_store(second_value)
      for char in first_char_store:
        if (first_char_store[char] != second_char_store[char]):
          return False
      return True
    return False
  return False

print('check_is_permutation 123 321', check_is_permutation('123', '321'))
print('check_is_permutation 1233 321', check_is_permutation('1233', '321'))
print('check_is_permutation 1233 321a', check_is_permutation('1233', '321a'))
print('check_is_permutation dog god', check_is_permutation('dog', 'god'))