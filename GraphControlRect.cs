using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Graphs
{
    public class GraphControlRect : GraphControl
    {
        public GraphControlRect(Graph _bindGraph, float _delta) : base(_bindGraph, _delta)
        {

        }

        /* Функции проверки и генерации графа */

        protected override Point GetRandomPoint()
        {
            int x = m_random.Next(m_maximalCircleSize / 2, m_quadreSizes - m_maximalCircleSize / 2);
            int y = m_random.Next(m_maximalCircleSize / 2, m_quadreSizes - m_maximalCircleSize / 2);
            return new Point(x, y);
        }
        protected override (Point, Point) GetRandomGarbage()
        {
            return (new Point(0, 0), new Point(0, 0));
        }
    }
}
