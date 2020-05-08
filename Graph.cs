using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;

namespace Graphs
{
    public class Graph
    {
        private readonly List<Edge> edges;
        private readonly List<Vertex> vertexes;

        public Graph()
        {
            edges = new List<Edge>();
            vertexes = new List<Vertex>();
        }

        public List<Vertex> Vertexes { get => vertexes; }
        public List<Edge> Edges { get => edges; }

        public void AddEdge(Edge _edge)
        {
            if (!edges.Contains(_edge))
                edges.Add(_edge);
        }
        public void AddVertex(Vertex _vertex)
        {
            if (!vertexes.Contains(_vertex))
                vertexes.Add(_vertex);
        }
        public Vertex GetVertex(string _vertex)
        {
            return vertexes.Find(v => v.name == _vertex);
        }

        public struct Vertex
        {
            public Vertex(int _x, int _y, string _name)
            {
                x = _x;
                y = _y;
                name = _name;
            }

            public Vertex(Point _point, string _name)
            {
                x = _point.X;
                y = _point.Y;
                name = _name;
            }

            public int x;
            public int y;
            public string name;
        }

        public struct Edge
        {
            public Edge(string _first, string _second)
            {
                first = _first;
                second = _second;
            }

            public string first;
            public string second;
        }
    }
}

