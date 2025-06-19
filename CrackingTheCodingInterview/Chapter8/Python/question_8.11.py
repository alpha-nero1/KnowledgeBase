def run_make_change(amount, denoms, index, map):
    # If we have already calculated the amount for this index just return it...
    if (map[amount][index] > 0): return map[amount][index]
    if (index >= len(denoms) - 1): return 1 # One denomination remaining...
    denom_amount = denoms[index] # 25 or 10 or ...
    ways = 0
    i = 0
    # while current add-up of denominations is less than the amount.
    while ((i * denom_amount) <= amount):
        # what is left is the amount - the new amount...
        amount_remaining = amount - (i * denom_amount)
        # Let's check this amount mixed with the next denomination index...
        ways += run_make_change(amount_remaining, denoms, index + 1, map)
        i += 1
    map[amount][index] = ways
    return ways

def run_make_change_myway(
    current_amount,
    denoms,
    current_denom_index,
    map
):
    print('hello', ' ' * current_denom_index, current_amount)
    if (map[current_amount][current_denom_index]): return map[current_amount][current_denom_index]
    # We have reached the end of the denominations so there can only be one way left...
    if (current_denom_index >= len(denoms) - 1): return 1
    ways = 0
    denom_amount = denoms[current_denom_index]
    i = 0

    # E.g. 25, 50, 75, 100
    while ((i * denom_amount) <= current_amount):
        # What has the recursive call got to work with.
        subtracted_amount = current_amount - (i * denom_amount)
        ways += run_make_change_myway(subtracted_amount, denoms, current_denom_index + 1, map)
        i += 1

    map[current_amount][current_denom_index] = ways
    return ways

def make_change(amount):
    # Define the denominations we want to use (25c, 10c e.t.c..)
    denominations = [25, 10, 5, 1]
    # Boostrap an array with positions already in it...
    running_map = [[None] * len(denominations)] * (amount + 1)
    return run_make_change_myway(amount, denominations, 0, running_map)

print('make_change(50) = ', make_change(50))
