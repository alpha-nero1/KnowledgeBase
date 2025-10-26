namespace question_17._11;

public class LocationPair
{
    public int LocOne, LocTwo;

    public LocationPair(int locOne, int locTwo)
    {
        Set(locOne, locTwo);
    }

    public int Distance()
    {
        return Math.Abs(LocOne - LocTwo);
    }

    public bool IsValid()
    {
        return LocOne >= 0 && LocTwo >= 0;
    }

    public void Set(int locOne, int locTwo)
    {
        LocOne = locOne;
        LocTwo = locTwo;
    }
}