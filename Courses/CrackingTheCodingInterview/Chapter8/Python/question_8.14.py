str_to_bool = lambda st : True if st == '1' else False

# Look I wish I knew what was going on here but I don't and I am just moving on..
def count_eval(expr, bool_res):
    parenthesised = ''
    if (len(expr) == 0): return 0
    if (len(expr) == 1): return 1 if str_to_bool(expr) == bool_res else 0

    ways = 0
    i = 1
    while (i < len(expr)):
        char = expr[i]
        left = expr[0:i]
        right = expr[(i + 1):len(expr)]

        left_true = count_eval(left, True)
        left_false = count_eval(left, False)
        right_true = count_eval(right, True)
        right_false = count_eval(right, False)

        total = (left_true + left_false) * (right_true + right_false)
        total_true = 0

        if (char == '^'): total_true = left_true * right_false + left_false * right_true
        elif (char == '&'): total_true = left_true * right_true
        elif (char == '|'): total_true = left_true * right_true + left_false * right_true + left_true * right_false

        sub_ways = total_true if bool_res else total - total_true
        ways += sub_ways
        i += 2

    return parenthesised

print(count_eval('0^0&0^1|1'))