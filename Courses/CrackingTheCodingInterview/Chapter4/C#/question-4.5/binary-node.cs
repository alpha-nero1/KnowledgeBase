using System.Text;

namespace C_ {
  class BinaryNode {
    public int value;
    public BinaryNode left;
    public BinaryNode right;
    public BinaryNode(int value) {
      this.value = value;
    }

    public string leftValue() {
      if (this.left != null) {
        return this.left.value.ToString();
      }
      return "";
    }

    public string rightValue() {
      if (this.right != null) {
        return this.right.value.ToString();
      }
      return "";
    }

    public override string ToString() {
      string str = "[BinaryNode value:";
      str = $"{str}{this.value.ToString()} l:{this.leftValue()} r:{this.rightValue()}]";
      return str;
    }
  }
}
