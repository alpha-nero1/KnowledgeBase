class ListNode:
  next = None
  prev = None
  value = None
  def __init__(self, value):
    self.value = value

# Simple linked list implementation.
class LinkedList:

  start = None

  end = None

  count = 0

  def add(self, value):
    newNode = ListNode(value)
    if (self.start == None or self.end == None):
      if (self.start == None):
        self.start = newNode
      if (self.end == None):
        self.end = newNode
    else:
      # Add to end
      self.end.next = newNode
      newNode.prev = self.end
      self.end = newNode
    self.count += 1

  def remove_from_start(self):
    if (self.start != None):
      self.start = self.start.next
      if (self.start != None):
        self.start.prev = None
      self.count -= 1

  def remove_from_end(self):
    if (self.end != None):
      self.end = self.end.prev
      self.end.next = None
      self.count -= 1

  def remove_node_from_list(self, node):
    tmpNode = self.start
    tmpPrev = None
    while (tmpNode):
      if (tmpNode == node):
        if (tmpPrev != None):
          tmpPrev.next = tmpNode.next
        if (tmpNode.next):
          tmpNode.next.prev = tmpPrev
        self.count -= 1
        return tmpNode
      tmpPrev = tmpNode
      tmpNode = tmpNode.next

  def remove_from_list_by_value(self, value):
    tmpNode = self.start
    tmpPrev = None
    while (tmpNode):
      if (tmpNode.value == value):
        if (tmpPrev != None):
          tmpPrev.next = tmpNode.next
        if (tmpNode.next):
          tmpNode.next.prev = tmpPrev
        self.count -= 1
        return tmpNode
      tmpPrev = tmpNode
      tmpNode = tmpNode.next

  def is_empty(self):
    return self.end == None and self.start == None

  # Override string rep.
  def __str__(self):
    retStr = ""
    passedFirst = False
    node = self.start
    while (node):
      if (passedFirst):
        retStr += ", "
      retStr += str(node.value)
      passedFirst = True
      node = node.next
    retStr += "\n"
    return retStr
