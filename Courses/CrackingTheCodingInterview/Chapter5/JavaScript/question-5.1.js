// Insertion.
const { BitUtils } = require('../../Core/JavaScript/bit-utils');

/**
 * 
 * @param { String } n 32 bit number.
 * @param { String } m 32 bit number to insert into n.
 * @param { Number } i start of bit insertion range.
 * @param { Number } j end of bit insertion range.
 */
const insert32Bit = (n, m, i, j) => {
  // Clear middle.
  const leftSeq = -1 << (j + 1); // 11100000
  const rightSeq = (1 << i) - 1; // 00000111
  const mask = leftSeq | rightSeq; // OR the two sides to get our one true mask!
  const shiftedM = BitUtils.BinaryToInt(m) << i;
  const clearedN = BitUtils.BinaryToInt(n) & mask;
  // OR with a shifted value
  return BitUtils.IntToBinary(clearedN | shiftedM);
}

console.log(
  'insert32Bit = ', 
  insert32Bit('10000000', '10011', 2, 7)
);
console.log(
  'insert32Bit = ', 
  insert32Bit('1000000000000011', '10011', 4, 9)
);