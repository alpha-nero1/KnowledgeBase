# The chance of making a shot is 'p' and it is 0-1 value.
# Therefor the chance of not making a shot is '1 - p'.

# In probability the OR chance = +
# But when finding the chance of something happening, all vars = *

# p: probability of making a shot.
def probability_of_making_shots(p, min_shots, total_shots):
  '''
  P(making1 and 2, and missing3)
    + P(making1 and 3, and missing2)
    + P(missing1, and making2and 3)
    p* p*(1-)p + p*(1-)p * p+(1-)p * p* p 3(1-p)p2
  '''
  '''
  Adding these together, we get:
    p3 + 3(1 - p)p2
    p3 + 3 p2 - 3 p3 3p2 - 2p3
  '''
  chance_of_missing = 1 - p
  chance_of_making_min_shots = pow(p, min_shots)
  return total_shots * chance_of_missing * chance_of_making_min_shots;

def get_chances_of_winning_game_one_and_two(p):
  if p == 1:
    return [1, 1]

  second_gets_all = pow(p, 3);
  second_gets_some = probability_of_making_shots(p, 2, 3)
  # Add the chance of getting 3/3 and 2/3
  total_second_chance = second_gets_all + second_gets_some
  return [p, total_second_chance]

print("0.4", get_chances_of_winning_game_one_and_two(0.4))
print("0.5", get_chances_of_winning_game_one_and_two(0.5))
print("0.6", get_chances_of_winning_game_one_and_two(0.6))
print("0.75", get_chances_of_winning_game_one_and_two(0.75))
print("0.8", get_chances_of_winning_game_one_and_two(0.8))