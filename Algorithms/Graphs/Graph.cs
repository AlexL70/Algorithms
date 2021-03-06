﻿using System;
using System.Collections.Generic;
using Algorithms.SelectionAndSearch;
using System.Linq;

namespace Algorithms.Graphs
{
    public abstract class Graph<TKey> where TKey : IComparable<TKey>
    {
        public class Vertex : IComparable<TKey>, IComparable<Vertex>
        {
            private TKey _key;
            private Graph<TKey> _graph;
            public bool Viewed { get; set; } = false;
            public int SecondaryOrder { get; set; } = 0;
            protected internal Vertex(TKey key, Graph<TKey> graph)
            {
                _key = key;
                _graph = graph;
            }

            public TKey Key { get { return _key; } }
            public Graph<TKey> Graph { get { return _graph; } }

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
            protected internal Vertex _source;
            protected internal Vertex _dest;
            protected internal int _weight;
            protected internal Edge(Vertex first, Vertex second, int weight = 1)
            {
                if (first.CompareTo(second) == 0)
                {
                    throw new Exception($"Loops edges are not allowed: {first.Key}.");
                }
                else
                {
                    _source = first;
                    _dest = second;
                }
                _weight = weight;
            }

            public int CompareTo(Edge other)
            {
                if (this._source.CompareTo(other._source) < 0)
                    return -1;
                else if (this._source.CompareTo(other._source) > 0)
                    return 1;
                else if (this._dest.CompareTo(other._dest) < 0)
                    return -1;
                else if (this._dest.CompareTo(other._dest) > 0)
                    return 1;
                else
                    return 0;
            }

            public int CompareTo(Tuple<TKey, TKey> other)
            {
                if (this._source.CompareTo(other.Item1) < 0)
                    return -1;
                else if (this._source.CompareTo(other.Item1) > 0)
                    return 1;
                else if (this._dest.CompareTo(other.Item2) < 0)
                    return -1;
                else if (this._dest.CompareTo(other.Item2) > 0)
                    return 1;
                else
                    return 0;
            }

            public Vertex Source { get { return _source; } }
            public Vertex Dest { get { return _dest; } }
            public int Weight { get { return _weight; } }
        }

        public Graph()
        {
            _vertices = new List<Vertex>();
            _edges = new List<Edge>();
        }

        public Graph(Tuple<TKey, TKey>[] tArr) : this()
        {
            foreach(var key in tArr.Select(e => e.Item1)
                .Union(tArr.Select(e => e.Item2))
                .GroupBy(i => i).Select(g => g.First()).AsEnumerable<TKey>())
            {
                _vertices.Add(new Vertex(key, this));
            }
            _vertices.Sort();
            _vOrdered = true;
        }

        protected List<Vertex> _vertices;
        protected List<Edge> _edges;
        protected virtual void CopyData(Graph<TKey> other)
        {
            foreach (var vertix in _vertices)
                other._vertices.Add(vertix);
            foreach (var edge in _edges)
                other._edges.Add(edge);
            other._eOrdered = _eOrdered;
            other._vOrdered = _vOrdered;
        }

        public virtual IReadOnlyList<Vertex> Vertices { get { return _vertices; } }
        public virtual IReadOnlyList<Edge> Edges { get { return _edges; } }
        public virtual int VerticesCount { get { return _vertices.Count; } }
        public virtual int EdgesCount { get { return _edges.Count; } }

        protected bool _vOrdered;
        protected bool _eOrdered;

        public virtual bool EnforceOrder
        {
            get { return _eOrdered && _vOrdered; }
            set
            {
                if (value && !_vOrdered)
                    _vertices.Sort();
                _vOrdered = value;
                if (value && !_eOrdered)
                    _edges.Sort();
                _eOrdered = value;
            }
        }

        public virtual Vertex AddVertex(TKey key)
        {
            var v = GetVertex(key);
            if (v == null)
            {
                v = NewVertex(key);
                _vertices.Add(v);
                _vOrdered = false;
            }
            return v;
        }

        protected virtual Vertex NewVertex(TKey key)
        {
            return new Vertex(key, this);
        }

        public virtual bool VertexExists(TKey key)
        {
            return GetVertexIndex(key) >= 0;
        }

        public virtual void RemoveVertex(TKey key)
        {
            int ind = GetVertexIndex(key);
            if (ind >= 0)
            {
                _vertices.RemoveAt(ind);
                //  !!!TODO!!! remove all edges;
            }
        }

        protected virtual int GetVertexIndex(TKey key)
        {
            if (_vOrdered)
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

        public virtual Vertex GetVertex(TKey key, bool throwIfNotFound = false)
        {
            int ind = GetVertexIndex(key);
            if (ind >= 0)
                return _vertices[ind];
            else if (throwIfNotFound)
                throw new Exception($"Vertex {key} not found.");
            else
                return null;
        }

        protected virtual int GetEdgeIndex(TKey first, TKey second)
        {
            int index = -1;
            var t = Tuple.Create(first, second);
            if (_eOrdered)
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

        public virtual Edge GetEdge(TKey first, TKey second)
        {
            var index = GetEdgeIndex(first, second);
            if (index >= 0)
            {
                return Edges[index];
            }
            else
            {
                return null;
            }
        }

        public abstract Edge AddEdge(TKey first, TKey second, int weight = 1);

        protected virtual Edge NewEdge(Vertex first, Vertex second, int weight = 1)
        {
            return new Edge(first, second, weight);
        }

        public abstract void RemoveEdge(TKey first, TKey second);

        public virtual IEnumerable<Edge> Outgoing(TKey key)
        {
            if (_eOrdered)
            {
                var min = Tuple.Create(key, Vertices[0].Key);
                var max = Tuple.Create(key, Vertices[Vertices.Count - 1].Key);
                var minInd = Edges.BinarySearch(min, GBinarySearch.Option.EqOrGreater);
                var maxInd = Edges.BinarySearch(max, GBinarySearch.Option.EqOrLess);
                if (minInd >= 0 && maxInd >= 0)
                    for (int i = minInd; i <= maxInd; i++)
                    {
                        yield return Edges[i];
                    }
            }
            else
            {
                foreach (var e in Edges)
                {
                    if (e.Source.Key.CompareTo(key) == 0)
                    {
                        yield return e;
                    }
                }
            }
        }
        public virtual IEnumerable<Vertex> Nearest(TKey key)
        {
            foreach (var e in Outgoing(key))
            {
                yield return e.Dest;
            }
        }

        public delegate void ProcessVertex(Vertex v);

        public void DepthFirstSearch(TKey key, ProcessVertex preProcess = null, ProcessVertex postProcess = null)
        {
            var v = GetVertex(key);
            DataStructures.Stack<Vertex> vStack = new DataStructures.Stack<Vertex>();
            v.Viewed = true;
            vStack.Push(v);
            preProcess?.Invoke(v);
            while (!vStack.IsEmpty)
            {
                bool neighboursFound = false;
                foreach (var vert in Nearest(vStack.Peek().Key).Where(vrt => !vrt.Viewed))
                {
                    neighboursFound = true;
                    preProcess?.Invoke(vert);
                    vert.Viewed = true;
                    vStack.Push(vert);
                    break;
                }
                if (!neighboursFound)
                {
                    var vert = vStack.Pop();
                    postProcess?.Invoke(vert);
                    vert.Viewed = true;
                }
            }
        }

        public void BreadthFirstSearch(TKey key, ProcessVertex preProcess = null, ProcessVertex postProcess = null)
        {
            var v = GetVertex(key);
            DataStructures.Queue<Vertex> vQueue = new DataStructures.Queue<Vertex>();
            v.Viewed = true;
            vQueue.Enqueue(v);
            preProcess?.Invoke(v);
            while (!vQueue.IsEmpty)
            {
                foreach (var vert in Nearest(vQueue.Peek().Key).Where(vrt => !vrt.Viewed))
                {
                    preProcess?.Invoke(vert);
                    vert.Viewed = true;
                    vQueue.Enqueue(vert);
                }
                v = vQueue.Dequeue();
                postProcess?.Invoke(v);
            }
        }
    }
}
