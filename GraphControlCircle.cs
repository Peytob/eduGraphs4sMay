using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Drawing;

namespace Graphs
{
    class GraphControlCircle : GraphControl
    {
        public GraphControlCircle(float _delta, int _h, int _vertexes) : base(_delta, _h, _vertexes)
        {

        }

        protected override void DrawBorders()
        {
            Ellipse ellipse = new Ellipse()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Fill = Brushes.White,
                Height = m_quadreSizes,
                Width = m_quadreSizes
            };

            RenderGraphImage.Children.Add(ellipse);
        }

        public override float H()
        {
            return m_startHeight;
        }

        static public float HStatic(float _s)
        {
            return MathF.Sqrt(_s / MathF.PI);
        }

        public override float H(float _s)
        {
            return HStatic(_s);
        }

        public override float S()
        {
            return (float) Math.PI * m_startHeight * m_startHeight;
        }

        public override float S(float _h)
        {
            return SStatic(_h);
        }

        static public float SStatic(float _h)
        {
            return (float) Math.PI * _h * _h;
        }

        protected override (Point, Point) GetRandomGarbage()
        {
            throw new NotImplementedException();
        }

        protected override Point GetRandomPoint()
        {
            for (int i = 0; i < 25; ++i)
            {
                int r = m_random.Next(0, m_quadreSizes / 2);
                double theta = m_random.NextDouble();

                int x = m_quadreSizes / 2 + (int) (r * MathF.Cos((float) theta * 2 * MathF.PI));
                int y = m_quadreSizes / 2 + (int) (r * MathF.Sin((float) theta * 2 * MathF.PI));
                Point point = new Point(x, y);

                if (isPointCurrect(point))
                    return point;
            }

            throw new InvalidOperationException();
        }
    }
}
