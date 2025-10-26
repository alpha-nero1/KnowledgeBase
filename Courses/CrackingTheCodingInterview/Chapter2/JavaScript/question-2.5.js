// Sum lists.
const { LinkedList, ListNode } = require('../../Core/JavaScript/linked-list');

const buildNumFromArray = (array, reversed) => {
  let str = '';
  if (array && array.length) {
    if (reversed) {
      for (let i = array.length - 1; i > -1; i--) {
        str += `${array[i]}`;
      }
    } else {
      for (let i = 0; i < array.length; i++) {
        str += `${array[i]}`;
      }
    }
  }
  return +str;
}

const sumLists = (listOne, listTwo, reversedOrder) => {
  let sum = 0;
  if (listOne, listTwo) {
    const firstValues = [];
    const secondValues = [];
    let listOneNode = listOne.head;
    while (listOneNode) {
      firstValues.push(listOneNode.value)
      listOneNode = listOneNode.next;
    }
    let listTwoNode = listTwo.head;
    while (listTwoNode) {
      secondValues.push(listTwoNode.value)
      listTwoNode = listTwoNode.next;
    }
    const numOne = buildNumFromArray(firstValues, reversedOrder);
    const numTwo = buildNumFromArray(secondValues, reversedOrder);
    sum = numOne + numTwo;
  }
  return sum;
}

const listOne = new LinkedList();
const listTwo = new LinkedList();

listOne.add(6)
listOne.add(1)
listOne.add(3)

listTwo.add(6)
listTwo.add(9)
listTwo.add(2)

console.log('list one: ', listOne.toString());
console.log('list two: ', listTwo.toString());
console.log('sum (reversed): ', sumLists(listOne, listTwo, true))
console.log('sum (normal): ', sumLists(listOne, listTwo, false))