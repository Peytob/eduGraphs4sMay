using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Graphs
{
    public class GraphControlRect : GraphControl
    {
        public GraphControlRect(float _delta, int _h, int _vertexes) : base(_delta, _h, _vertexes)
        {

        }
        // Конвертирует высоту в площадь
        public override float S()
        {
            return m_startHeight * m_startHeight;
        }
        public override float S(float _h)
        {
            return SStatic(_h);
        }
        static public float SStatic(float _h)
        {
            return _h * _h;
        }
        public override float H()
        {
            return m_startHeight;
        }
        public override float H(float _s)
        {
            return HStatic(_s);
        }
        static public float HStatic(float _s)
        {
            return MathF.Sqrt(_s);
        }
        protected override void DrawBorders()
        {
            // Пусто, тк весь квадрат - поле для размещения вершин
        }

        /* Функции проверки и генерации графа */

        protected override Point GetRandomPoint()
        {
            for (int i = 0; i < 25; ++i)
            {
                int x = m_random.Next(0, m_quadreSizes);
                int y = m_random.Next(0, m_quadreSizes);
                Point point = new Point(x, y);
                if (isPointCurrect(point))
                    return point;
            }

            throw new InvalidOperationException();
        }
        protected override (Point, Point) GetRandomGarbage()
        {
            return (new Point(0, 0), new Point(0, 0));
        }
    }
}
