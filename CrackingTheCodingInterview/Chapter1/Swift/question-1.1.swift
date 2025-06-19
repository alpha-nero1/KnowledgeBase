// Could extend string, choosing not to.

/**
 * Is unique function, does not leverage a hashtable.
 */
func isUnique(value: String) -> Bool {
  if (value.count != 0) {
    let lastIndex = value.count - 1;
    var currentIndex = 0;
    for char in value {
      let subStr = value.suffix(lastIndex - currentIndex);
      for subChar in subStr {
        if (subChar == char) {
          return false;
        }
      }
      currentIndex = currentIndex + 1;
    }
  }
  return true;
}

/**
 * Is unique function with hastable.
 */
func isUniqueWithHashtable(value: String) -> Bool {
  if (value.count != 0) {
    var charDict: [String: String] = [:]
    for char in value {
      let charStr = String(char)
      if let _ = charDict[charStr] {
        return false;
      }
      charDict[charStr] = charStr;
    }
  }
  return true;
} 

print("is unqiue 123456a: " + isUnique(value: "123456a").description);
print("is unqiue 1223456a: " + isUnique(value: "1223456a").description);
print("is unqiue with hashtable 123456a: " + isUniqueWithHashtable(value: "123456a").description);
print("is unqiue with hashtable 1233456a: " + isUniqueWithHashtable(value: "1233456a").description);