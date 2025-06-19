// g++ binary-node.cpp question-4.12.cpp && ./a.out OR g++ *.cpp -o output && ./output
#include "binary-node.cpp" // Include .cpp here not .h
#include <iostream>
#include <map>

using namespace std;

// Start brute force solution.

int count_paths_with_sum_brute(BinaryNode* node, int target_sum, int running_sum) {
  if (node == NULL) return 0;
  int total_paths = 0;
  running_sum += node->value;
  if (running_sum == target_sum) {
    total_paths += 1;
  }
  total_paths += count_paths_with_sum_brute(node->left, target_sum, running_sum);
  total_paths += count_paths_with_sum_brute(node->right, target_sum, running_sum);
  return total_paths;
}

int count_paths_with_sum_brute(BinaryNode* root, int target_sum) {
  if (root == NULL) {
    return 0;
  }
  int paths_from_root = count_paths_with_sum_brute(root, target_sum, 0);
  int left_paths = count_paths_with_sum_brute(root->left, target_sum, 0);
  int right_paths = count_paths_with_sum_brute(root->right, target_sum, 0);
  return paths_from_root + left_paths + right_paths;
}

// Start optimal solution.

void increment_map(map<int, int> path_map, int key, int delta) {
  int new_count = path_map[key];
  new_count += delta;
  if (new_count == 0) {
    path_map.erase(key);
  } else {
    path_map[key] = new_count;
    cout << "\nSave new count " + to_string(path_map[key]);
  }
}

int count_paths_with_sum(BinaryNode* root, int target_sum, int running_sum, map<int, int> path_map) {
  if (root == NULL) return 0;
  // Else we have node.
  // Immediately increment the running sum.
  running_sum += root->value;
  // Get the "prev_sum_key" a link to previous work done. 
  int prev_sum_key = running_sum - target_sum;
  int total_paths = path_map[prev_sum_key]; // Was there a prior log of total paths for this unique key?
  if (running_sum == target_sum) { // Now in this instance if running sum = target increment the paths.
    total_paths++;
  }
  
  increment_map(path_map, running_sum, 1);
  total_paths += count_paths_with_sum(root->left, target_sum, running_sum, path_map);
  total_paths += count_paths_with_sum(root->right, target_sum, running_sum, path_map);
  increment_map(path_map, running_sum, -1);
  return total_paths;
}

int count_paths_with_sum(BinaryNode* root, int target_sum) {
  map<int, int> sum_map;
  return count_paths_with_sum(root, target_sum, 0, sum_map);
}

int main() {
  BinaryNode* root = new BinaryNode(10);
  root->set_left(new BinaryNode(5));
  root->set_right(new BinaryNode(-3));
  root->left->set_left(new BinaryNode(3));
  root->left->set_right(new BinaryNode(1));
  root->left->left->set_left(new BinaryNode(3));
  root->left->left->set_right(new BinaryNode(-2));
  root->right->set_right(new BinaryNode(11));
  cout << "\nRisultato: " + to_string(count_paths_with_sum_brute(root, 8)) + " \n";
  return 0;
}

/*
IN: 16.

Running sum now: 10
Sum now: -6
PATHS: 0

Save new count 1
Running sum now: 15
Sum now: -1
PATHS: 0

Save new count 1
Running sum now: 18
Sum now: 2
PATHS: 0

Save new count 1
Running sum now: 21
Sum now: 5
PATHS: 0

Save new count 1
Save new count -1
Running sum now: 16
Sum now: 0
PATHS: 0

Save new count 1
Save new count -1
Save new count -1
Running sum now: 16
Sum now: 0
PATHS: 0

Save new count 1
Save new count -1
Save new count -1
Running sum now: 7
Sum now: -9
PATHS: 0

Save new count 1
Running sum now: 18
Sum now: 2
PATHS: 0

Save new count 1
Save new count -1
Save new count -1
Save new count -1
Risultato: 2 

*/