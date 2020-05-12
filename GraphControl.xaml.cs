using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Graphs
{
    /// <summary>
    /// Логика взаимодействия для GraphControl.xaml
    /// </summary>
    public abstract partial class GraphControl : UserControl
    {
        private Graph graph;

        protected Graph Graph { get => graph; set => graph = value; }

        protected bool m_renderDeltaRadius;

        protected int m_quadreSizes;
        protected Random m_random;

        protected readonly int m_maximalCircleSize = 20;
        protected float m_delta;
        protected int m_vertexes;

        protected Line gar = new Line
        {
            X1 = 100,
            Y1 = 250,

            X2 = 150,
            Y2 = 300,

            Stroke = Brushes.Green,
            StrokeThickness = 5
        };

        protected readonly int m_startHeight;

        public GraphControl(float _delta, int _h, int _vertexes)
        {
            InitializeComponent();

            Graph = new Graph();
            m_random = new Random();
            m_startHeight = _h;
            m_quadreSizes = m_startHeight;
            m_delta = _delta;
            m_renderDeltaRadius = false;
            m_vertexes = _vertexes;

            RenderGraphImage.Width = RenderGraphImage.Height = m_startHeight;
        }
        public abstract float S();
        public abstract float S(float _s);
        public abstract float H();
        public abstract float H(float _h);
        public bool DeltaShow
        {
            get => m_renderDeltaRadius;
        }
        public void GraphRenderUpdate()
        {
            RenderGraphImage.Children.Clear();

            DrawBorders();

            foreach (Graph.Edge e in Graph.Edges)
            {
                Line line = new Line
                {
                    X1 = Graph.GetVertex(e.first).x,
                    Y1 = Graph.GetVertex(e.first).y,
                    X2 = Graph.GetVertex(e.second).x,
                    Y2 = Graph.GetVertex(e.second).y,
                    Stroke = Brushes.Red,
                    StrokeThickness = 5
                };

                RenderGraphImage.Children.Add(line);
            };

            RenderGraphImage.Children.Add(gar);

            foreach (Graph.Vertex v in Graph.Vertexes)
            {
                Canvas mainCanvas = new Canvas
                {
                    Width = m_maximalCircleSize,
                    Height = m_maximalCircleSize
                };

                RenderGraphImage.Children.Add(mainCanvas);

                Ellipse childCtrl = new Ellipse
                {
                    Fill = Brushes.Blue,
                    Width = m_maximalCircleSize,
                    Height = m_maximalCircleSize
                };

                Canvas.SetTop(childCtrl, v.y - m_maximalCircleSize / 2);
                Canvas.SetLeft(childCtrl, v.x - m_maximalCircleSize / 2);

                mainCanvas.Children.Add(childCtrl);

                if (m_renderDeltaRadius)
                {
                    System.Windows.Media.Color color = System.Windows.Media.Color.FromArgb(80, 80, 80, 160);
                    childCtrl = new Ellipse
                    {
                        Fill = new SolidColorBrush(color),
                        Width = 2 * m_delta,
                        Height = 2 * m_delta
                    };

                    Canvas.SetTop(childCtrl, v.y - m_delta);
                    Canvas.SetLeft(childCtrl, v.x - m_delta);

                    mainCanvas.Children.Add(childCtrl);
                }
            }
        }
        protected abstract void DrawBorders();
        public void GenerateGraph()
        {
            Graph = new Graph();

            try
            {
                for (int i = 0; i < m_vertexes; ++i)
                    Graph.AddVertex(new Graph.Vertex(GetRandomPoint(), i.ToString()));
            }

            catch (InvalidOperationException exp)
            {
                System.Windows.MessageBox.Show("Хммм. Вы ввели слишком много вершин (кто-то не поместился)! Уменьшите их размер или количество!");
                return;
            }

            for (int i = 0; i < Graph.Vertexes.Count; ++i)
            {
                Graph.Vertex v1 = Graph.GetVertex(i.ToString());

                for (int j = i + 1; j < Graph.Vertexes.Count; ++j)
                {
                    Graph.Vertex v2 = Graph.GetVertex(j.ToString());

                    // Проверка на пересечения с мусором
                    if (SegmentWorker.Intersect(new Point(v1.x, v1.y), new Point(v2.x, v2.y), new Point((int) gar.X1, (int) gar.Y1), new Point((int) gar.X2, (int) gar.Y2)))
                        continue;

                    float t1 = (v1.x - v2.x) * (v1.x - v2.x);
                    float t2 = (v1.y - v2.y) * (v1.y - v2.y);

                    if (MathF.Sqrt(t1 + t2) < 2* m_delta)
                        Graph.AddEdge(new Graph.Edge(i.ToString(), j.ToString()));
                }
            }

            GraphRenderUpdate();
        }
        protected void UserControl_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            m_quadreSizes = (int) Math.Min(RenderGraphImage.Width, RenderGraphImage.Height);
            RenderGraphImage.Width = m_quadreSizes;
            RenderGraphImage.Height = m_quadreSizes;

            GraphRenderUpdate();
        }

        public void ToggleDeltaShow()
        {
            m_renderDeltaRadius = !m_renderDeltaRadius;
            GraphRenderUpdate();
        }

        /* Функции проверки и генерации графа */

        protected abstract Point GetRandomPoint();
        protected abstract (Point, Point) GetRandomGarbage();
        protected bool isPointCurrect(Point _point)
        {
            foreach (Graph.Vertex vertex in Graph.Vertexes)
            {
                float t1 = (vertex.x - _point.X) * (vertex.x - _point.X);
                float t2 = (vertex.y - _point.Y) * (vertex.y - _point.Y);
                if (MathF.Sqrt(t1 + t2) < m_maximalCircleSize)
                    return false;
            }
            return true;
        }
    }
}

