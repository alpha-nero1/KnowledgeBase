// Refer to Core -> JavaScript -> stack-list.js

const { StackList } = require('../../Core/JavaScript/stack-list');

const sl = new StackList();
sl.push(10)
sl.push(10)
sl.push(10)
sl.push(8)
sl.push(2)
sl.pop()

console.log('min is: ', sl.min())