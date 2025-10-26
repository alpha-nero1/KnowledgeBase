const { BinaryNode } = require('../../Core/JavaScript/binary-node'); 

const ERR_CODE = -66;

const checkHeight = (node) => {
  // If no node, discredit this count.
  if (!node) return -1;
  const leftHeight = checkHeight(node.left);
  // If left count is an err, throw up the chain.
  if (leftHeight == ERR_CODE) return leftHeight;
  const rightHeight = checkHeight(node.right);
  // if right count is an err, throw up the chain.
  if (rightHeight == ERR_CODE) return rightHeight;
  // Check the actual diff of two sides and...
  const heightDiff = leftHeight - rightHeight;
  console.log(leftHeight, rightHeight)
  if (Math.abs(heightDiff) > 1) {
    // Either return err code up.
    return ERR_CODE;
  } else {
    // Or keep the checking going.
    return Math.max(leftHeight, rightHeight) + 1;
  }
}

const checkIsBalanced = (node) => {
  // Check if one side has more than one levels than the other.
  // This is unbalanced.
  return checkHeight(node) != ERR_CODE;
}

const n = new BinaryNode(1);
// L1
n.setLeft(new BinaryNode(2));
n.setRight(new BinaryNode(3));
// L2
n.left.setRight(new BinaryNode(5));
n.left.setLeft(new BinaryNode(4));
n.right.setRight(new BinaryNode(6));
n.right.setLeft(new BinaryNode(7));
// L3
n.right.right.setRight(new BinaryNode(8));
n.right.right.setLeft(new BinaryNode(9));
// L4
n.right.right.right.setRight(new BinaryNode(10))

console.log(checkIsBalanced(n));