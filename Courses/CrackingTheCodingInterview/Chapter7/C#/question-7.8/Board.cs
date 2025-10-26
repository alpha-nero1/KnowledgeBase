using System;
using System.Collections.Generic;

namespace question_7._8
{
    public class Piece
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsBlack { get; set; }
    }


    public class Board
    {
        private Piece[,] _pieces;
        private int _width;
        private int _height;

        public Board(int x, int y)
        {
            _pieces = new Piece[x, y];
            _width = x;
            _height = y;
        }

        public void PlacePiece(int x, int y, bool isBlack)
        {
            if (_pieces[x, y] != null) throw new Exception("Cannot place piece here.");
            _pieces[x, y] = new Piece
            {
                IsBlack = isBlack,
                X = x,
                Y = y
            };
            CheckBoard(x, y, isBlack);
        }

        private void CheckBoard(int x, int y, bool isBlack)
        {
            // Wow!!
            // Legend! You got it!!
            // These ranges specify how long you can go each way on x and each way on y.
            int[] ranges = { -2, 2 };
            foreach (var range in ranges)
            {
                bool canMoveX = (x + range) > 0 && (x + range < _width);
                bool canMoveY = (y + range) > 0 && (y + range < _height);
                if (canMoveX)
                {
                    // Check x.
                    int hoppedEl = x + range;
                    var hoppedPiece = _pieces[hoppedEl, y];
                    if (hoppedPiece?.IsBlack == isBlack)
                    {
                        // Flip the middle guy!
                        var middleGuy = _pieces[x + (range / 2), y];
                        middleGuy.IsBlack = !middleGuy.IsBlack;
                    }
                }
                if (canMoveY)
                {
                    // Check y.
                    int hoppedEl = y + range;
                    var hoppedPiece = _pieces[x, hoppedEl];
                    if (hoppedPiece?.IsBlack == isBlack)
                    {
                        // Flip the middle guy!
                        var middleGuy = _pieces[x, y + (range / 2)];
                        middleGuy.IsBlack = !middleGuy.IsBlack;
                    }
                }
            }
        }

        public void Display()
        {
            int x = _width;
            int y = _height;
            int totalPieces = x * y;
            int row = 0;
            // Use that modulus.
            for (int i = 0; i < totalPieces; i++)
            {
                int col = i % x;
                var item = _pieces[col, row];
                if (item != null)
                {
                    Console.Write($"[Piece {item.IsBlack}], ");
                }
                else
                {
                    Console.Write("[Null      ], ");
                }

                if (col == x - 1)
                {
                    row++;
                    Console.WriteLine();
                }
            }
        }
    }
}