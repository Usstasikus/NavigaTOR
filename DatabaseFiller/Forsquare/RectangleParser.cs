using System;
using System.Collections;
using System.Collections.Generic;

namespace Forsquare
{
    class RectangleParser : IEnumerable<Point>
    {
        static Random rnd;
        Rect rect;
        double radius;
        public RectangleParser(Rect rect, double radius)
        {
            Rect = rect;
            Radius = radius;
            rnd = new Random();
        }
        public Rect Rect {
            get => rect;
            set => rect = value;
        }
        public double Radius {
            get => radius;
            set => radius = value;
        }
        public IEnumerator<Point> GetEnumerator()
        {
            var areas = new Queue<Rect>();
            areas.Enqueue(rect);

            while (areas.Count != 0)
            {
                var rect = areas.Dequeue();
                var point = new Point(rnd.NextDouble() * rect.Width + rect.Point.X,
                                        rnd.NextDouble() * rect.Height + rect.Point.Y);
                var pRect = new Rect(point.Offset(-radius, -radius), radius, radius);
                var rects = new Rect[4];
                rects[0] = Rect.GetRect(rect.Point, pRect.Point);
                rects[1] = Rect.GetRect(rect.GetUpRight(), pRect.GetUpRight());
                rects[2] = Rect.GetRect(rect.GetDownLeft(), pRect.GetDownLeft());
                rects[3] = Rect.GetRect(rect.GetDownRight(), pRect.GetDownRight());
                foreach (var r in rects)
                    if (r.Height > radius && r.Width > radius)
                        areas.Enqueue(r);

                yield return point;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
    }
}
