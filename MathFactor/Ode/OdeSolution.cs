

namespace MathFactor.Ode
{
    public class OdeSolution
    {
        public Function[] Functions { get; set; }
    }

    public class Function
    {
        public Point[] Points { get; set; }
    }

    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Point()
        {
        }
    }
}