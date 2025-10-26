// Single array to implement 3 stacks.

class FixedMutliStack {
  constructor(stackSize, stackCount) {
    if (stackSize && stackCount) {
      this.array = new Array(stackSize * stackCount);
      this.stacks = new Array(stackCount);
      let stackFloor = 0;
      let stackCeiling = 0
      for (let i = 0; i < stackCount; i++) {
        stackCeiling = stackFloor;
        stackFloor += this.array.length / stackCount;
        // [ceiling, floor, index pointer]
        this.stacks[i] = [stackCeiling, stackFloor, stackCeiling]
      }
    }
  }

  pushToStack(stackNum, value) {
    const stack = this.stacks[stackNum - 1];
    // Increase stack pointer
    if (stack[2] < stack[1]) {
      stack[2] = stack[2] + 1;
      this.array[stack[2]] = value;
    } else {
      throw new Error(`Stack ${stackNum} is out of room.`)
    }
    
  }

  popFromStack(stackNum) {
    const stack = this.stacks[stackNum - 1];
    const pointer = stack[2];
    this.array[pointer] = null;
    stack[2] -= 1;
  }

  peekStack(stackNum) {
    const stack = this.stacks[stackNum - 1];
    return this.array[stack[2]];
  }

  isStackEmpty(stackNum) {
    return !!this.peekStack(stackNum)
  }

  toString() {
    return this.array;
  }
}

const fms = new FixedMutliStack(10, 3);

fms.pushToStack(2, 13)
fms.pushToStack(2, 11)
fms.pushToStack(2, 16)

fms.pushToStack(1, 'a')
fms.pushToStack(1, 133)
fms.pushToStack(1, 16)

fms.popFromStack(1)
fms.pushToStack(1, 16)

console.log(fms.toString());