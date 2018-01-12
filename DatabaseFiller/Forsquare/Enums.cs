using System;

namespace Forsquare
{
    public struct Point
    {
        public readonly double X, Y;
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    public struct Rect
    {
        public readonly Point Point;
        public readonly double Width, Height;

        public Rect(Point point, double width, double height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("Rect invelid Width or Height");

            Point = point;
            Width = width;
            Height = height;
        }
        public static Rect GetRect(Point p1, Point p2)
        {
            return new Rect(
                new Point(Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y)),
                Math.Abs(p2.X - p1.X),
                Math.Abs(p2.Y - p1.Y)
            );
        }
        public static Rect GetRect(double minX, double minY, double maxX, double maxY)
        {
            return new Rect(
                new Point(minX, minY),
                maxX - minX,
                maxY - minY
            );
        }
    }

    public static class Methods
    {
        public static Point Offset(this Point point, double x, double y)
        {
            return new Point(point.X + x, point.Y + y);
        }

        public static Point OffsetX(this Point point, double x)
        {
            return new Point(point.X + x, point.Y);
        }

        public static Point OffsetY(this Point point, double y)
        {
            return new Point(point.X, point.Y + y);
        }

        public static Point GetUpRight(this Rect rect)
        {
            return new Point(rect.Point.X + rect.Width, rect.Point.Y);
        }

        public static Point GetDownLeft(this Rect rect)
        {
            return new Point(rect.Point.X, rect.Point.Y + rect.Height);
        }

        public static Point GetDownRight(this Rect rect)
        {
            return new Point(rect.Point.X + rect.Width, rect.Point.Y + rect.Width);
        }
    }
    
}
