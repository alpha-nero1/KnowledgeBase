import java.util.*;

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

class QuestionFourDotNine {
  public static void main(String[] args) {
    BinaryNode root = new BinaryNode(6);
    root.right = new BinaryNode(8);
    root.left = new BinaryNode(3);
    root.left.left = new BinaryNode(1);
    ArrayList<LinkedList<Integer>> sequences = QuestionFourDotNine.AllSequences(root);
    for (LinkedList<Integer> seq : sequences) {
      System.out.println(seq);
    }
  }

  public static ArrayList<LinkedList<Integer>> AllSequences(BinaryNode n) {
    ArrayList<LinkedList<Integer>> res = new ArrayList<LinkedList<Integer>>();
    if (n == null) {
      res.add(new LinkedList<Integer>());
      return res;
    }
    LinkedList<Integer> prefix = new LinkedList<Integer>();
    prefix.add(n.value);
    // Recurse on left nand right subtrees.
    ArrayList<LinkedList<Integer>> leftSeq = QuestionFourDotNine.AllSequences(n.left);
    ArrayList<LinkedList<Integer>> rightSeq = QuestionFourDotNine.AllSequences(n.right);
    System.out.println(leftSeq);
    System.out.println(rightSeq);
    // Weave together each list from left and right side.
    for (LinkedList<Integer> left : leftSeq) {
      for (LinkedList<Integer> right: rightSeq) {
        ArrayList<LinkedList<Integer>> weave = new ArrayList<LinkedList<Integer>>();
        QuestionFourDotNine.WeaveLists(left, right, weave, prefix);
        res.addAll(weave);
      }
    }
    return res;
  }

  /**
   * Weave two linked lists together in all possible ways.
   */
  public static void WeaveLists(
    LinkedList<Integer> first, 
    LinkedList<Integer> second,
    ArrayList<LinkedList<Integer>> results,
    LinkedList<Integer> prefix
    ) {
    if (first.size() == 0 || second.size() == 0) {
      // One list is empty, add remainder to a cloned prefix and store the result.
      LinkedList<Integer> result = (LinkedList<Integer>)prefix.clone();
      result.addAll(first);
      result.addAll(second);
      results.add(result);
      return;
    }
    // Recurse with head of first added to the prefix, removing the head will damage first so
    // we need to put it back where we found it afterwards.
    int hFirst = first.removeFirst();
    prefix.addLast(hFirst);
    QuestionFourDotNine.WeaveLists(first, second, results, prefix);
    prefix.removeLast();
    first.addFirst(hFirst);
    // Do the same with second, damaginf and restoring the list.
    int hSecond = second.removeFirst();
    prefix.addLast(hSecond);
    QuestionFourDotNine.WeaveLists(first, second, results, prefix);
    prefix.removeLast();
    second.addFirst(hSecond);
  }
}