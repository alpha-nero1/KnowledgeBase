#include<string>

using namespace std;

class BinaryNode {
  public:
    int value;
    BinaryNode* left;
    BinaryNode* right;
    BinaryNode(int value);
    string left_value();
    string right_value();
    string get_string();
    ~BinaryNode();
};
