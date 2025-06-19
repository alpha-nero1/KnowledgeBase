/*
10.4. Sorted Search, No Size: You are given an array-like data structure Listy which lacks a size
method. It does, however, have an elementAt ( i) method that returns the element at index i in
0( 1) time. If i is beyond the bounds of the data structure, it returns -1. (For this reason, the data
structure only supports positive integers.) Given a Listy which contains sorted, positive integers,
find the index at which an element x occurs. If x occurs multiple times, you may return any index.
*/


class Node {
    constructor(value) {
        this.next = null;
        this.value = value;
    }
}
class Listy {
    constructor() {
        this.head = null;
        this.tail = null;
    }

    addNode(node) {
        if (this.head === null) {
            this.head = node;
        }
        if (this.tail === null) {
            this.tail = node;
            return;
        }
        // Set the next of the tail to be the node;
        this.tail.next = node;
        // Set the new tail.
        this.tail = this.tail.next;
    }

    valueAt(index) {
        let node = this.head;
        let runningIndex = 0;
        while (node != null) {
            if (index === runningIndex) return node.value;
            runningIndex += 1;
            node = node.next;
        }
        return -1;
    }
}

function binarySearch(list, searchVal) {
    let index = 1;
    // Get a calculation on the size of the array.
    while (list.valueAt(index) !== -1 && list.valueAt(index) < searchVal) {
        index *= 2;
    }

    return binarySearchInner(list, searchVal, index / 2, index);
}

function binarySearchInner(list, searchVal, low, high) {
    let mid = null;
    while (low <= high) {
        mid = (low + high) / 2
        let middle = list.valueAt(mid);
        if (middle > searchVal || middle == -1) {
            high = mid - 1;
        } else if (middle < value) {
            low = mid + 1;
        }
        return mid;
    }
    return -1;
}