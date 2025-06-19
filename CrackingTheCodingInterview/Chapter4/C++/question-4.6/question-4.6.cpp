// g++ binary-node.cpp question-4.6.cpp && ./a.out OR g++ *.cpp -o output
#include "binary-node.cpp" // Include .cpp here not .h
#include <iostream>

BinaryNode* leftmost_child(BinaryNode* n) {
  if (n == NULL) {
    return NULL;
  }
  while (n->left != NULL) {
    n = n->left;
  }
  return n;
}

// Return left most of right subtree, if no subtree return highest parent up the chain.
BinaryNode* inorder_successor(BinaryNode* n) {
  if (n == NULL) {
    return NULL;
  }
  if (n->right != NULL) {
    // We found a right subtree ret leftmost of that tree.
    return leftmost_child(n->right);
  } else {
    BinaryNode* q = n;
    BinaryNode* parent = n->parent;

    while (parent != NULL && parent->left != q) {
      // Go up the parent chain to check where is null and left is not this node.
      q = parent;
      parent = parent->parent;
    }
    return parent;
  }
}

int main() {
  BinaryNode* root = new BinaryNode(12);
  root->set_left(new BinaryNode(10));
  root->set_right(new BinaryNode(13));
  root->right->set_left(new BinaryNode(15));
  root->right->set_right(new BinaryNode(18));
  root->left->set_left(new BinaryNode(9));
  cout << "\nroot:" + root->get_string() + "\n";
  
  BinaryNode* succ = inorder_successor(root->right);
  if (succ != NULL) {
    cout << "\ninorder succ: " + succ->get_string() + "\n";
  } else {
    cout << "\ncould not find!";
  }
  return 0;
}