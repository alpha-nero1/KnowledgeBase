"""
    17.19. Missing Two.
"""

from cmath import sqrt

def missing_two(*arr):
    max_value = len(arr) + 2
    rem_square = square_sum_to_n(max_value, 2)
    rem_one = max_value * (max_value + 1) / 2

    for i in range(0, len(arr)):
        rem_square -= i * i
        rem_one -= i

    return solve_equation(int(rem_one), int(rem_square))

def square_sum_to_n(n, power):
    sm = 0
    for i in range(1, n + 1):
        sm += int(pow(i, power))

    return sm

# Use the quadratic formula to solve.
def solve_equation(r_one, r_two):
    # ax^2 + bx + c
    # x = [-b +- sqrt(b^2, 4ac)] / 2a
    # In this case it has to be a + not a - * /
    a = 2
    b = -2 * r_one
    c = r_one * r_one - r_two

    part_one = -1 * b
    part_two = sqrt(b * b - 4 * a * c).real
    part_three = 2 * a

    solution_x = ((part_one + part_two) / part_three)
    solution_y = r_one - solution_x

    return (solution_x, solution_y)

print(missing_two(1, 2, 3, 4, 5, 7, 9, 10))
