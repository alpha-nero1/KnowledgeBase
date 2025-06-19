
def binary_search_strs(strings, str_val, first, last):
    if (first > last): return -1
    # Move mid to the middle.
    mid = int((first + last) / 2)

    # If mid is empty, find closest to empty string.
    if (strings[mid] == ""):
        left = mid - 1
        right = mid + 1
        # Pretty cheeky while loop algo.
        while (True):
            # Non-sensical case, return -1, we cannot find the element.
            if (left > right or right > last): return -1
            # Else if the current right is not empty, use that as mid.
            elif (right <= last and strings[right] != ""):
                mid = right
                break # Break the loop, we found the mid point.
            # Else use the eventual left.
            elif (left >= first and strings[left] != ""):
                mid = left
                break # Break the loop, we found the mid point.
            right += 1
            left -= 1

    # Check for string, recurse if needed.
    if (strings[mid] == str_val): return mid
    if (strings[mid] < 0): return binary_search_strs(strings, str_val, mid, right)
    return binary_search_strs(strings, str_val, first, mid);


def search_strings(strings, str_val):
    if (strings == None or str_val == None or str_val == ""): return -1;
