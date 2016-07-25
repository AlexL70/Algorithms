using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Graphs
{
    public class DirectedGraph<TKey> : Graph<TKey>
        where TKey : IComparable<TKey>
    {
        public DirectedGraph() : base() { }

        public DirectedGraph(Tuple<TKey, TKey>[] tArr) : base(tArr)
        {
            foreach (var t in tArr.Where(e => e.Item1.CompareTo(e.Item2) != 0)
                .GroupBy(e => new { e.Item1, e.Item2 })
                .Select(e => new {
                first = e.First().Item1,
                second = e.First().Item2,
                weight = e.Count()
            }))
            {
                _edges.Add(NewEdge(GetVertex(t.first), GetVertex(t.second), t.weight));
            }
            _edges.Sort();
            _eOrdered = true;
        }

        public virtual DirectedGraph<TKey> Clone()
        {
            var gr = new DirectedGraph<TKey>();
            CopyData(gr);
            return gr;
        }

        public override Edge AddEdge(TKey first, TKey second, int weight = 1)
        {
            var v0 = AddVertex(first);
            var v1 = AddVertex(second);
            int index = GetEdgeIndex(first, second);
            if (index >= 0)
            {
                _edges[index]._weight += weight;
                return _edges[index];
            }
            else
            {
                var edge = NewEdge(v0, v1, weight);
                _edges.Add(edge);
                _eOrdered = false;
                return edge;
            }
        }

        public override void RemoveEdge(TKey first, TKey second)
        {
            var loopInd = GetEdgeIndex(first, second);
            if (loopInd < 0)
                throw new Exception($"Edge {{{first}, {second}}} not found.");
            _edges.RemoveAt(loopInd);
        }

        protected void ReverseEdges()
        {
            foreach (var edge in _edges)
            {
                var tmp = edge._source;
                edge._source = edge._dest;
                edge._dest = tmp;
            }
            if (_eOrdered)
            {
                _edges.Sort();
            }
        }

        public int FindStronglyConnectedComponents()
        {
            int componentsCount = 0;
            //  1. Reverse edges before running Depth First Search first time
            ReverseEdges();
            EnforceOrder = true;
            //  2. Run depth first search to create vertices list
            //  in reversed finishing time order
            var ftStack = new DataStructures.Stack<Vertex>();
            for (int i = Vertices.Count - 1; i >= 0; i--)
            {
                if (!Vertices[i].Viewed)
                {
                    DepthFirstSearch(Vertices[i].Key, null, (Vertex v) => ftStack.Push(v));
                }
            }
            //  Restore initial edges and viewed state before DFS second run
            ReverseEdges();
            foreach (var vertex in Vertices)
            {
                vertex.Viewed = false;
            }
            //  3. Finally run DFS in reverse finishing time order;
            //  count strongly connected components and mark vertices
            //  belonging to each component with component's number (secondary number)
            while (!ftStack.IsEmpty) 
            {
                var vertex = ftStack.Pop();
                if (!vertex.Viewed)
                {
                    componentsCount++;
                    DepthFirstSearch(vertex.Key, 
                        (Vertex v) => v.SecondaryOrder = componentsCount, null);
                }
            }
            return componentsCount;
        }
    }
}
