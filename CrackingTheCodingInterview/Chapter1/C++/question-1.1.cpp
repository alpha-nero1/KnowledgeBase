#include <iostream>
#include <bitset>

// Specifying this namespace means we do not need to prefix every
// single typing with std:: making the code more readable.
using namespace std;

bool is_unique(string value) {
  if (!value.empty()) {
    for (int i = 0; i < value.length(); i++) {
      for (int j = i + 1; j < value.length(); j++) {
        if (value[i] == value[j]) {
          return false;
        }
      }
    }
  }
  return true;
}

bool is_unique_with_hashtable(string value) {
  if (!value.empty()) {
    bitset<128> charStore;
    for (char character: value) {
      if (charStore[(int) character]) {
        return false;
      }
      charStore.flip((int) character);
    }
  }
  return true;
} 

int main() {
  // Makes sure that the output of true shoes "true" not 1.
  // `<< std::` flags are manipulators.
  cout << boolalpha;
  cout << "is unqiue abcdef: " << is_unique("abcdef") << endl;
  cout << "is unqiue abcddef: " << is_unique("abcddef") << endl;
  cout << "is unqiue with hashtable abcdef: " << is_unique_with_hashtable("abcdef") << endl;
  cout << "is unqiue with hashtable abcddef: " << is_unique_with_hashtable("abcddef") << endl;
}