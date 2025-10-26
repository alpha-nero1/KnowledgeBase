def rshift(val, n): return val>>n if val >= 0 else (val+0x100000000)>>n

def find_longest_seq(value):
  if ((~value == 0)): return len(value.to_bytes(2, 'big'))
  maxLen = 0
  currLen = 0
  prevLen = 0

  while (value > 0):
    if value & 1 == 1: # Found 1 bit
      currLen += 1
    elif value & 1 == 0: # Found break
      prevLen = currLen if value & 2 != 0 else 0
      currLen = 0
    # RE: we only add the + 1 here because the question denotes we get to fip one bit to make a longer sequence!
    maxLen = max(currLen + prevLen + 1, maxLen)
    value = rshift(value, 1)
  return maxLen


print(find_longest_seq(15))
print(find_longest_seq(63))