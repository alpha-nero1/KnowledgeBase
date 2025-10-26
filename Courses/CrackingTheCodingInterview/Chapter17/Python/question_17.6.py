def count_twos_at_range_in_digit(num, digit_index):
    print('\naa ^', num, digit_index)
    # E.g. 1, 10, 100
    power_of_ten = int(pow(10, digit_index))
    # E.g. 10, 100, 1000
    next_power_of_ten = int(power_of_ten * 10)
    # This will be what digit value was on the right of the current digit index.
    # Is always a value less than 10.
    right = int(num % power_of_ten)

    round_down = int(num - num % next_power_of_ten)
    round_up = int(round_down + next_power_of_ten)

    digit = int((num / power_of_ten) % 10)

    print('aa - power_of_ten', power_of_ten)
    print('aa - next_power_of_ten', next_power_of_ten)
    print('aa - right', right)
    print('aa - round_down', round_down)
    print('aa - round_up', round_up)
    print('aa - digit', digit)
    if (digit < 2):
        # If the digit is less than two then the count of 2s in this digit index is the round down / 10
        print('aa + less than 2')
        return (round_down / 10)
    if (digit == 2):
        # Else if this digit is a two then the count of 2s is round down / 10 + this 2 + 
        print('aa + digit is two')
        return (round_down / 10) + right + 1
    print('aa + digit > 2')
    return round_up / 10


def count_twos_in_range(num):
    count = 0
    num_len = len((f'{num}'))
    for digit in range(0, num_len):
        count += count_twos_at_range_in_digit(num, digit)
    return count

print(count_twos_in_range(21))
print(count_twos_in_range(211))
print(count_twos_in_range(312))