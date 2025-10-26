# Draw horizontal line from (x1, y) to (x2, y)
def draw_line(screen, width, x1, x2, y):
  bytes_num = int(width / 8) # Because the screen is divisible exactly by 8.
  x1_pos_in_byte = x1 % 8
  x1_first_full_byte = x1 / 8
  if (x1_pos_in_byte != 0):
    x1_first_full_byte += 1
  x2_pos_in_byte = x2 % 8
  x2_first_full_byte = x2 / 8
  if (x2_pos_in_byte != 7):
    x2_first_full_byte -= 1

  # Set the full bytes.
  for byte in range(int(x1_first_full_byte), int(x2_first_full_byte)):
    screen[(bytes_num * y + byte)] = 0xFF # Set to all ones.

  x1_mask = (0xFF >> x1_pos_in_byte)
  x2_mask = (0xFF << (x2_pos_in_byte - 1))

  # x1 and x2 are same byte!
  if (x1_first_full_byte == x2_first_full_byte):
    byte_mask = x1_mask & x2_mask
    screen[bytes_num * y + x1_first_full_byte] |= byte_mask
  else:
    # Check if pos not the first because its drawn all ones anyway.
    if (x1_pos_in_byte != 0):
      byte_in_screen = int(bytes_num * y + x1_first_full_byte - 1)
      screen[byte_in_screen] |= x1_mask
    # Check if pos 2 not 7 because its drawn all ones anyway also.
    if (x2_pos_in_byte != 7):
      byte_in_screen = int(bytes_num * y + x2_first_full_byte + 1)
      screen[byte_in_screen] |= x2_mask
  return screen

screen = draw_line(
  [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00],
  16,
  4,
  13,
  1
)

print('draw_line = ')
for by in screen:
  print(f"{by:b}")
