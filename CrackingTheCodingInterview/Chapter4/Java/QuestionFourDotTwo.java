// Create a minimal binary search tree from an array of sorted numbers.
// Exec use: javac ../../Core/Java/ArrayUtils.java QuestionFourDotTwo.java

class BinaryNode {
  public int value;
  public BinaryNode left;
  public BinaryNode right;

  BinaryNode(int value) {
    this.value = value;
  }

  public String toString() {
    return this.value + " (l: " + this.leftValue() + ", r: " + this.rightValue() + ")";
  }

  public String leftValue() {
    if (this.left != null) {
      return "" + this.left.value;
    }
    return "";
  }

  public String rightValue() {
    if (this.right != null) {
      return "" + this.right.value;
    }
    return "";
  }
}

// How it works: We know that for the root to be the middle value (all left is lower and all right is greater)
// the root should be arr[middle] we can then recursively make this division on either side to build the
// tree. so the code looks like this:
/**
Node buildNode(arr, start, end) {
  int m = (start + end) / 2;
  Node n = new Node(arr[m])
  n.left = buildNode(arr, start, m - 1);
  n.right = buildNode(arr, m + 1, end);
}

So given array like: { 0, 2, 5, 7, 8, 9, 11, 14, 18, 22, 23, 35 }

Our stack will look like:
Creating node for: 9 s: 0 e: 11
Creating node for: 5 s: 0 e: 4
Creating node for: 0 s: 0 e: 1
Creating node for: 2 s: 1 e: 1
Creating node for: 7 s: 3 e: 4
Creating node for: 8 s: 4 e: 4
Creating node for: 18 s: 6 e: 11
Creating node for: 11 s: 6 e: 7
Creating node for: 14 s: 7 e: 7
Creating node for: 23 s: 9 e: 11
Creating node for: 22 s: 9 e: 9
Creating node for: 35 s: 11 e: 11
 */
class QuestionFourDotTwo {
  public static void main(String[] args) {
    int[] arr = { 0, 2, 5, 7, 8, 9, 11, 14, 18, 22, 23, 35 };
    BinaryNode root = QuestionFourDotTwo.BuildMinimalTree(arr, 0, arr.length - 1);
  }

  private static BinaryNode BuildMinimalTree(int[] arr, int startIndex, int endIndex) {
    if (arr != null && arr.length > 0 && !(endIndex < startIndex)) {
      int middleIndex = (startIndex + endIndex) / 2;
      System.out.println("Creating node for: " + arr[middleIndex] + " s: " + startIndex + " e: " + endIndex);
      BinaryNode root = new BinaryNode(arr[middleIndex]);
      // We have the middle node, now create its left and right.
      root.left = QuestionFourDotTwo.BuildMinimalTree(arr, startIndex, middleIndex - 1);
      root.right = QuestionFourDotTwo.BuildMinimalTree(arr, middleIndex + 1, endIndex);
      return root;
    }
    return null;
  }
}