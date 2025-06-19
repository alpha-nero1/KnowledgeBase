def get_higher(num):
  tmp = num
  zeros_after_p = 0
  ones_after_p = 0

  # 100111(00)
  while ((tmp & 1) == 0 and tmp != 0):
    zeros_after_p += 1
    tmp >>= 1
  
  # 100(111)00
  while ((tmp & 1) == 1 and tmp != 0):
    ones_after_p += 1
    tmp >>= 1

  # 10(0)11100
  p = ones_after_p + zeros_after_p

  if (p == 31 or p == 0):
    return -1

  # Turn p bit to 1 to make value greater.
  num |= (1 << p)