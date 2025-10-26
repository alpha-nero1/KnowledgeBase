# Is unique function, does not use extra datastructures.
def is_unique(value):
  if (value and len(value)):
    i = 0
    for item in value:
      for j in range(i + 1, len(value)):
        if (value[j] == item):
          return False
      i = i + 1
  return True

# Is unique function, uses hashtable.
def is_unique_with_hashtable(value):
  if (value and len(value)):
    str_store = { }
    for item in value:
      stringItem = str(item)
      if (item and item in str_store):
        return False
      str_store[stringItem] = True
  return True

print('is unqiue 123456a', is_unique('123456a'))
print('is unqiue with hashtable 123456a', is_unique_with_hashtable('123456a'))