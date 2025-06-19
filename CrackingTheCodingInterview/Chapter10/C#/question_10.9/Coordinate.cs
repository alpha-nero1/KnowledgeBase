namespace question_10._9
{
    public interface ICloneable<T>
    {
        T Clone();
    }

    public class Coordinate : ICloneable<Coordinate>
    {
        public int row, col;

        public Coordinate(int row, int col)
        {
            row = row;
            col = col;
        }

        public bool IsInBounds(int[][] matrix) =>
        (
            row > 0
            && col > 0
            && col < matrix[0].Length
            && row < matrix.Length
        );

        public bool IsBefore(Coordinate p)
        {
            return row <= p.row && col <= p.col;
        }

        public void SetToAverage(Coordinate min, Coordinate max)
        {
            row = (min.row + max.row) / 2;
            col = (min.col + max.col) / 2;
        }

        public Coordinate Clone()
        {
            return new Coordinate(row, col);
        }
    }
}