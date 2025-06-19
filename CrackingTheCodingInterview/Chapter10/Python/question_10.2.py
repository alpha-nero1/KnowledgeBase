def bucket_sort(arr):
    # Using a hashtable to "bucket" sort.
    map_list = {}
    all_keys = []
    bucket_sorted_arr = []

    for str in arr:
        key = ''.join(sorted(str))
        if map_list.get(key) == None:
            all_keys.append(key)
            map_list[key] = []
        map_list[key].append(str)

    for key in all_keys:
        key_list_strs = map_list.get(key)
        if (key_list_strs == None): continue
        for bucket_str in key_list_strs:
            bucket_sorted_arr.append(bucket_str)

    return bucket_sorted_arr


arr = ['cat', 'mat', 'tah', 'tam', 'act', 'amt', 'hat']

print('aa sorting: ', arr)
print('aa sorted: ', bucket_sort(arr))



    