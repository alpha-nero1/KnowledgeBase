// Kth to last.

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
linkedList.add(1)
linkedList.add(1)

const findKthToLast = (linkedList, k) => {
  if (linkedList && linkedList.head) {
    const trackingArray = [];
    let node = linkedList.head;
    while (node) {
      trackingArray.push(node);
      // Condition changer.
      node = node.next;
    }
    const accessorIndex = (trackingArray.length - 1) - k;
    if (accessorIndex > -1 && accessorIndex < trackingArray.length) {
      return trackingArray[(trackingArray.length - 1) - k];
    }
    return null;
  }
  return null;
}

console.log('linked list: ', linkedList.toString());
console.log('findKthToLast 3', findKthToLast(linkedList, 3))
console.log('findKthToLast 5', findKthToLast(linkedList, 5))
console.log('findKthToLast 0', findKthToLast(linkedList, 0))
console.log('findKthToLast 7', findKthToLast(linkedList, 7))
console.log('findKthToLast 10', findKthToLast(linkedList, 10))