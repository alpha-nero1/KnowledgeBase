# Regular Exp lib.
import re

def increment_char_store_count(char, char_store):
  if char and char_store is not None:
    if char in char_store:
      char_store[char] = char_store[char] + 1
    else:
      char_store[char] = 1
  return char_store

def build_char_store(value):
  char_store = {}
  for char in value:
    is_valid_char = re.search('[a-zA-Z]', char)
    if (is_valid_char):
      char_store = increment_char_store_count(char, char_store)
  return char_store

def store_has_no_more_than_one_odd_count(char_store):
  already_found_odd_count = False
  if char_store is not None:
    for char in char_store:
      if (char_store[char] % 2 == 1):
        # Is odd
        if already_found_odd_count:
          return False
        already_found_odd_count = True
  return True

def is_permutation_of_palindrome(value):
  char_store = build_char_store(value)
  return store_has_no_more_than_one_odd_count(char_store)

print("is a permutation of a palindrome \"tact cao\"", is_permutation_of_palindrome("tact coa"))
print("is a permutation of a palindrome \"tactw cao\"", is_permutation_of_palindrome("tactw coa"))