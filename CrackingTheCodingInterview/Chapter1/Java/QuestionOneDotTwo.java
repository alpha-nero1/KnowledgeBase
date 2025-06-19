import java.util.Hashtable;
import java.util.Set;

public class QuestionOneDotTwo {
  public static void main(String[] args) {
    System.out.println("IsPermutation dogger abcefd " + QuestionOneDotTwo.IsPermutation("dogger", "abcefd"));
    System.out.println("IsPermutation abcdef abcefd " + QuestionOneDotTwo.IsPermutation("abcdef", "abcefd"));
  }

  private static void IncrementCharCountStore(Character character, Hashtable<Character, Integer> charStore) {
    if (character != null && charStore != null) {
      if (charStore.containsKey(character)) {
        // If the param was not properly specified with its generics, the get would think
        // it was returning an object and not compile...
        charStore.put(character, (charStore.get(character) + 1));
      } else {
        charStore.put(character, 1);
      }
    }
  }

  private static Hashtable<Character, Integer> BuildCharCountStore(String value) {
    Hashtable<Character, Integer> charCountStore = new Hashtable<Character, Integer>();
    for (int i = 0; i < value.length(); i++) {
      QuestionOneDotTwo.IncrementCharCountStore(value.charAt(i), charCountStore);
    }
    return charCountStore;
  }

  private static boolean IsPermutation(String firstValue, String secondValue) {
    if (
      firstValue != null &&
      secondValue != null && 
      firstValue.length() > 0 &&
      secondValue.length() > 0
    ) {
      if (firstValue.length() == secondValue.length()) {
        // Okay we need to buid the hash table.
        Hashtable<Character, Integer> firstCharCountStore = QuestionOneDotTwo.BuildCharCountStore(firstValue);
        Hashtable<Character, Integer> secondCharCountStore = QuestionOneDotTwo.BuildCharCountStore(secondValue);
        // Check counts are identical.
        Object[] firstChars = firstCharCountStore.keySet().toArray();
        for (int i = 0; i < firstChars.length; i++) {
          Character charKey = firstChars[i].toString().charAt(0);
          if (!secondCharCountStore.containsKey(charKey)) {
            return false;
          } else {
            if (firstCharCountStore.get(charKey) != secondCharCountStore.get(charKey)) {
              return false;
            }
          }
        }
        return true;
      }
      return false;
    }
    return false;
  }
}