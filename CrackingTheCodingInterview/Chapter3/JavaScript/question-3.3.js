// Stack of plates.
const { StackList } = require('../../Core/JavaScript/stack-list');

class SetOfStacks {
  constructor(capacity) {
    if (!capacity) {
      throw new Error('Must specify capacity for set of stacks.');
    }
    this._capacity = capacity;
    this._stackPointer = 0;
    this._stacks = [new StackList()];
  }

  push(value) {
    const stack = this._stacks[this._stackPointer];
    if (stack.count() > this._capacity) {
      // start new stack, else push to it.
      const newStack = new StackList();
      newStack.push(value);
      this._stacks.push(newStack);
      this._stackPointer = this._stacks.length - 1;
    } else {
      stack.push(value);
    }
  }

  pop() {
    let stack = this._stacks[this._stackPointer];
    if (stack.count() === 0) {
      // We depleted the stack move back to previous and run func again.
      this._stackPointer--;
      this.pop();
    } else {
      stack.pop();
    }
  }

  popAt(stack) {
    const stack = this._stacks[this._stackPointer];
    if (!stack) {
      throw new Error('Invalid stack specified.')
    }
    stack.pop();
  }
}

const ses = new SetOfStacks(5);

ses.push(1)
ses.push(7)
ses.push(1)
ses.push(3)
ses.push(1)
ses.push(12)
ses.push(12)
ses.push('a')
ses.push('b')
ses.pop()
ses.pop()
ses.pop()
ses.pop()

console.log('set of stacks', ses._stacks);