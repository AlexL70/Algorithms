using System;
using Algorithms.Sorting;
using System.Collections.Generic;

namespace Algorithms.Graphs
{
    public class UndirectedGraph<TKey> where TKey: IComparable<TKey>
    {
        public class Vertex
        {
            private TKey _key;
            private UndirectedGraph<TKey> _graph;

            private Vertex(TKey key, UndirectedGraph<TKey> graph)
            {
                _key = key;
                _graph = graph;
            }
        }

        public class Edge
        {
            private Vertex _minVert;
            private Vertex _maxVert;
            private uint _weight;
            public Edge(Vertex min, Vertex max, uint weight = 1)
            {
                _maxVert = min;
                _maxVert = max;
                _weight = weight;
            }
        }

        public bool EnforceOrder { get; set; } = false;
        private List<Vertex> _vertices;
        private List<Edge> _edged;


    }
}
