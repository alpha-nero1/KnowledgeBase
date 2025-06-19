
func buildChatCountStore(value: String) -> [String: Int] {
  var newChatCountStore: [String: Int] = [:];
  for char in value {
    let charStr = String(char)
    // "In normal usage there's no difference, they are interchangeable. .None is an enum representation of the nil (absence of) value implemented by the Optional<T> enum."
    if (newChatCountStore[charStr] != nil) {
      // Needed to coallesce here because swift complains. It likes var security!
      newChatCountStore[charStr] = (newChatCountStore[charStr] ?? 0) + 1;
    } else {
      newChatCountStore[charStr] = 1;
    }
  }
  return newChatCountStore;
}

func isPermutation(firstValue: String, secondValue: String) -> Bool {
  if (firstValue.count > 0 && secondValue.count > 0) {
    let firstValueCharStore = buildChatCountStore(value: firstValue);
    let secondValueCharStore = buildChatCountStore(value: secondValue);
    for (char, count) in firstValueCharStore {
      let charStr = String(char)
      if count != secondValueCharStore[charStr] {
        return false;
      }
    }
    return true;
  }
  return false;
}

print("isPermutation 1234 1234", isPermutation(firstValue: "1234", secondValue: "1234"))
print("isPermutation 1234ab 12ab34", isPermutation(firstValue: "1234ab", secondValue: "12ab34"))
print("isPermutation 1234abb 12ab34", isPermutation(firstValue: "1234abb", secondValue: "12ab34"))