def get_interval_from_floors(floors):
  return 14

def drop_egg(egg, breaking_point):
  return egg < breaking_point

# Returns an array of [breaking point, drops needed]
def egg_drop(breaking_point, floors):
  interval = get_interval_from_floors(floors)
  print("Interval: ", interval)
  egg_one = interval
  egg_one_last_floor = 0
  drops = 0

  while (drop_egg(egg_one, breaking_point) and egg_one <= floors):
    # Egg one has not broken yet
    interval -= 1
    egg_one_last_floor = egg_one
    egg_one += interval
    print("Interval: ", interval, "Egg one: ", egg_one)
    drops += 1

  egg_two = egg_one_last_floor + 1

  while (drop_egg(egg_two, breaking_point) and egg_two < egg_one and egg_two < floors):
    # Egg two has not broken
    egg_two += 1
    drops += 1

  return [egg_two, drops]


print('egg_drop(30, 100) = ', egg_drop(30, 100))
print('egg_drop(47, 100) = ', egg_drop(47, 100))
print('egg_drop(98, 100) = ', egg_drop(98, 100))
print('egg_drop(29, 100) = ', egg_drop(29, 100))

