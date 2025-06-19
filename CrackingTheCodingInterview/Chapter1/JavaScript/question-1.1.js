// Is unique string.

// Without additional datastructures.
// Runtime - O(n^2)
const isUnique = (value) => {
  if (value && value.length) {
    for (let i = 0; i < value.length; i++) {
      const item = value[i];
      for (let j = i + 1; j < value.length; j++) {
        if (value[j] === item) {
          return false;
        }
      }
    }
  }
  return true;
}

// With datastruct.
// Runtime - O(n)
const isUniqueWithHashTable = value => {
  if (value && value.length) {
    const strStore = new Map()
    for (let i = 0; i < value.length; i++) {
      const item = value[i];
      if (item && strStore.get(item)) {
        return false;
      }
      strStore.set(item, true)
    }
  }
  return true;
}

// Tests
console.log('is unqiue 123456a', isUnique('123456a'))
console.log('is unqiue 12233456a', isUnique('12233456a'))
console.log('is unqiue 123456a', isUniqueWithHashTable('123456a'))
console.log('is unqiue 1223456a', isUniqueWithHashTable('1223456a'))