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

        public UndirectedGraph<TKey> Clone()
        {
            var g = new UndirectedGraph<TKey>();
            foreach (var vertix in this._vertices)
                g.AddVertex(vertix.Key);
            foreach (var edge in this._edges)
                g.AddEdge(edge.Min.Key, edge.Max.Key);
            return g;
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

        public Vertex GetVertex(TKey key, bool throwIfNotFound = false)
        {
            int ind = GetVertexIndex(key);
            if (ind >= 0)
                return _vertices[ind];
            else if (throwIfNotFound)
                throw new Exception($"Vertex {key} not found.");
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

        public void Contraction(TKey first, TKey second)
        {
            if (!EnforceOrder)
                EnforceOrder = true;
            Vertex min, max;
            if (first.CompareTo(second) == 0)
            {
                throw new Exception($"Vertix {first} cannot be contracted with itself.");
            }
            else if (first.CompareTo(second) < 0)
            {
                min = GetVertex(first, true);
                max = GetVertex(second, true);
            }
            else
            {
                max = GetVertex(first, true);
                min = GetVertex(second, true);
            }
            var loopInd = GetEdgeIndex(first, second);
            if (loopInd < 0)
                throw new Exception($"Edge {{{first}, {second}}} not found.");
            _edges.RemoveAt(loopInd);
            var newEdges = new List<Edge>();
            for (int i = 0; i < _edges.Count;)
            {
                if (_edges[i].Min.CompareTo(max) == 0)
                {
                    var replInd = GetEdgeIndex(min.Key, _edges[i].Max.Key);
                    if (replInd >= 0)
                        _edges[replInd]._weight += _edges[i]._weight;
                    else
                        newEdges.Add(new Edge(min, _edges[i].Max, _edges[i]._weight));
                    _edges.RemoveAt(i);
                }
                else if (_edges[i].Max.CompareTo(max) == 0)
                {
                    var replInd = GetEdgeIndex(_edges[i].Min.Key, min.Key);
                    if (replInd >= 0)
                        _edges[replInd]._weight += _edges[i]._weight;
                    else
                        newEdges.Add(new Edge(_edges[i].Min, min, _edges[i]._weight));
                    _edges.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
            foreach (var edge in newEdges)
            {
                _edges.Add(edge);
            }
            _edges.Sort();
            _vertices.Remove(max);
        }
    }
}
