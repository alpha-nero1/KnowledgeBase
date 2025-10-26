// g++ question-6.5.cpp -o out && ./out

#include <iostream>

using namespace std;

int* tip_jug_one_into_two(int jug_one, int jug_two, int vol_one, int vol_two) {
  int res[2];
  int amount_of_jug_two_empty = vol_two - jug_two;
  if (jug_one > vol_two) {
    jug_one -= vol_two;
    jug_two += vol_two;
  } else {
    jug_two += jug_one;
    jug_one = 0;
  }
  // Correct for over spillage.
  if (jug_two > vol_two) {
    jug_two = vol_two;
    jug_one = vol_one - amount_of_jug_two_empty;
  }
  cout << "\n- New jug values: ";
  cout << jug_one;
  cout << " ";
  cout << jug_two;
  res[0] = jug_one;
  res[1] = jug_two;
  return res;
}

void print_jug_vals(int jug_one, int jug_two) {
  cout << "\n- New jug values: ";
  cout << jug_one;
  cout << " ";
  cout << jug_two;
}

bool jugs_of_water(int vol_one, int vol_two, int target_vol) {
  int jug_one, jug_two;
  int tries = 0;
  jug_one = 5; // 1
  jug_two = 0;
  print_jug_vals(jug_one, jug_two);

  while (tries < 10 && jug_one != target_vol) {
    // Insert jug one into jug two
    int* jugs = tip_jug_one_into_two(jug_one, jug_two, vol_one, vol_two);
    jug_one = jugs[0];
    jug_two = jugs[1];

    if (jug_one == target_vol) return true;

    if (jug_two == vol_two) {
      jug_two = 0;
      print_jug_vals(jug_one, jug_two);
    }

    int* jugs_two = tip_jug_one_into_two(jug_one, jug_two, vol_two, vol_two);
    jug_one = jugs_two[0];
    jug_two = jugs_two[1];

    if (jug_one == target_vol) return true;

    if (jug_one == 0 && tries < 2) {
      // Trick with this algorithm is that we only require complete two refills
      // of jug one!
      jug_one = vol_one;
    } else {
      jug_one = jug_two;
    }
    print_jug_vals(jug_one, jug_two);
    tries++;
  }

  return jug_one == target_vol;
}

int main() {
  bool res = jugs_of_water(5, 3, 4);
  cout << "\njugs_of_water(5, 3, 4) = ";
  cout << to_string(res);
  cout << "\n";
  return 0;
}