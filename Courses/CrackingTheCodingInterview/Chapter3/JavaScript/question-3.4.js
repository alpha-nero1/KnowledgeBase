// Que via stacks. que: FIFO.
const { StackList } = require('../../Core/JavaScript/stack-list');

class MyQueue {
  constructor() {
    this.stackOldest = new StackList();
    this.stackNewest = new StackList();
  }

  add(value) {
    this.stackNewest.push(value);
  }

  // Remove first from two stacks.
  remove() {
    // sift stacks.
    this.shiftStacks();
    this.stackOldest.pop();    
  }

  shiftStacks() {
    // Funnel all those from stack newest into oldest.
    if (!this.stackNewest.isEmpty()) {
      // Push em.
      while (!this.stackNewest.isEmpty()) {
        let newPopped = this.stackNewest.pop();
        if (newPopped) {
          this.stackOldest.push(newPopped.value);
        }
      }
    }
  }

  count() {
    return this.stackNewest.count() + this.stackOldest.count();
  }

  peek() {
    // Shift stacks.
    this.shiftStacks();
    return this.stackOldest.peek();
  }

  isEmpty() {
    return !this.stackOldest.isEmpty();
  }

  toString() {
    let str = '';
    let oldNode = this.stackOldest.last;
    while (oldNode) {
      str += `\n- old node: ${oldNode.value}`
      oldNode = oldNode.prev;
    }
    let newNode = this.stackNewest.last;
    while (newNode) {
      str += `\n- new node: ${newNode.value}`
      newNode = newNode.prev;
    }
    return str;
  }
}

const mq = new MyQueue(5);
mq.add(3);
mq.add(1);
mq.add(9);
mq.add(0);
mq.add(2);
mq.add(7);
mq.add(2);
mq.add(5);
console.log(mq.toString())
mq.remove();
mq.remove();
mq.remove();
mq.remove();
mq.remove();
mq.remove();

console.log(mq.toString())