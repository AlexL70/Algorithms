using NUnit.Framework;
using Algorithms.Graphs;

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

        [Test]
        public void UG_Contraction()
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
            Assert.AreEqual(8, gr.VerticesCount);
            Assert.AreEqual(14, gr.EdgesCount);
            var g = gr.Clone();
            Assert.AreEqual(8, g.VerticesCount);
            Assert.AreEqual(14, g.EdgesCount);
        }
    }
}
