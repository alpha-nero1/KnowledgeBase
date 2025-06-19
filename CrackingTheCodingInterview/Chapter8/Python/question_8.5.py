# Recursive multipy using addition. Love this one!
def multiply(a, b):
    return recursive_multiply(0, a, b)

def recursive_multiply(total, next_val, additions_left):
    if (additions_left == 0): return total
    total += next_val;
    return recursive_multiply(total, next_val, additions_left - 1)

print('5 x 5 recursive_multiply = ', multiply(5, 5))
print('7 x 8 recursive_multiply = ', multiply(8, 7))
print('12 x 12 recursive_multiply = ', multiply(12, 12))
print('12 x 43 recursive_multiply = ', multiply(12, 43))
print('1 x 7 recursive_multiply = ', multiply(1, 7))
print('9 x 7 recursive_multiply = ', multiply(9, 7))
