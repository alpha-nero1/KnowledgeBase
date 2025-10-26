// Partition

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
linkedList.add(1)

const partitionLinkedList = (linkedList, partition) => {
  const firstPartition = new LinkedList();
  const secondPartition = new LinkedList()
  let currentNode = linkedList.head;
  let firstPartitionTail;
  while (currentNode) {
    if (currentNode.value < partition) {
      firstPartitionTail = firstPartition.add(currentNode.value);
    } else {
      secondPartition.add(currentNode.value)
    }
    currentNode = currentNode.next;
  }
  // Join the partitions.
  if (secondPartition.head) {
    if (firstPartitionTail) {
      firstPartitionTail.next = secondPartition.head
    }
  }
  return firstPartition;
}

console.log(linkedList.toString())

const parted = partitionLinkedList(linkedList, 3)

console.log(parted.toString())
