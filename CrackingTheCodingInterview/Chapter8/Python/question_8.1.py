# Whats cool about this solution is that as each call splits into three,
# we negate the possibility of any duplicate calculations that may happen
# in any branching off.

# Total steps and the memoization hashtable (or in this case array).
def calculate_steps(total, mem_array):
    if (total < 0): return 0
    if (total == 0): return 1
    # Cache and return as we go.
    if (mem_array[total] > -1): return mem_array[total]
    mem_array[total] = (
        calculate_steps(total - 1, mem_array) +
        calculate_steps(total - 2, mem_array) +
        calculate_steps(total - 3, mem_array)
    )
    return mem_array[total]


def calculate_all_steps(total):
    memoization_array = [-1] * (total + 1)
    return calculate_steps(total, memoization_array)


total_steps = calculate_all_steps(100)

print('total steps: ', total_steps)