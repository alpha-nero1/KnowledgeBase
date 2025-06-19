// Loop detection.

const { LinkedList, ListNode } = require('../../Core/JavaScript/linked-list');

const getFirstNodeAtStartOfLoop = list => {
  let node = list.head;
  const map = new Map();
  let foundLoop = false;
  while (node && !foundLoop) {
    if (map.has(node.next)) {
      foundLoop = true;
      return node.next
    }
    map.set(node, node);
    node = node.next;
  }
  return null
}

const l1 = new LinkedList();
l1.addBatch(1, 2, 3);
const sln = l1.add('a');
l1.addBatch(23, 'b', 'c');
const eln = l1.add('z');
eln.next = sln

console.log(getFirstNodeAtStartOfLoop(l1))