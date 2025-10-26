import types
import datetime
from linked_list import LinkedList

class Animal:
  def __init__(self, type, name, id):
    self.type = type
    self.name = name
    self.id = id

  def __str__(self):
    return self.name + " (" + self.type + ")"


class AnimalShelter:

  # So we can track which animals are last/first added.
  insertion_count = 0

  # Contains our key : linked list pairs.
  animal_lists = {}

  def __init__(self, animals):
    for animal in animals:
      self.animal_lists[animal] = LinkedList()
      self.__build_animal_funcs(animal)

  # Builds our animal functions on the class.
  # For example if given the animal "monkey", this func will add a bound func
  # called "dequeue_monkey()" on the class.
  def __build_animal_funcs(self, animal):
    deque_func_name = "dequeue_" + animal
    def deque_animal(self):
      if (self.animal_lists[animal]):
        self.animal_lists[animal].remove_from_start()
    # Do not worry about the warning, it works, and well...
    bound_dequeue_func = deque_animal.__get__(self, self.__class__)
    # Nice!
    setattr(self, deque_func_name, bound_dequeue_func)

  # Add an animal to a que.
  def enqueue(self, animal_type, name):
    if (animal_type not in self.animal_lists):
      self.animal_lists[animal_type] = LinkedList()
      self.__build_animal_funcs(animal_type)

    # Add the new obj
    self.insertion_count += 1
    self.animal_lists[animal_type].add(Animal(animal_type, name, self.insertion_count))

  # Search the queues for the oldest item then pop from that que.
  def dequeue_any(self):
    que_with_last_added = None
    current_last = None
    for key in self.animal_lists:
      list = self.animal_lists[key]
      if (list != None and list.start != None):
        first_in_list = self.animal_lists[key].start
        if (current_last == None or current_last > first_in_list.value.id):
          current_last = first_in_list.value.id
          que_with_last_added = self.animal_lists[key]

    if (que_with_last_added != None):
      que_with_last_added.remove_from_start()

  def __str__(self):
    retStr = ""
    for key in self.animal_lists:
      retStr += "- " + key + " Animal Que -> "
      retStr += str(self.animal_lists[key])

    return retStr

  
  ## Here we should have our specific deque funcs made in constructor.


animal_shelter = AnimalShelter(["monkey", "dog", "cat"])
animal_shelter.enqueue("monkey", "George")
animal_shelter.enqueue("yoda species", "Grogu")
animal_shelter.enqueue("cat", "MOngroell")
animal_shelter.enqueue("monkey", "tammy")
animal_shelter.dequeue_monkey()
print(animal_shelter)