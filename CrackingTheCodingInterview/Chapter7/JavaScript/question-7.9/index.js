class Node {
    constructor(prev = null, next = null, value) {
        this.prev = prev;
        this.next = next;
        this.value = value;
    }
}

class CircularArray {
    constructor() {
        this.head = null;
        this.count = 0;
    }

    // Get an element at the index specified.
    // Really nice re-useable code here to pull items out of the list
    // and most importantly, get the tail el.
    get(index) {
        if (index < 0 || index > (this.count - 1)) throw new Error(`Index ${index} out of range`);
        let node = this.head;
        for (let i = 0; i <= index; i++) {
            if (i === index) return node;
            node = node.next;
        }
    }

    // Push a new item to the end of the circular array.
    push(item) {
        const newNode = new Node(null, null, item);
        // If no head, set the first el.
        if (this.head == null) {
            this.head = newNode;
        } else {
            // Else get the tail element.
            const tail = this.getTail();
            // This would remove the current tails reference to head as next!
            tail.next = newNode;
            newNode.prev = tail;
        }
        // New tail next is the head, it's circular!
        newNode.next = this.head;
        this.count += 1;
    }

    // Pop an item off the end of the circular array.
    pop() {
        const tail = this.getTail();
        const newTail = tail.prev;
        if (!newTail) return;
        newTail.next = this.head;
        this.count -= 1;
    }

    // Reverse the circular linkes list. You can think of this solution
    // as iterating through each node and turning it 180 degrees.
    reverse() {
        if (!this.head) return;
        let prev = null;
        let node = this.head;
        // Great use of do - while here.
        // This do while lets us do the first pass of logic where current will be head!
        do {
            const next = node.next;
            // We essentially loop through the list and swap prev and next on the current node.
            node.next = prev;
            node.prev = next;
            // Move the dial along.
            // Specify previous as current node to show that we have moved along.
            prev = node;
            // Now containue on to the next node.
            node = next;
        } while (node != this.head)

        // Now the heads next will be the last recorded node of the iteration.
        this.head.next = prev;
        // And the current head will be the lat recorded node.
        this.head = prev;
    }

    // Run a callback for each element in the circular array.
    forEach(callback) {
        const headRef = this.head;
        let node = this.head;
        let i = 0;
        if (!this.head) return;
        while (node != null) {
            callback(node, i);
            // Short out if the next element will be head again!
            if (node.next === headRef) return;
            node = node.next;
            i++;
        }
    }

    // Convert linked list to array.
    toArray() {
        const retArray = [];
        this.forEach((node) => {
            retArray.push(node.value);
        })
        return retArray;
    }

    // Display the javascript object.
    toString() {
        let str = '[CircularArray: '
        this.forEach((node, i) => {
            const comma = i !== (this.count - 1) ? ', ' : '';
            str += (node.value + `${comma}`);
        });
        str += ']';
        return str;
    }

    getTail() {
        return this.get(this.count - 1);
    }
}


const arr = new CircularArray();
arr.push(1);
arr.push(2);
arr.push(4);
arr.push(7);
arr.pop();
console.log('Before: ' + arr)
arr.reverse();
console.log('After: ' + arr)