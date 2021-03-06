﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Graphs
{
    public class UndirectedGraph<TKey> : Graph<TKey>
        where TKey : IComparable<TKey>
    {
        public UndirectedGraph() : base() { }

        public UndirectedGraph(Tuple<TKey, TKey>[] tArr) : base(tArr)
        {
            foreach (var t in tArr.Where(e => e.Item1.CompareTo(e.Item2) != 0)
                .Select(e => new { Item1 = e.Item1, Item2 = e.Item2 })
                .Union(tArr.Select(e => new { Item1 = e.Item2, Item2 = e.Item1}))
                .GroupBy(e => new { e.Item1, e.Item2 }).Select(e => new {
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

        public UndirectedGraph<TKey> Clone()
        {
            var g = new UndirectedGraph<TKey>();
            CopyData(g);
            return g;
        }

        public override int EdgesCount { get { return _edges.Count / 2; } }

        public override Edge AddEdge(TKey first, TKey second, int weight = 1)
        {
            var v0 = AddVertex(first);
            var v1 = AddVertex(second);
            int index = GetEdgeIndex(first, second);
            if (index >= 0)
            {
                int secondIndex = GetEdgeIndex(second, first);
                _edges[index]._weight += weight;
                _edges[secondIndex]._weight += weight;
                return _edges[index];
            }
            else
            {
                var edge = NewEdge(v0, v1, weight);
                var edge1 = NewEdge(v1, v0, weight);
                _edges.Add(edge);
                _edges.Add(edge1);
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
            var loopInd0 = GetEdgeIndex(second, first);
            _edges.RemoveAt(loopInd0);
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
            RemoveEdge(first, second);
            var newEdges = new List<Edge>();
            for (int i = 0; i < _edges.Count;)
            {
                if (_edges[i].Source.CompareTo(max) == 0)
                {
                    var replInd = GetEdgeIndex(min.Key, _edges[i].Dest.Key);
                    var replInd1 = GetEdgeIndex(Edges[i].Dest.Key, min.Key);
                    if (replInd >= 0)
                    {
                        _edges[replInd]._weight += _edges[i]._weight;
                        _edges[replInd1]._weight += _edges[i]._weight;
                    }
                    else
                        newEdges.Add(new Edge(min, _edges[i].Dest, _edges[i]._weight));
                    RemoveEdge(Edges[i].Source.Key, Edges[i].Dest.Key);
                    if (i > 0)
                        i--;
                    else if (_edges.Count == 0)
                        break;
                }
                else
                {
                    i++;
                }
            }
            foreach (var edge in newEdges)
            {
                AddEdge(edge.Source.Key, edge.Dest.Key, edge.Weight);
            }
            _edges.Sort();
            _vertices.Remove(max);
        }
    }
}
