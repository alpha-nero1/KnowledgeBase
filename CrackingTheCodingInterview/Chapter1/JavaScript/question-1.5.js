// One Away
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

const isOneOrLessEditsAway = (firstValue, secondValue) => {
  if (
    firstValue && 
    secondValue &&
    typeof firstValue === 'string' &&
    typeof secondValue === 'string'
  ) {
    if (firstValue === secondValue) {
      return true;
    }
    // Lets track how many edits are needed for the strings to be the same.
    let editsRequired = 0;
    let firstCharStoreIteratorDone = false;
    // Count chars in both strings in a map.
    const firstCharStore = buildCharStore(firstValue);
    const secondCharStore = buildCharStore(secondValue);
    // Pinch the iterator out.
    const firstCharStoreIterator = firstCharStore.entries();
    while (!firstCharStoreIteratorDone && editsRequired < 2) {
      const nextValue = firstCharStoreIterator.next();
      if (nextValue.value) {
        const [firstKey, firstCount] = nextValue.value;
        const secondCount = secondCharStore.get(firstKey);
        // Get the absolute difference in char counts.
        let editsNeededToMatchChar = Math.abs((firstCount || 0) - (secondCount || 0));
        // Add to the edits needed value.
        editsRequired += editsNeededToMatchChar;
      }
      firstCharStoreIteratorDone = nextValue.done;
    }
    return editsRequired < 2;
  }
  return false;
}

console.log('isOneOrLessEditsAway pleb ple', isOneOrLessEditsAway('pleb', 'ple'))
console.log('isOneOrLessEditsAway plebb plea', isOneOrLessEditsAway('plebb', 'plea'))
console.log('isOneOrLessEditsAway pale ple', isOneOrLessEditsAway('pale', 'ple'))
console.log('isOneOrLessEditsAway pales pale', isOneOrLessEditsAway('pales', 'pale'))
console.log('isOneOrLessEditsAway pale bale', isOneOrLessEditsAway('pale', 'bale'))
console.log('isOneOrLessEditsAway pale bake', isOneOrLessEditsAway('pale', 'bake'))