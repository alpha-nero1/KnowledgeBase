// Dominos.
/*
There is an 8x8 chessboard in which two diagonally opposite corners have been cut off. 
You are given 31 dominos, and a single domino can cover exactly two squares. 
Can you use the 31 dominos to cover the entire board? 
Prove your answer (by providing an example or showing why it's impossible).
*/

class QuestionSixDotThree {
  public static void main(String[] args) {
    boolean enough = QuestionSixDotThree.EnoughSquaresForDominos(8, 2, 31);
    System.out.println("EnoughSquaresForDominos(8, 2, 31): " + enough);
    boolean enough2 = QuestionSixDotThree.EnoughSquaresForDominos(8, 0, 31);
    System.out.println("EnoughSquaresForDominos(8, 0, 31): " + enough2);
    // Is true because we need at least 31 black and 31 white.
    boolean enough3 = QuestionSixDotThree.EnoughSquaresForDominos(8, 1, 31);
    System.out.println("EnoughSquaresForDominos(8, 1, 31): " + enough3);
  }

  public static boolean EnoughSquaresForDominos(
    int edgeLength, 
    int whiteRemoved,
    int dominos
  ) {
    int allSquares = edgeLength * edgeLength;
    // array of white = 32, black = 32.
    int[] colourBreakup = { allSquares / 2, allSquares / 2 };
    colourBreakup[0] -= whiteRemoved;
    for (int i = 0; i < dominos; i++) {
      // Remember: each domino MUST take exactly one black and one white square.
      colourBreakup[0] -= 1;
      colourBreakup[1] -= 1;
      if (colourBreakup[0] == -1 || colourBreakup[1] == -1) {
        return false;
      }
    }
    return true;
  }
}