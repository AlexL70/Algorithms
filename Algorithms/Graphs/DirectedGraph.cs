using System;
using System.Collections.Generic;

namespace Algorithms.Graphs
{
    public class DirectedGraph<TKey> : Graph<TKey>
        where TKey : IComparable<TKey>
    {
        public DirectedGraph() : base() { }

        public DirectedGraph<TKey> Clone()
        {
            var gr = new DirectedGraph<TKey>();
            CopyData(gr);
            return gr;
        }

        public override Edge AddEdge(TKey first, TKey second, uint weight = 1)
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
                var edge = new Edge(v0, v1, weight);
                _edges.Add(edge);
                EnforceOrder = false;
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
    }
}
