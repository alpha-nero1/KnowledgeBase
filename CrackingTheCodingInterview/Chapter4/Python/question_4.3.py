from binary_node import BinaryNode
from linked_list import LinkedList

def buildDepthList(listArr, node, level = 1):
  if (node == None):
    return listArr
  # For the current node add to the list via its level.
  # Run the same code for its left and right.
  if (len(listArr) < level):
    listArr.append(LinkedList())
  listArr[level - 1].add(node)
  if (node.left != None):
    listArr = buildDepthList(listArr, node.left, level + 1)
  if (node.right != None):
    listArr = buildDepthList(listArr, node.right, level + 1)
  return listArr

def exec():
  root = BinaryNode(1)
  root.left = BinaryNode(2)
  root.right = BinaryNode(3)
  root.right.right = BinaryNode(4)
  root.right.left = BinaryNode(5)
  root.left.left = BinaryNode(6)
  root.left.right = BinaryNode(7)
  list = buildDepthList([], root)
  for llist in list:
    print(llist)

exec()