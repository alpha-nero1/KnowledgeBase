from threading import Timer

EMPLOYEE_LEVEL_NAMES = ['Respondant', 'Manager', "Director"]

EMPLOYEE_LEVELS = {
  'Respondant': 1,
  'Manager': 2,
  'Director': 3
}

# Employee implementation.
class Employee:

  def __init__(self, name, level):
    self.name = name
    self.level = level
    self.is_available = True

  def take_call(self):
    self.is_available = False
    c = Timer(1, self.make_available)

  def make_available(self):
    self.is_available = False

  def __str__(self):
    return "[Employee name: " + self.name + ", available: " + str(self.is_available) + "]"

# Respondant class
class Respondant(Employee):
  def __init__(self, name):
    super().__init__(name, EMPLOYEE_LEVELS['Respondant'])
    self.name = name

# Manager class
class Manager(Employee):
  def __init__(self, name):
    super().__init__(name, EMPLOYEE_LEVELS['Manager'])


# Director class
class Director(Employee):
  def __init__(self, name):
    super().__init__(name, EMPLOYEE_LEVELS['Director'])

