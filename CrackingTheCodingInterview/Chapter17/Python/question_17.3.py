import random as rand

# Get unique m values in bounds of 0 to arr_length,
# if we had the same index duplicated we just try again recursively.
def get_unique_random(arr_length, m):
    # Just for bad input use case.
    if (m > arr_length): arr_length = m
    random_indexes = set()
    for n in range(0, m):
        random_val = int(rand.random() * arr_length)
        random_indexes.add(random_val)
        if (n == len(random_indexes)): return get_unique_random(arr_length, m)

    return random_indexes

# Pick a random m elements from an array of size n
def random_m_from_arr_n(arr, m):
    ret_arr = []
    for rand_index in get_unique_random(len(arr), m):
        ret_arr.append(arr[rand_index])
    return ret_arr

array = [0, 1, 2, 3, 5, 2, 11, 22, 2, 8, 9, 1, 109, 200, 304]
print(random_m_from_arr_n(array, 3))