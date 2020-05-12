using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Security.Cryptography;

namespace Graphs
{
    class SegmentWorker
    {
        private SegmentWorker()
        { }

        // Swap, ты редиска!
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Swap<T>(ref T v1, ref T v2)
        { 
            T v3 = v1;
            v1 = v2;
            v2 = v3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Swap(ref Point point)
        {
            int v3 = point.X;
            point.X = point.Y;
            point.Y = v3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int Area(Point a, Point b, Point c)
        {
            return (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
        }

        static bool IntersectOther(Point a, Point b)
        {
            // Swap, ты редиска!
            if (a.X > a.Y)
                Swap(ref a);
            
            if (b.X > b.Y)
                Swap(ref a);

            return Math.Min(a.X, b.X) <= Math.Min(a.Y, b.Y);
        }

        static bool IntersectOther(int a, int b, int c, int d)
        {
            if (a > b)
                Swap(ref a, ref b);

            if (c > d)
                Swap(ref c, ref d);

            return Math.Max(a, c) <= Math.Min(b, d);
        }

        public static bool Intersect(Point a, Point b, Point c, Point d)
        {
            return IntersectOther(a.X, b.X, c.X, d.X) &&
                   IntersectOther(a.Y, b.Y, c.Y, d.Y) &&
                   Area(a, b, c) * Area(a, b, d) <= 0 &&
                   Area(c, d, a) * Area(c, d, b) <= 0;
        }
    }
}
