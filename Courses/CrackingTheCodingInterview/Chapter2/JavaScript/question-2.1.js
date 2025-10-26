// 2.1 Remove Dups.

class LinkedList {
  constructor() {
    // First node is called the head.
    this.head = null;
    this.idCount = 0;
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
    } else {
      this.head = newNode;
    }
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
  }

  appendNewNodeToTail(value) {
    endNode = new ListNode(value);
    const node = this;
    while (node.next !== null) {
      node = node.next;
    }
    node.next = endNode
  }
}


const linkedList = new LinkedList();
linkedList.add(2)
linkedList.add(2)
linkedList.add(3)
linkedList.add(7)
linkedList.add(7)
linkedList.add(7)
linkedList.add(1)
linkedList.add(1)

const removeNodeRef = (prev, node, next) => {
  if (prev) {
    // Say that the current next value is the initial one sent in.
    let currentNext = next;
    // We will remove the node ref by setting the `next` value of our
    // previous node to the currentNext (initial) which puts `node` out of
    // the equation.
    prev.next = currentNext;
    // Okay we deleted node, but wait, the next we set, still equals our node's value, it must also be deleted.
    while (prev.next && prev.next.value === node.value) {
      // Run the same statement.
      prev.next = currentNext;
      // Increment the currentNext up the next chain.
      currentNext = next.next;
    }
  }
}

const removeDuplicates = linkedList => {
  if (linkedList && linkedList.head) {
    const nodeStore = new Map();
    let node = linkedList.head;
    let prev = null;

    while (node) {
      if (nodeStore.has(node.value)) {
        removeNodeRef(prev, node, node.next)
      } else {
        nodeStore.set(node.value, true);
      }
      // Keep track of what will be the next nodes previous node.
      prev = node;
      node = node.next;
    }
  }
}

console.log('before: ', linkedList.toString())
removeDuplicates(linkedList)
console.log('after: ', linkedList.toString())

