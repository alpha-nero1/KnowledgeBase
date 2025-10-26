"""
    17.10. Majority Element
"""
import sys

"""
Example iteration output:
    #### IT START #####
    n 1, majority 0, count 0

    #### IT START #####
    n 1, majority 1, count 1

    #### IT START #####
    n 1, majority 1, count 2

    #### IT START #####
    n 1, majority 1, count 3

    #### IT START #####
    n 1, majority 1, count 4

    #### IT START #####
    n 1, majority 1, count 5

    #### IT START #####
    n 1, majority 1, count 6

    #### IT START #####
    n 1, majority 1, count 7

    #### IT START #####
    n 2, majority 1, count 8

    #### IT START #####
    n 2, majority 1, count 7

    #### IT START #####
    n 3, majority 1, count 6

    #### IT START #####
    n 3, majority 1, count 5

    #### IT START #####
    n 3, majority 1, count 4

    #### IT START #####
    n 3, majority 1, count 3

    #### IT START #####
    n 3, majority 1, count 2
"""


"""
    Entry point function, get the majority element.
"""
def find_majority_element(arr):
    candidate = get_candidate(arr)
    return candidate if validate(arr, candidate) else -1

"""
    Get the most populous element in the array.
"""
def get_candidate(arr):
    majority = 0
    count = 0

    # Really clever way of getting the most populous array element!
    for n in arr:
        print('#### IT START #####')
        print(f'n {n}, majority {majority}, count {count}')
        print()
        # If the count has balanaced back down to 0 then reassign majority.
        if (count == 0): majority = n
        # If this number is the majority number then increment the count, else decrement.
        if (n == majority):
            count += 1
            continue

        count -= 1

    return majority

"""
    Validate that the most populous element is a majority element.
"""
def validate(arr, majority):
    count = 0

    for n in arr:
        if (n == majority): count += 1

    return count > (len(arr) / 2)


print(find_majority_element(sys.argv[1:]))