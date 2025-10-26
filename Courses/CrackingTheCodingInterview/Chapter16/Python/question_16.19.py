"""
    16.19. Pond counter
"""

def count_pond(counted_dict, area, row, col, total_rows, total_cols):
    count = 0
    if (
        counted_dict.get(f'{row},{col}')
        or row >= total_rows
        or row < 0
        or col >= total_cols
        or col < 0
        or area[row][col] != 0
    ): return count

    counted_dict[f'{row},{col}'] = True
    count = 1

    return count + (
        # Up-down, side-to-sides
        count_pond(counted_dict, area, row, col + 1, total_rows, total_cols)
        + count_pond(counted_dict, area, row, col - 1, total_rows, total_cols)
        + count_pond(counted_dict, area, row + 1, col, total_rows, total_cols)
        + count_pond(counted_dict, area, row - 1, col, total_rows, total_cols)
        # Diagonals
        + count_pond(counted_dict, area, row - 1, col - 1, total_rows, total_cols)
        + count_pond(counted_dict, area, row + 1, col + 1, total_rows, total_cols)
        + count_pond(counted_dict, area, row - 1, col + 1, total_rows, total_cols)
        + count_pond(counted_dict, area, row + 1, col - 1, total_rows, total_cols)
    )

def pond_counter(area):
    total_elements = len(area) * len(area[0])
    total_rows = len(area)
    total_cols = len(area[0])
    counted_dict = {}
    pond_counts = {}
    # Row cursor.
    row = -1
    for i in range(0, total_elements):
        col = i % len(area[0])
        if (col == 0): row += 1;
        el = area[row][col]
        if (el != 0 or counted_dict.get(f'{row},{col}')): continue

        # Hey we have a 0 that is not part of any pond, lets record it, and count how large the pond is!
        pond_counts[f'{row},{col}'] = count_pond(counted_dict, area, row, col, total_rows, total_cols)

    return pond_counts        


print(pond_counter([
    [0, 2, 1, 0],
    [0, 1, 0, 1],
    [1, 1, 0, 1],
    [0, 1, 0, 1],
]))