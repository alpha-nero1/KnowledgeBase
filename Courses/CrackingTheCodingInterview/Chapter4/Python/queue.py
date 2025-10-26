# Queue implementation.

class Queue:
  first = None
  last = None

  def add(self, value):
    # add using last node
    new_node = QueueNode(value)
    if (self.first == None):
      self.first = new_node
    if (self.last == None):
      self.last = new_node
    self.last.next = new_node
    new_node.prev = self.last
    self.last = new_node

  def remove(self):
    # Remove from start
    if (self.first):
      removed_node = self.first
      self.first = self.first.next
      return removed_node
    raise Exception('Queue has no first to remove.')

  def peek(self):
    return self.first

  def is_empty(self):
    return (self.first == None)
      

class QueueNode:
  prev = None
  next = None
  def __init__(self, value):
    self.value = value