using System;
using System.Collections;

namespace QuestionOne {
  class QuestionOne {
    public static void Main(string[] args) {
      // To concat we need to specify args like `{0} or {1} e.t.c..`
      Console.WriteLine("Is unique \"abcdefg\": {0}", QuestionOne.IsUnique("abcdefg"));
      Console.WriteLine("Is unique \"abccdefg\": {0}", QuestionOne.IsUnique("abccdefg"));
      Console.WriteLine("Is unique \"abcdefg\": {0}", QuestionOne.IsUniqueWithHashtable("abcdefg"));
      Console.WriteLine("Is unique \"abccdefg\": {0}", QuestionOne.IsUniqueWithHashtable("abccdefg"));
    }

    private static bool IsUnique(string value) {
      if (value != null && value.Length > 0) {
        for (var i = 0; i < value.Length; i++) {
            for (var j = i + 1; j < value.Length; j++) {
                if (value[i] == value[j]) {
                    return false;
                }
            }
        }
      }
      return true;
    }

    private static bool IsUniqueWithHashtable(string value) {
      if (value != null && value.Length > 0) {
        Hashtable charStore = new Hashtable();
        for (var i = 0; i < value.Length; i++) {
            if (charStore.ContainsKey(value[i])) {
                return false;
            }
            charStore.Add(value[i], true);
        }
      }
      return true;
    }
  }
}