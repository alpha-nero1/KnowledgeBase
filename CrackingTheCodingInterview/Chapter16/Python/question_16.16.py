"""
    16.16. Sub Sort: Given an array of integers, write a method to find indices m and n such that if you sorted 
    elements m through n, the entire array would be sorted. Minimize n - m (that is, find the smallest 
    such sequence).
"""

def find_mn_indices(arr):
    start = None
    end = None
    sorted_arr = sorted(arr)
    i = 0
    for num in arr:
        inverse_index = (len(arr) - 1) - i
        inverse_num = arr[inverse_index]
        if (start is None and num != sorted_arr[i]): start = i
        if (end is None and inverse_num != sorted_arr[inverse_index]): end = inverse_index
        i += 1
    
    return (start, end)
        
print(find_mn_indices([1, 2, 4, 7, 10, 11, 7, 12, 6, 7, 16, 18, 19]))