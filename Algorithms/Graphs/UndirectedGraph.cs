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

        public class Edge : IComparable<Edge>, IComparable<Tuple<TKey, TKey>>
        {
            private Vertex _minVert;
            private Vertex _maxVert;
            protected internal uint _weight;

            public Edge(Vertex first, Vertex second, uint weight = 1)
            {
                if (first.CompareTo(second) == 0)
                {
                    throw new Exception($"Loops edges are not allowed: {first.Key}.");
                }
                else if (first.CompareTo(second) < 0)
                {
                    _minVert = first;
                    _maxVert = second;
                }
                else
                {
                    _minVert = second;
                    _maxVert = first;
                }
                _weight = weight;
            }

            public int CompareTo(Edge other)
            {
                if (this._minVert.CompareTo(other._minVert) < 0)
                    return -1;
                else if (this._minVert.CompareTo(other._minVert) > 0)
                    return 1;
                else if (this._maxVert.CompareTo(other._maxVert) < 0)
                    return -1;
                else if (this._maxVert.CompareTo(other._maxVert) > 0)
                    return 1;
                else
                    return 0;
            }

            public int CompareTo(Tuple<TKey, TKey> other)
            {
                if (this._minVert.CompareTo(other.Item1) < 0)
                    return -1;
                else if (this._minVert.CompareTo(other.Item1) > 0)
                    return 1;
                else if (this._maxVert.CompareTo(other.Item2) < 0)
                    return -1;
                else if (this._maxVert.CompareTo(other.Item2) > 0)
                    return 1;
                else
                    return 0;
            }

            public Vertex Min { get { return _minVert; } }
            public Vertex Max { get { return _maxVert; } }
            public uint Weight { get { return _weight; } }
        }

        private bool _enforceOrder;
        public bool EnforceOrder
        {
            get { return _enforceOrder; }
            set
            {
                _enforceOrder = value;
                if (value)
                {
                    _vertices.Sort();
                    _edges.Sort();
                }
            }
        }

        private List<Vertex> _vertices;
        private List<Edge> _edges;
        public IReadOnlyList<Edge> Edges { get { return _edges; } }
        public IReadOnlyList<Vertex> Vertices { get { return _vertices; } }

        public int VerticesCount { get { return _vertices.Count; } }
        public int EdgesCount { get { return _edges.Count; } }

        public UndirectedGraph()
        {
            _vertices = new List<Vertex>();
            _edges = new List<Edge>();
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

        public Edge AddEdge(TKey first, TKey second)
        {
            var v0 = AddVertex(first);
            var v1 = AddVertex(second);
            int index = GetEdgeIndex(first, second);
            if (index >= 0)
            {
                _edges[index]._weight++;
                return _edges[index];
            }
            else
            {
                var edge = new Edge(v0, v1);
                _edges.Add(edge);
                EnforceOrder = false;
                return edge;
            }
        }

        protected int GetEdgeIndex(TKey first, TKey second)
        {
            int index = -1;
            var t = first.CompareTo(second) < 0 ? Tuple.Create(first, second) : Tuple.Create(second, first);
            if (EnforceOrder)
            {
                index = _edges.BinarySearch(t);
            }
            else
            {
                for (int i = 0; i < _edges.Count; i++)
                {
                    if (_edges[i].CompareTo(t) == 0)
                    {
                        index = i;
                    }
                }
            }
            return index;
        }
    }
}
