// cat ../../Core/Swift/binary-node.swift question-4.5.swift | swift -

func checkValidBst(root: BinaryNode?, minValue: Int?, maxValue: Int?) -> Bool {
  if (root == nil) {
    return true;
  }
  if let mxValue = maxValue { // Using if lets to unwrap the Int?
    if (root!.value > mxValue) {
      return false;
    }
  }
  if let miValue = minValue {
    if (root!.value <= miValue) {
      return false;
    }
  }
  if (
    !checkValidBst(root: root?.left, minValue: minValue, maxValue: root!.value) || 
    !checkValidBst(root: root?.right, minValue: root!.value, maxValue: maxValue)
  ) {
    return false;
  }
  return true;
}

let root = BinaryNode(value: 9);
root.left = BinaryNode(value: 7);
root.right = BinaryNode(value: 11);
// ? means only run if value present.
root.left?.left = BinaryNode(value: 6);
root.right?.right = BinaryNode(value: 20);
root.right?.left = BinaryNode(value: 1);

print("Valid bst: \(checkValidBst(root: root, minValue: nil, maxValue: nil))");