/**
 * I struggled to understand this problem so I am going to break it down
 * A palindrome is a string that reads the same backwards and forwards e.g "poop"
 * A permutation is a re-organised form of the original string e.g. a palindrome
 * of "rat" is "tar" or "art" 
 * 
 * We can know that a string in particular is a palindrome because it only has a max of one odd count of characters in 
 * total e.g.
 * 
 * "total" cannot make a palindrome because its counts are: { 't' => 2, 'o' => 1, 'a' => 1, 'l' => 1 }, it has 3 chars that have odd counts.
 * Whereas the permutation/scramble "tact coa" (original string "taco cat") DOES have potential to form a palindrome because the counts are:
 * { 't' => 2, 'a' => 2, 'c' => 2, 'o' => 1 } where "o" is the one and only odd count.
 */

const incrementItemOfMap = (key, map) => {
  if (map && key) {
    const validCharTest = /[a-zA-Z]/g
    if (validCharTest.test(key)) {
      if (map.has(key)) {
        map.set(key, map.get(key) + 1);
      } else {
        map.set(key, 1);
      }
    }
  }
}

const buildCharStore = (value) => {
  const charStore = new Map();
  if (value && value.length) {
    for (let i = 0; i < value.length; i++) {
      incrementItemOfMap(value[i], charStore);
    }
  }
  return charStore;
}

const noMoreThanOneCharHasOddCount = (map) => {
  let hasNoMoreThanOneOdd = true;
  let alreadyFoundOddCount = false;
  map.forEach(charCount => {
    // REMEMBER: A return in the map callback will not terminate the outer scope.
    if (charCount % 2 === 1) {
      // Found an odd.
      if (alreadyFoundOddCount) {
        hasNoMoreThanOneOdd = false;
      }
      alreadyFoundOddCount = true;
    }
  });  
  return hasNoMoreThanOneOdd;
}

const isPermutationOfPalindrome = (value) => {
  const charStore = buildCharStore(value);
  console.log(charStore)
  return noMoreThanOneCharHasOddCount(charStore);
}

console.log('Is a permutation of a palindrome: "total"', isPermutationOfPalindrome('total'))
console.log('Is a permutation of a palindrome: "tact coa"', isPermutationOfPalindrome('tact coa'))
console.log('Is a permutation of a palindrome: "tact tact"', isPermutationOfPalindrome('tact tact'))
console.log('Is a permutation of a palindrome: "tactw coa"', isPermutationOfPalindrome('tactw coa'))

