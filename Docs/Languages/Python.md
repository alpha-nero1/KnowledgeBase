# Python

To run your python files exec `python3 <path_to_file>`

Notes on python.
- Python leans towards the use of global functions to calculate common things. E.g. `len(myArray)`, `range(0, 9) // for i loops`, `str(myNum) // casting...`
- The true and false values are capitalised e.g `True`, `False`
- Can make use of `for in` loops (we can do this directly on objects e.g `for key in obj:`).
- An empty dictionary will evaluate to Falsy, to do checks for defined check `if variable is not None` `None` is the python equivalent of `undefined`
- py constructor = `def __init__(self):` and constructuon: `Dictionary()`
- to get index of for in loop you must use `for (idx, item) in enumerate(array)`
- to create array of fixed size note that `arr = [[]] * 10` will create and array of size 10 but all containing the SAME array ref.
- you can use lambda functions for easy filtering e.g. `filter(lambda chain: len(chain) > 0, self.__items)`
- Prefixing a func or var with `__` makes it private!!

- We can add functions to classes dynamically just as we can in JS, using the `setattr()` function. Example bellow:
```
  # Builds our animal functions on the class.
  # For example if given the animal "monkey", this func will add a bound func
  # called "dequeue_monkey()" on the class.
  def __build_animal_funcs(self, animal):
    deque_func_name = "dequeue_" + animal
    def deque_animal(self):
      if (self.animal_lists[animal]):
        self.animal_lists[animal].remove_from_start()
    # Do not worry about the warning, it works, and well...
    bound_dequeue_func = deque_animal.__get__(self, self.__class__) # this binds so we do not need to provide self arg.
    # Nice!
    setattr(self, deque_func_name, bound_dequeue_func)
```

- `def __str__(self):` is a func you can override on a class to return the string representation of instances.