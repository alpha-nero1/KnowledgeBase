# First common ancestor.
# " Design an algorithm and write code to find the first common 
# ancestor of two nodes in a binary tree. 
# Avoid storing additional nodes in a data structure.
# NOTE: This is not necessarily a binary search tree. "
from binary_node import BinaryNode

def get_node_height(node):
  height = 0
  while (node != None):
    node = node.parent
    height += 1
  return height

def go_up_by_levels(node, levels):
  c = levels
  while (c > 0 and node != None):
    node = node.parent
    c -= 1
  return node

def find_first_common_ancestor(node_one, node_two):
  h_one = get_node_height(node_one)
  h_two = get_node_height(node_two)
  first = None
  second = None
  if h_one > h_two:
    first = node_two
    second = node_one
  else:
    first = node_two
    second = node_one
  # This brings us to an equal playing field, is important for the following while loop.
  second = go_up_by_levels(second, abs(h_one - h_two))
  
  # This check only works because we force second to be of that of the
  # same height as first, this means we can check on by one as we go up levels
  # if the nodes are the same, then we have found the ancestor!
  while (first != second and first != None and second != None):
    first = first.parent
    second = second.parent
  
  # This means either reached the top and the while did not detect where first equalled second
  # We could not find ancestor :/ ret None.
  if (first == None or second == None): return None
  # Else first and second are equal now so, return either.
  return first

root = BinaryNode(1)
root.set_left(BinaryNode(2))
root.set_right(BinaryNode(3))
root.left.set_left(BinaryNode(4))
root.left.set_right(BinaryNode(5))
root.right.set_right(BinaryNode(6))
root.right.set_left(BinaryNode(7))

common = find_first_common_ancestor(root.right.right, root.right.left)
print(common)