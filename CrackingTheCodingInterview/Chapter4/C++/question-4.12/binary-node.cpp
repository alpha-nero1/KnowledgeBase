#include<string>

using namespace std;

class BinaryNode {
  public:
    int value;

    BinaryNode* left;

    BinaryNode* right;

    BinaryNode* parent;

    BinaryNode(int value) {
      this->value = value;
    }

    void set_left(BinaryNode * left) {
      this->left = left;
      if (left != NULL) {
        this->left->parent = this;
      }
    }

    void set_right(BinaryNode * right) {
      this->right = right;
      if (right != NULL) {
        this->right->parent = this;
      }
    }

    string left_value() {
      if (left != NULL) {
        return to_string(left->value);
      }
      return "NULL";
    }

    string right_value() {
      if (right != NULL) {
        return to_string(right->value);
      }
      return "NULL";
    }

    string get_string() {
      return "[BinaryNode value:" + to_string(value) + ", l:" + left_value() + ", r:" + right_value() + "]";
    }

    ~BinaryNode() {
      delete this->left;
      delete this->right;
    }
};
