class BinaryNode:
  value = None
  left = None
  right = None
  parent = None

  def __init__(self, value):
    self.value = value

  def set_left(self, node):
    self.left = node
    self.left.parent = self

  def set_right(self, node):
    self.right = node
    self.right.parent = self

  def left_value(self):
    if (self.left != None):
      return str(self.left.value)
    return ""

  def right_value(self):
    if (self.right != None):
      return str(self.right.value)
    return ""
    
  def __str__(self):
    return "[BinaryNode value: " + str(self.value) + ", left: " + self.left_value() + ", right: " + self.right_value() + "]"