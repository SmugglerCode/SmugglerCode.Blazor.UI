public class Point
{
    public double X { get; private set; } = 0;
    public double Y { get; private set; } = 0;

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }
}
