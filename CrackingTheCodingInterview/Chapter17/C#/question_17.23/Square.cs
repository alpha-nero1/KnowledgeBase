namespace question_17._23;

class Square
{
    public int Row { get; set; }
    public int Col { get; set; }
    public int Length { get; set; }

    public Square(int row, int col, int length)
    {
        Row = row;
        Col = col;
        Length = length;
    }

    public override string ToString()
    {
        return $"[Row = {Row}, Col = {Col}, N = {Length}]";
    }
}
