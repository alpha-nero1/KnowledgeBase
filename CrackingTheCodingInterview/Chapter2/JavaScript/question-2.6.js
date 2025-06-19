// Palindrome

const { LinkedList, ListNode } = require('../../Core/JavaScript/linked-list');

const listIsPalindrome = (linkedList) => {
  if (linkedList) {
    let node = linkedList.head;
    const values = [];
    while (node) {
      values.push(node.value);
      node = node.next;
    }
    for (let i = 0; i < values.length; i++) {
      const oppositeIndex = values.length - 1 - i;
      if (values[i] !== values[oppositeIndex]) {
        return false
      }
    }
    return true;
  }
  return false;
}

const list = new LinkedList();
list.addBatch(3, 1, 1, 3);

const listTwo = new LinkedList();
listTwo.addBatch(3, 1, 2, 3);

console.log('listIsPalindrome addBatch(3, 1, 1, 3)', listIsPalindrome(list))
console.log('listIsPalindrome addBatch(3, 1, 2, 3)', listIsPalindrome(listTwo))