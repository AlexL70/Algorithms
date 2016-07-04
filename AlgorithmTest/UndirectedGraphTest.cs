using NUnit.Framework;
using Algorithms.Graphs;
using System;

namespace AlgorithmTest
{
    [TestFixture, Category(nameof(Algorithms.Graphs))]
    public class UndirectedGraphTest
    {
        [Test]
        public void UG_AddRemoveVertices()
        {
            var g = new UndirectedGraph<int>();
            g.AddVertex(1);
            g.AddVertex(5);
            g.AddVertex(3);
            g.AddVertex(7);
            g.AddVertex(3);
            g.RemoveVertex(5);
            Assert.AreEqual(3, g.VerticesCount);
            Assert.IsTrue(g.VertexExists(7));
            Assert.IsFalse(g.VertexExists(5));
            Assert.IsFalse(g.VertexExists(9));
        }

        [Test]
        public void UG_AddEdges()
        {
            var g = new UndirectedGraph<int>();
            g.AddEdge(1, 2);
            g.AddEdge(2, 3);
            g.AddEdge(2, 4);
            g.AddEdge(1, 3);
            g.AddEdge(1, 4);
            Assert.AreEqual(5, g.EdgesCount);
            Assert.AreEqual(4, g.VerticesCount);
        }

        private UndirectedGraph<int> DoubleConvert()
        {
            var gr = new UndirectedGraph<int>();
            gr.AddEdge(1, 2);
            gr.AddEdge(1, 3);
            gr.AddEdge(1, 4);
            gr.AddEdge(2, 3);
            gr.AddEdge(2, 4);
            gr.AddEdge(2, 5);
            gr.AddEdge(3, 4);
            gr.AddEdge(4, 7);
            gr.AddEdge(5, 6);
            gr.AddEdge(5, 7);
            gr.AddEdge(5, 8);
            gr.AddEdge(6, 7);
            gr.AddEdge(6, 8);
            gr.AddEdge(7, 8);
            return gr;
        }

        [Test]
        public void UG_Contraction()
        {
            var gr = DoubleConvert();
            Assert.AreEqual(8, gr.VerticesCount);
            Assert.AreEqual(14, gr.EdgesCount);
            var g = gr.Clone();
            Assert.AreEqual(8, g.VerticesCount);
            Assert.AreEqual(14, g.EdgesCount);
            g.Contraction(1, 3);
            g.Contraction(2, 4);
            Assert.AreEqual(6, g.VerticesCount);
            Assert.AreEqual(9, g.EdgesCount);
            Assert.AreEqual(4, g.Edges[0].Weight);
        }

        private void ContractEdge(UndirectedGraph<int> g, int index)
        {
            var edge = g.Edges[index];
            g.Contraction(edge.Min.Key, edge.Max.Key);
        }

        [Test]
        public void UG_MinContraction()
        {
            var gr = DoubleConvert();
            var g = gr.Clone();
            g.Contraction(1, 2);
            g.Contraction(1, 3);
            g.Contraction(1, 4);
            g.Contraction(5, 6);
            g.Contraction(5, 7);
            g.Contraction(5, 8);
            Assert.AreEqual(2, g.Edges[0].Weight);
            g = gr.Clone();
            ContractEdge(g, 0);
            ContractEdge(g, 0);
            ContractEdge(g, 0);
            ContractEdge(g, 2);
            ContractEdge(g, 2);
            ContractEdge(g, 1);
            Assert.AreEqual(2, g.Edges[0].Weight);
            Assert.AreEqual(1, g.EdgesCount);
            g = gr.Clone();
            ContractEdge(g, 2);
            ContractEdge(g, 1);
            ContractEdge(g, 0);
            ContractEdge(g, 3);
            ContractEdge(g, 2);
            ContractEdge(g, 1);
            Assert.AreEqual(2, g.Edges[0].Weight);
            Assert.AreEqual(1, g.EdgesCount);
        }

        [Test]
        public void UG_RandomContraction()
        {
            var gr = DoubleConvert();
            int runCount = (int)Math.Pow(gr.VerticesCount, 2) *
                (int)Math.Ceiling(Math.Log(gr.VerticesCount));
            uint minWeight = (uint)gr.VerticesCount;
            for (int i = 0; i < runCount; i++)
            {
                Random rnd = new Random((int)DateTime.Now.Ticks & (0x0000FFFF + i));
                var g = gr.Clone();
                while (g.EdgesCount > 1)
                {
                    int rndInt = rnd.Next(0, g.EdgesCount);
                    ContractEdge(g, rndInt);
                }
                if (g.Edges[0].Weight < minWeight)
                    minWeight = g.Edges[0].Weight;
            }
            Assert.AreEqual(2, minWeight);
        }
    }
}
