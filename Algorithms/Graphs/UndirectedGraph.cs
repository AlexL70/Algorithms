using System;
using Algorithms.Sorting;
using System.Collections.Generic;
using Algorithms.SelectionAndSearch;

namespace Algorithms.Graphs
{
    public class UndirectedGraph<TKey> where TKey : IComparable<TKey>
    {
        public class Vertex : IComparable<TKey>, IComparable<Vertex>
        {
            private TKey _key;
            private UndirectedGraph<TKey> _graph;

            protected internal Vertex(TKey key, UndirectedGraph<TKey> graph)
            {
                _key = key;
                _graph = graph;
            }

            public TKey Key { get { return _key; } }

            public int CompareTo(TKey other)
            {
                return this.Key.CompareTo(other);
            }

            public int CompareTo(Vertex other)
            {
                return this.Key.CompareTo(other.Key);
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

        public int VerticesCount { get { return _vertices.Count; } }
        public int EdgesCount { get { return _edged.Count; } }

        public UndirectedGraph()
        {
            _vertices = new List<Vertex>();
            _edged = new List<Edge>();
        }

        public Vertex AddVertex(TKey key)
        {
            var v = GetVertex(key);
            if (v == null)
            {
                v = new Vertex(key, this);
                _vertices.Add(v);
                EnforceOrder = false;
            }
            return v;
        }

        public bool VertexExists(TKey key)
        {
            return GetVertexIndex(key) >= 0;
        }

        public void RemoveVertex(TKey key)
        {
            int ind = GetVertexIndex(key);
            if (ind >= 0)
            {
                _vertices.RemoveAt(ind);
                //  !!!TODO!!! remove all edges;
            }
        }

        protected int GetVertexIndex(TKey key)
        {
            if (EnforceOrder)
            {
                return _vertices.BinarySearch(key);
            }
            else
            {
                for (int i = 0; i < _vertices.Count; i++)
                {
                    if (_vertices[i].Key.CompareTo(key) == 0)
                    {
                        return i;
                    }
                }
                return -1;
            }
        }

        public Vertex GetVertex(TKey key)
        {
            int ind = GetVertexIndex(key);
            if (ind >= 0)
                return _vertices[ind];
            else
                return null;
        }
    }
}
