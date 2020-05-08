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

        public GraphControl(Graph _bindGraph, float _delta)
        {
            InitializeComponent();

            Graph = _bindGraph;
            m_random = new Random();
            m_quadreSizes = (int) Math.Min(RenderGraphImage.Width, RenderGraphImage.Height);
            m_delta = _delta;
            m_renderDeltaRadius = false;

            GraphRenderUpdate();
        }
        public void GraphRenderUpdate()
        {
            RenderGraphImage.Children.Clear();

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
        public void GenerateGraph()
        {
            string db = "";
            Graph = new Graph();
            for (int i = 0; i < 4; ++i)
                Graph.AddVertex(new Graph.Vertex(GetRandomPoint(), i.ToString()));

            for (int i = 0; i < 4; ++i)
            {
                Graph.Vertex v1 = Graph.GetVertex(i.ToString());
                db += $"{v1.x}, {v1.y}";

                for (int j = i + 1; j < 4; ++j)
                {
                    Graph.Vertex v2 = Graph.GetVertex(j.ToString());
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

        /* Функции проверки и генерации графа */

        protected abstract Point GetRandomPoint();
        protected abstract (Point, Point) GetRandomGarbage();
        public void ToggleDeltaShow()
        {
            m_renderDeltaRadius = !m_renderDeltaRadius;
            GraphRenderUpdate();
        }
    }
}

