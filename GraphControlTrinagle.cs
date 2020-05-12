using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Drawing;

namespace Graphs
{
    class GraphControlTrinagle : GraphControl
    {
        protected List<Line> m_lines;
        public GraphControlTrinagle(float _delta, int _h, int _vertexes) : base(_delta, _h, _vertexes)
        {
            m_lines = new List<Line>
            {
                // Левая сторона
                new Line()
                {
                    X1 = 0,
                    Y1 = m_quadreSizes,
                    X2 = m_quadreSizes / 2,
                    Y2 = 0,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                },

                // Правая сторона
                new Line()
                {
                    X1 = m_quadreSizes,
                    Y1 = m_quadreSizes,
                    X2 = m_quadreSizes / 2,
                    Y2 = 0,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                },

                // Низ
                new Line()
                {
                    X1 = m_quadreSizes,
                    Y1 = m_quadreSizes,
                    X2 = 0,
                    Y2 = m_quadreSizes,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                }
            };
        }

        public override float H()
        {
            return m_startHeight;
        }

        static public float HStatic(float _s)
        {
            return (int) MathF.Sqrt(2 * _s);
        }

        public override float H(float _s)
        {
            return HStatic(_s);
        }

        public override float S()
        {
            return 0.5f * m_startHeight * m_startHeight;
        }

        static public float SStatic(float _h)
        {
            return 0.5f * _h * _h;
        }

        public override float S(float _h)
        {
            return SStatic(_h);
        }

        protected override void DrawBorders()
        {
            foreach (Line line in m_lines)
                RenderGraphImage.Children.Add(line);
        }

        protected override (Point, Point) GetRandomGarbage()
        {
            throw new NotImplementedException();
        }

        protected override Point GetRandomPoint()
        {
            for (int i = 0; i < 25; ++i)
            {
                // Немного смещаю генерацию вниз.
                int y = m_random.Next((m_random.NextDouble() > 0.333) ? m_quadreSizes / 3 : 0, m_quadreSizes);
                int x = m_random.Next((m_quadreSizes - y) / 2, (m_quadreSizes + y) / 2);
                Point point = new Point(x, y);
                if (isPointCurrect(point))
                    return point;
            }

            throw new InvalidOperationException();
        }
    }
}
