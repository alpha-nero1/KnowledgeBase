from jigsaw import Jigsaw

jigsaw = Jigsaw(3, 3)

print(jigsaw)

piece_one = jigsaw.grid[0][0]
piece_two = jigsaw.grid[1][0]
piece_three = jigsaw.grid[2][0]
piece_four = jigsaw.grid[2][1]

print(jigsaw.fits_with(piece_one.get_right_edge(), piece_two.get_left_edge()))
print(jigsaw.fits_with(piece_three.get_bottom_edge(), piece_four.get_top_edge()))