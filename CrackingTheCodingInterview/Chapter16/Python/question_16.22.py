"""
    16.22. Langton's Ant
"""

from random import random
from math import ceil

dirs = [
    [-1, 0], # Up
    [0, 1], # Right
    [1, 0], # Down
    [0, -1] # Left
]

def get_square():
    return 1 if random() > 0.5 else 0


def grid_init(k):
    grd = []
    grd_len = k * 2 if k % 2 != 0 else (k * 2) + 1
    for i in range(0, grd_len):
        grd.append([])
        for _ in range(0, grd_len):
            grd[i].append(get_square())

    return grd


def print_grid(grid):
    for row in grid:
        print(row)


def new_dir(dir_index, clockwise):
    # I know I could have done this using modulus % but just had a blank
    new_index = dir_index + 1 if clockwise else dir_index - 1
    if (new_index > 3): new_index = 0
    if (new_index < 0): new_index = 3
    return new_index


def move_ant(k_val, row, col, grid, dir_index):
    if (k_val == 0): return grid # Eol
    current = grid[row][col]
    # Flip the colour.
    grid[row][col] = 1 if current == 0 else 0
    new_dir_index = new_dir(dir_index, current == 1)
    new_direction = dirs[new_dir_index]
    return move_ant(k_val - 1, row + new_direction[0], col + new_direction[1], grid, new_dir_index)


def print_k_moves(k):
    grid = grid_init(k)
    print('Grid before: ')
    print_grid(grid)
    middle_index = ceil(len(grid) / 2)
    grid = move_ant(k, middle_index, middle_index, grid, 1)
    print('Grid after: ')
    print_grid(grid)


print_k_moves(10);