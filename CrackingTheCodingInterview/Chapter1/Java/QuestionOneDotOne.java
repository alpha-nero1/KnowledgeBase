import java.util.Hashtable;

public class QuestionOneDotOne {
  // Main to execute.
  public static void main(String[] args) {
    // Run the tests to verify the program.
    System.out.println("is unqiue 123456a: " + QuestionOneDotOne.IsUnique("123456a"));
    System.out.println("is unqiue 1234356ab: " + QuestionOneDotOne.IsUnique("1234356ab"));
    System.out.println("is unqiue with hashtable 123456a: " + QuestionOneDotOne.IsUniqueWithHashtable("123456a"));
    System.out.println("is unqiue with hashtable 1123456a: " + QuestionOneDotOne.IsUniqueWithHashtable("1123456a"));
  }

  /**
   * Is unique function. Does not use hashtable.
   */
  private static boolean IsUnique(String value) {
    if (value != null && value.length() > 0) {
      for (int i = 0; i < value.length(); i++) {
        char item = value.charAt(i);
        for (int j = i + 1; j < value.length(); j++) {
          if (value.charAt(j) == item) {
            return false;
          }
        }
      }
    }
    return true;
  }

  /**
   * Is unique with hash table function.
   */
  private static boolean IsUniqueWithHashtable(String value) {
    if (value != null && value.length() > 0) {
      Hashtable<Character, Boolean> charStore = new Hashtable<Character, Boolean>();
      for (int i = 0; i < value.length(); i ++) {
        char item = value.charAt(i);
        if (charStore.containsKey(item)) {
          return false;
        }
        charStore.put(item, true);
      }
    }
    return true;
  }
}