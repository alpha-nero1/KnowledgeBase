using System;

namespace question_7._8
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);
            board.PlacePiece(2, 2, true);
            board.PlacePiece(2, 3, false);
            board.PlacePiece(2, 4, true);

            board.PlacePiece(3, 3, false);
            board.PlacePiece(3, 4, true);
            board.PlacePiece(3, 5, false);
            board.PlacePiece(1, 4, false);
            board.Display();
        }
    }
}
