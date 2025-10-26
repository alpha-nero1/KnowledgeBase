// cat question-4.10.swift ../../Core/Swift/binary-node.swift | swift -

// Is subtree.

func doesMatchTree(r1: BinaryNode?, r2: BinaryNode?) -> Bool {
  if (r1 == nil && r2 == nil) {
    return true; // We reached the end of the tree, our match has been detected.
  } else if (r1 == nil || r2 == nil) {
    return false; // A null cannot match another actual value.
  } else if (r1!.value != r2!.value) {
    return false; // we had two nodes but no match.
  }
  // Else continue checks down the chain of left and rights.
  return doesMatchTree(r1: r1!.left, r2: r2!.left) && doesMatchTree(r1: r1!.right, r2: r2!.right)
}

func isSubtree(r1: BinaryNode?, r2: BinaryNode) -> Bool {
  if (r1 == nil) {
    return false; // The bigger tree is empty and sub still was not found.
  } else if (r1!.value == r2.value && doesMatchTree(r1: r1!, r2: r2)) {
    // We have a potential starting root node + does match tree suceeded!
    return true;
  }
  // Else continue down the tree to check for further possible r2 root matches!
  return isSubtree(r1: r1?.left, r2: r2) || isSubtree(r1: r1?.right, r2: r2);
}

func containsTree(nodeOne: BinaryNode, nodeTwo: BinaryNode?) -> Bool {
  if let safeNodeTwo = nodeTwo {
    return isSubtree(r1: nodeOne, r2: safeNodeTwo);
  }
  return true;
}

// func preOrderTraverse(node: BinaryNode?) -> Void {
//   if let safeNode = node {
//     print(safeNode);
//     preOrderTraverse(node: safeNode.left);
//     preOrderTraverse(node: safeNode.right);
//   }
// }

let root = BinaryNode(value: 1);
root.left = BinaryNode(value: 2);
root.right = BinaryNode(value: 3);
root.right?.right = BinaryNode(value: 4);
root.right?.right?.left = BinaryNode(value: 10);
root.right?.left = BinaryNode(value: 5);
let match = root.right?.right;

let rand = BinaryNode(value: 77);
rand.left = BinaryNode(value: 66);

print(containsTree(nodeOne: root, nodeTwo: match))