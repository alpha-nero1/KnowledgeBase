# Order 66 err code.
ERR_CODE = -66

def get_node_height(node):
  leftHeight, rightHeight = 0, 0 # Init heights.
  if (node == None): # If node is none, discredit the count. our check prevents this though.
    return -1
  if (node.left): # If left, call this func on the left node for left height.
    leftHeight = get_node_height(node.left)
    # If left check resulted in err, propogate that without checking the right.
    if (leftHeight == ERR_CODE):
      return ERR_CODE
  if (node.right): # If right, call this func for right height.
    rightHeight = get_node_height(node.right)
    if (rightHeight == ERR_CODE):
      return ERR_CODE

  # Lets check if this step results in err (diff more than 1).
  if (abs(leftHeight - rightHeight) > 1):
    return ERR_CODE
  else:
    # In the case now that everything is okay return the max of both heights and increment it.
    return max(leftHeight, rightHeight) + 1

def check_tree_is_balanced(root):
  return get_node_height(root) != ERR_CODE