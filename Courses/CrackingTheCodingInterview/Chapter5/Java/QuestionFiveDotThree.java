class QuestionFiveDotThree {
  public static void main(String[] args) {
    System.out.println("FindLongestSequence (15) = " + QuestionFiveDotThree.FindLongestSequence(15));
    System.out.println("FindLongestSequence (63) = " + QuestionFiveDotThree.FindLongestSequence(63));
  }

  public static int FindLongestSequence(int value) {
    if (~value == 0) return Integer.BYTES * 8;
    int currentLen = 0; // 00110(1)00 currentLen = 1, prev = 2
    int previousLen = 0;
    int maxLen = 1;
    while (value != 0) {
      if ((value & 1) == 1) { // Found 1.
        currentLen += 1; // All we need to do is increment currentLen
      } else if ((value & 1) == 0) { // Found 0.
        // We need to now set the previous length and reset the current length because we found a break.
        // Now the previous length would generally be the current length now, however if we know we also do not
        // even have the next bit, we can set previousLen to 0 because the absence of the next bit would kill that
        // sequence anyway.
        previousLen = (value & 2) == 0 ? 0 : currentLen;
        currentLen = 0;
      }
      // RE: we only add the + 1 here because the question denotes we get to fip one bit to make a longer sequence!
      maxLen = Math.max(previousLen + currentLen + 1, maxLen);
      value >>>= 1; // Statement that ensures while does not run forever (a is equal to a right shift of itself).
    }
    return maxLen;
  }
}