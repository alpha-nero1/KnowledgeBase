const { StackList } = require('../../Core/JavaScript/stack-list');

class SortStack extends StackList {
  constructor(isAscending = true) {
    super();
    this.isAscending = isAscending;
  }

  push(value) {
    super.push(value);
    this.sort();
  }

  pushNoSort(value) {
    super.push(value);
  }

  sort() {
    let tmpStack = new StackList();
    while (!this.isEmpty()) { // On first run, while this stack is not empty..
      // Pick an el from the top of the stack.
      let tmp = this.pop();
      // While the tmp stack is not empty and its top value is greater than el.
      while (
        !tmpStack.isEmpty() && 
        tmpStack.peek().value > tmp.value
      ) {
        // Push the tmp vars into this.
        let poppedValue = tmpStack.pop()
        this.pushNoSort(poppedValue ? poppedValue.value : null);
      }
      // Then in the temporarty stack push our value (tmp will stay sorted.)
      tmpStack.push(tmp ? tmp.value : null);
    }

    // We have cleaned this stack and the tmp stack is sorted, copy the elements across.
    while (!tmpStack.isEmpty()) {
      let poppedValue = tmpStack.pop();
      this.pushNoSort(poppedValue ? poppedValue.value : null);
    }
  }
}

const ss = new SortStack();
ss.push(1);
ss.push(3);
ss.push(1);
ss.push(7);
ss.push(1);
ss.push(2);
ss.push(1);
ss.push(3);
ss.push(8);

console.log(ss.toString());