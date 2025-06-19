// Build a binary search tree from an ordered array.
const { BinaryNode } = require('../../Core/JavaScript/binary-node')

// Returns root binary node.
const buildBstFromArray = (array, start, end) => {
  if (array && !(end < start)) {
    const mid = parseInt((start + end) / 2);
    const root = new BinaryNode(array[mid]);
    console.log('created root for', mid,  array[mid])
    root.left = buildBstFromArray(array, start, mid - 1);
    root.right = buildBstFromArray(array, mid + 1, end);
    return root;
  }
  return null;
}

const testArray = [0, 1, 3, 5, 6, 7, 9, 10, 11, 12, 34, 55, 66];
buildBstFromArray(testArray, 0, testArray.length - 1);