// Lists intersection.

const { LinkedList, ListNode } = require('../../Core/JavaScript/linked-list');

const getListIntersection = (listOne, listTwo) => {
  if (listOne && listTwo) {
    const intersectionMap = new Map();
    let node = listOne.head;
    while (node) {
      intersectionMap.set(node, node);
      node = node.next;
    }
    let nodeTwo = listTwo.head;
    while (nodeTwo) {
      if (intersectionMap.has(nodeTwo)) {
        return nodeTwo;
      }
      nodeTwo = nodeTwo.next;
    }
  }
  return null;
}


const l1 = new LinkedList()
l1.addBatch(1, 3, 4, 5, 10, 9)
const intersect = l1.add(3);
l1.addBatch(3, 6, 9, 72)

const l2 = new LinkedList();
l2.addBatch(1, 2, 66, 55, 34, 2)
l2.addNode(intersect)
l2.addBatch(1, 2, 66, 55, 34, 2)

console.log('getListIntersection: ', getListIntersection(l1, l2))