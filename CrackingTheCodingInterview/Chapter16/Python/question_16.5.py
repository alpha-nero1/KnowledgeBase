"""
    16.5. Factorial Zeros
"""

# Because we need only count the multiples of 5.
def count_factorial_trailing_zeros(num):
    count = 0
    if (num < 0): return -1
    # Start from 5
    i = 5
    # Check that the number divided by 5 is still greater than 0
    while ((num / i) > 0):
        count += int(num / i)
        # Increment by multiplying by 5 again.
        i = int(i * 5)

    return count

print(count_factorial_trailing_zeros(19))