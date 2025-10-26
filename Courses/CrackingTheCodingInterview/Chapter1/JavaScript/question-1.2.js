// NOTE: This is incorrect, you accidently went a step further and figured out if
// permutation of string a occurs in string s instead of if they are both equals perms.

const buildStringAndOccurancesMap = (str) => {
  const retStore = new Map();
  if (str && str.length) {
    for (let i = 0; i < str.length; i++) {
      if (!retStore.get(str[i])) {
        retStore.set(str[i], 1);
      } else {
        retStore.set(str[i], retStore.get(str[i]) + 1);
      }
    }
  }
  return retStore;
}

// Count down store e.g
// abbc = a : 1, b : 2, c : 1
const isPermutationFromMap = (str, countDownStore) => {
  for (let i = 0; i < str.length; i++) {
    const current = countDownStore.get(str[i])
    if (current) {
      countDownStore.set(str[i], current - 1)
    } else if (typeof current === 'undefined') {
      // The current was not even found in ths str,
      // back out, this is not a permutation.
      return false;
    }
  }
  const iterator = countDownStore.values();
  let countRemaining;
  while (!countRemaining) {
    const nextValue = iterator.next();
    countRemaining = Boolean(nextValue);
  }
  return countRemaining;
}

const isPermutation = (perm, str) => {
  if (perm && perm.length && str && str.length) {
    const permStore = buildStringAndOccurancesMap(perm);
    for (let i = 0; i < str.length; i++) {
      if (i + perm.length < str.length && permStore.has(str[i])) {
        // Check if the next perm length of items are a permutation.
        const countDownStore = new Map(permStore);
        return isPermutationFromMap(str.substring(i, i + perm.length), countDownStore)
      }
    }
  }
  return false;
}


console.log('is perm 123aa 123455', isPermutation('123aa', '123455'))
console.log('is perm 123 123455', isPermutation('123', '123455'))
console.log('is perm bbca abcdefbbcadd', isPermutation('cadd', 'abcdefbbcadd'))