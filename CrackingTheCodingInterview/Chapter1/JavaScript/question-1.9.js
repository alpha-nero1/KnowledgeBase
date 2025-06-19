// String rotation.
// I liked this question very clever.

// Check value one is a substr of valueTwo.
const isSubstring = (firstValue, secondValue) => {
  return secondValue.search(firstValue) > -1;
}

const isStringRotation = (firstValue, secondValue) => {
  // First value must have identical char count to second value.
  // But more importantly it must have the same sequence if you imagein the string to be a loop.
  if (
    firstValue && 
    firstValue.length &&
    secondValue &&
    secondValue.length
  ) {
    // E.g secondValue = erbottlewat
    //      firstValue = waterbottlewaterbottle
    // We can always find if it is a rotation if the second value
    // can be found as a substring of first value doubled.
    // this of course works as well if you try find first value in the two second values.
    // Can you find string 2 amongst two string ones.
    return isSubstring(secondValue, `${firstValue}${firstValue}`)
  }
  return false;
}

console.log('isStringRotation water - erwat', isStringRotation('water', 'erwat'));
console.log('isStringRotation donkey - schmon', isStringRotation('donkey', 'schmon'));
console.log('isStringRotation waterbottle - bottledater', isStringRotation('waterbottle', 'bottledater'))
console.log('isStringRotation waterbottle - erbottlewat', isStringRotation('waterbottle', 'erbottlewat'))