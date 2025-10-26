using System.Text;

namespace question_4._11 {
  class BinaryNode {
    public int value;
    public BinaryNode left;
    public BinaryNode right;
    public BinaryNode parent;
    public int size = 0;
    public BinaryNode(int value) {
      this.value = value;
      this.size = 1;
    }

    public void setLeft(BinaryNode left) {
      this.left = left;
      this.left.parent = this;
      this.size += 1;
    }

    public void setRight(BinaryNode right) {
      this.right = right;
      this.right.parent = this;
      this.size += 1;
    }

    public BinaryNode getRandomNode() {
      int leftSize = this.left == null ? 0 : left.size;
      System.Random random = new System.Random();
      // Get a random value in the total size of the node.
      int index = random.Next(this.size);
      // If it is less than the left size, give left a go.
      if (index < leftSize) {
        return this.left.getRandomNode();
        // As we recurse down we are making sure that we find the item of the exact index
        // wether or not it falls on left or right side.
      } else if (index == leftSize) {
        return this;
      } else {
        // Else get a random from the right side.
        return this.right.getRandomNode();
      }
    }

    public void insert(int value) {
      if (value < this.value) {
        if (this.left == null) {
          this.left = new BinaryNode(value);
        } else {
          this.left.insert(value);
        }
      } else {
        if (this.right == null) {
          this.right = new BinaryNode(value);
        } else {
          this.right.insert(value);
        }
      }
      this.size += 1;
    }

    public BinaryNode find(int value) {
      if (value == this.value) {
        return this;
      }
      if (value <= this.value) {
        return left != null ? left.find(value) : null;
      }
      if (value > this.value) {
        return right != null ? right.find(value) : null;
      }
      return null;
    }

    public bool delete(int value) {
      BinaryNode nodeToDelete = this.find(value);
      if (nodeToDelete != null) {
        if (nodeToDelete.parent != null) {
          nodeToDelete.parent.size -= nodeToDelete.size;
          if (nodeToDelete.parent.value >= value) {
            // delete parents left
            nodeToDelete.parent.left = null;
          } else if (nodeToDelete.parent.value < value) {
            nodeToDelete.parent.right = null;
          }
          return true;
        }
      }
      return false;
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
