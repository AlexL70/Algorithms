using System;
using Algorithms.DataStructures;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Algorithms.Graphs
{
    public class DijkstrasGraph<TKey> : DirectedGraph<TKey>
        where TKey : IComparable<TKey>
    {
        public class DEdge : Edge, IComparable<DEdge>
        {
            protected internal int _lenght;
            protected internal int _heapIndex;
            public int Length { get { return _lenght; } }
            protected internal DEdge(Vertex first, Vertex second, int length) : base(first, second)
            {
                _lenght = length;
                _heapIndex = 0;
            }

            public int PathLen { get { return ((DVertex)_source).PathLen + Length; } }

            public int CompareTo(DEdge other)
            {
                return PathLen.CompareTo(other.PathLen);
            }
        }

        public class DVertex : Vertex
        {
            protected internal int _pathLen;
            public int PathLen { get { return _pathLen; } }

            protected internal DVertex(TKey key, Graph<TKey> graph) : base(key, graph)
            {
                _pathLen = int.MaxValue / 2;
            }
        }

        public DijkstrasGraph() : base() { }
        public DijkstrasGraph(Tuple<TKey, TKey, int>[] tArr) : base()
        {
            foreach (var key in tArr.Select(e => e.Item1)
                .Union(tArr.Select(e => e.Item2))
                .GroupBy(i => i).Select(g => g.First()).AsEnumerable<TKey>())
            {
                _vertices.Add(NewVertex(key));
            }
            _vertices.Sort();
            _vOrdered = true;
            foreach (var t in tArr)
            {
                _edges.Add(NewEdge(GetVertex(t.Item1), GetVertex(t.Item2), t.Item3));
            }
            _edges.Sort();
            _eOrdered = true;
        }
        public new DijkstrasGraph<TKey> Clone()
        {
            return (DijkstrasGraph<TKey>)base.Clone();
        }

        private class _vList : IReadOnlyList<DVertex>
        {
            private List<Vertex> _vertices;
            public _vList(List<Vertex> vertices)
            {
                _vertices = vertices;
            }

            public DVertex this[int index]
            {
                get
                {
                    return (DVertex)_vertices[index];
                }
            }

            public int Count
            {
                get
                {
                    return _vertices.Count;
                }
            }

            public System.Collections.Generic.IEnumerator<DVertex> GetEnumerator()
            {
                foreach (var v in _vertices)
                {
                    yield return (DVertex)v;
                };
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class _eList : IReadOnlyList<DEdge>
        {
            private List<Edge> _edges;
            public _eList(List<Edge> edges)
            {
                _edges = edges;
            }

            public DEdge this[int index]
            {
                get
                {
                    return (DEdge)_edges[index];
                }
            }

            public int Count
            {
                get
                {
                    return _edges.Count;
                }
            }

            public System.Collections.Generic.IEnumerator<DEdge> GetEnumerator()
            {
                foreach (var e in _edges)
                {
                    yield return (DEdge)e;
                };
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public new IReadOnlyList<DVertex> Vertices { get { return new _vList(_vertices); } }
        public new IReadOnlyList<DEdge> Edges { get { return new _eList(_edges); } }

        protected override Edge NewEdge(Vertex first, Vertex second, int weight = 1)
        {
            return new DEdge(first, second, weight);
        }
        protected override Vertex NewVertex(TKey key)
        {
            return new DVertex(key, this);
        }


        private BinaryHeap<DEdge> heap;
        private new void ProcessVertex(DVertex v, int pathLen)
        {
            v.Viewed = true;
            v._pathLen = pathLen;
            foreach (var e in Outgoing(v.Key))
            {
                var dedge = (DEdge)e;
                heap.RemoveAt(dedge._heapIndex);
                heap.Insert(dedge);
            }
        }
        public void CalcPathLen(TKey start)
        {
            CalcPathLen((DVertex)GetVertex(start));
        }
        public void CalcPathLen(DVertex start)
        {
            //  Reinitialize vertices
            foreach (var v in Vertices)
            {
                v._pathLen = int.MaxValue / 2;
                v.Viewed = false;
            }
            heap = new BinaryHeap<DEdge>();
            heap.postProcess = (DEdge e, int oldIndex, int newIndex) =>
                e._heapIndex = newIndex;
            foreach (var e in Edges)
            {
                heap.Insert(e);
            }
            var vCount = 1;
            ProcessVertex(start, 0);
            while (vCount < VerticesCount && !heap.IsEmpty)
            {
                var e = heap.ExtractMin();
                if (!e.Dest.Viewed)
                {
                    ProcessVertex((DVertex)e.Dest, e.PathLen);
                    vCount++;
                }
            }
        }
    }
}
