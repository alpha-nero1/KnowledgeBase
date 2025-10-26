const { BitUtils } = require('../../Core/JavaScript/bit-utils');

/**
 * Swap the pairs of bits in a binary integer. E.g. swap bit 0 with 1, bit 2 with 3.
 * 
 * @param { String } binary string binary representation.
 */
const pairWiseSwap = (binary) => {
  const value = BitUtils.BinaryToInt(binary);
  // 0xaa = 1010 1010 0xaaaa = 1010 1010 1010 1010 // 1s in even spots.
  // And with the value to save the evens if they were there, then shift all to the right.
  // to odd positions.
  const evenToOdd = ((value & 0xaaaaaaaa) >>> 1);
  // 0x5 = 0101 (all odd) save all odd values and shift LEFT to put into even positions.
  const oddToEven = ((value & 0x55555555) << 1);
  // Return the OR to combine the values!
  return (oddToEven | evenToOdd);
}

console.log('pairWiseSwap (10101010) = ', BitUtils.IntToBinary(pairWiseSwap('10101010')));
console.log('pairWiseSwap (11101000) = ', BitUtils.IntToBinary(pairWiseSwap('11110000')));