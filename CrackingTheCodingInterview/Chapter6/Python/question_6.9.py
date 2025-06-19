def build_perfect_squares(num):
  perfect_squares = []
  running_total = 0
  i = 1
  while running_total < num:
    running_total = i * i
    if (running_total <= num):
      perfect_squares.append(i * i)
    i += 1
  return perfect_squares

def lockers(num):
  perfect_squares = build_perfect_squares(num)
  return len(perfect_squares);

print(lockers(100));