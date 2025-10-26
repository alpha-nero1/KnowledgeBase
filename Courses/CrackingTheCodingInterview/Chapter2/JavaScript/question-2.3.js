// Delete middle node. O(n)

class LinkedList {
  constructor() {
    // First node is called the head.
    this.head = null;
    this.idCount = 0;
  }

  static DeleteNode(prev, next) {
    if (prev) {
      prev.next = next;
    }
  }

  add(value) {
    const newNode = new ListNode(value);
    this.idCount = this.idCount + 1;
    newNode.id = this.idCount;
    if (this.head) {
      let node = this.head;
      while (node.next != null) {
        node = node.next;
      }
      // By this point we are at last node of the chain.
      node.next = newNode;
      newNode.prev = node;
    } else {
      this.head = newNode;
    }
    return newNode;
  }

  delete(value) {
    if (this.head) {
      let node = this.head;
      if (node.value === value) {
        this.head = node.next; // moved head...
      }
      while (node.next !== null) {
        if (node.next.value === value) {
          node.next = node.next.next;
          // Standard deletion, head did not change.
          return;          
        }
        node = node.next;
      }
    }
  }

  toString() {
    let node = this.head;
    let str = ''
    while (node) {
      str += ` -> (id: ${node.id}: value: ${node.value})`
      node = node.next;
    }
    return str;
  }
}

class ListNode {
  constructor(value) {
    this.id = null;
    this.next = null;
    this.value = value;
    this.prev = null;
  }

  appendNewNodeToTail(value) {
    endNode = new ListNode(value);
    const node = this;
    while (node.next !== null) {
      node = node.next;
    }
    node.next = endNode
  }

  toString() {
    return `(id: ${this.id}: value: ${this.value})`;
  }
}

const linkedList = new LinkedList();
linkedList.add(2)
linkedList.add(2)
linkedList.add(3)
linkedList.add(7)
linkedList.add(7)
linkedList.add(7)
const refNode = linkedList.add(1)
linkedList.add(1)

const deleteMiddleNode = (node) => {
  if (node) {
    const allNodes = [];
    // Find all previous nodes.
    let prevNode = node.prev;
    while (prevNode) {
      allNodes.unshift(prevNode)
      prevNode = prevNode.prev
    }
    // Find all next nodes.
    let nextNode = node.next;
    while (nextNode) {
      allNodes.push(nextNode)
      nextNode = nextNode.next
    }
    // If prevC + nextC count > 2 then run a midway deletion.
    if (allNodes.length > 2) {
      const middle = Math.floor((allNodes.length - 1) / 2);
      LinkedList.DeleteNode(allNodes[middle - 1], allNodes[middle + 1])
    }
  }
}

console.log(linkedList.toString())

deleteMiddleNode(refNode);

console.log(linkedList.toString())
