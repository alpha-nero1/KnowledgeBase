import math

def decimal_to_binary(value):
  if value > 1 or value < 0:
    print("ERROR invalid dec")
    return
  # Passed our checks, run logic.
  count = 0
  binary_res = ''
  total = int(value * 100)
  max = 32
  first_total_sub_made = False
  while count <= max:
    this_binary = pow(2, max - count)
    if (this_binary <= total):
      binary_res = binary_res + "1"
      total -= this_binary
      first_total_sub_made = True
    elif first_total_sub_made:
      binary_res = binary_res + "0"
    count += 1
  if (total > 0):
    # Then we know the number did not get finished!
    print("ERROR to large")
    return
  return binary_res

print("decimal_to_binary 0.72", decimal_to_binary(0.72))
print("decimal_to_binary 0.67", decimal_to_binary(0.67))
print("decimal_to_binary 0.95", decimal_to_binary(0.95))
print("decimal_to_binary 0.94", decimal_to_binary(0.94))
