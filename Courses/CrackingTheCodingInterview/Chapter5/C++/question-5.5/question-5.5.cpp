#include <iostream>

using namespace std;

bool is_power_of_two(int num) {
  // If we subtract 1 and we have no common bits, 
  // that means we got a sequence of 1s after num and it is a power of 2.
  return (num & (num - 1)) == 0;
}

int main() {
  cout << boolalpha;
  cout << "\nis_power_of_two(72) = ";
  cout << is_power_of_two(72);
  cout << "\nis_power_of_two(64) = ";
  cout << is_power_of_two(64);
  cout << "\nis_power_of_two(13) = ";
  cout << is_power_of_two(13);
  cout << "\nis_power_of_two(8) = ";
  cout << is_power_of_two(8);
  cout << "\n";

  return 0;
}