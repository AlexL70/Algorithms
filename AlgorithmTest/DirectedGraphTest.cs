﻿using NUnit.Framework;
using Algorithms.Graphs;
using System;
using System.Linq;
using System.Collections;

namespace AlgorithmTest
{
    [TestFixture, Category(nameof(Algorithms.Graphs))]
    class DirectedGraphTest
    {
        [Test]
        public void DG_AddRemoveVertices()
        {
            var g = new DirectedGraph<int>();
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
        public void DG_AddEdges()
        {
            var g = new DirectedGraph<int>();
            g.AddEdge(1, 2);
            g.AddEdge(2, 3);
            g.AddEdge(2, 4);
            g.AddEdge(1, 3);
            g.AddEdge(1, 4);
            Assert.AreEqual(5, g.EdgesCount);
            Assert.AreEqual(4, g.VerticesCount);
        }

        private DirectedGraph<int> ThreeCycles(bool enforceOrder = true)
        {
            var gr = new DirectedGraph<int>();
            gr.AddEdge(1, 2);
            gr.AddEdge(2, 3);
            gr.AddEdge(3, 1);
            gr.AddEdge(3, 4);
            gr.AddEdge(4, 5);
            gr.AddEdge(5, 6);
            gr.AddEdge(6, 4);
            gr.AddEdge(6, 7);
            gr.AddEdge(7, 8);
            gr.AddEdge(8, 9);
            gr.AddEdge(9, 7);
            gr.EnforceOrder = enforceOrder;
            return gr;
        }

        private DirectedGraph<int> TwoRhombuses(bool enforceOrder = true)
        {
            var gr = new DirectedGraph<int>();
            gr.AddEdge(1, 2);
            gr.AddEdge(1, 3);
            gr.AddEdge(2, 4);
            gr.AddEdge(3, 4);
            gr.AddEdge(4, 5);
            gr.AddEdge(4, 6);
            gr.AddEdge(5, 7);
            gr.AddEdge(6, 7);
            gr.EnforceOrder = enforceOrder;
            return gr;
        }

        private DirectedGraph<int> FourSCCs(bool enforceOrder = true)
        {
            Tuple<int, int>[] tArr = new Tuple<int, int>[]
            {
                Tuple.Create(1, 2),
                Tuple.Create(2, 3),
                Tuple.Create(2, 11),
                Tuple.Create(3, 1),
                Tuple.Create(3, 4),
                Tuple.Create(3, 5),
                Tuple.Create(4, 5),
                Tuple.Create(5, 6),
                Tuple.Create(5, 7),
                Tuple.Create(5, 8),
                Tuple.Create(6, 7),
                Tuple.Create(6, 10),
                Tuple.Create(7, 4),
                Tuple.Create(8, 9),
                Tuple.Create(9, 10),
                Tuple.Create(10, 8),
                Tuple.Create(11, 8),
                Tuple.Create(11, 9)
            };
            var gr = new DirectedGraph<int>(tArr);
            return gr;
        }

        [Test]
        public void DG_Nearest()
        {
            DirectedGraph<int> gr = ThreeCycles();
            Assert.AreEqual(new Graph<int>.Vertex[]
            {
                gr.GetVertex(1),
                gr.GetVertex(4)
            }, gr.Nearest(3).ToArray());
            Assert.AreEqual(new Graph<int>.Vertex[]
            {
                gr.GetVertex(8)
            }, gr.Nearest(7).ToArray());
        }

        [Test]
        public void DG_BFG()
        {
            Graph<int> gr = TwoRhombuses();
            int[] secOrd = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            int order = 0;
            Graph<int>.ProcessVertex enumProc = (Graph<int>.Vertex v) => v.SecondaryOrder = ++order;
            gr.BreadthFirstSearch(1, null, enumProc);
            Assert.AreEqual(secOrd,
                gr.Vertices.OrderBy(v => v.Key).Select(v => v.SecondaryOrder).ToArray()
                );
        }

        [Test]
        public void DG_DFS()
        {
            Graph<int> gr = TwoRhombuses();
            int[] secOrd = new int[] { 1, 2, 4, 5, 7, 6, 3 };
            int order = 0;
            Graph<int>.ProcessVertex enumProc = (Graph<int>.Vertex v) => v.SecondaryOrder = ++order;
            gr.DepthFirstSearch(1, enumProc, null);
            Assert.AreEqual(secOrd,
                gr.Vertices.OrderBy(v => v.SecondaryOrder).Select(v => v.Key).ToArray());
        }

        [Test]
        public void DG_SCC()
        {
            var gr = FourSCCs(false);
            int sccCount = gr.FindStronglyConnectedComponents();
            Assert.AreEqual(4, sccCount);
            Assert.AreEqual(new int[] { 1, 2, 3, 4 },
                gr.Vertices.GroupBy(v => v.SecondaryOrder)
                .Select(g => g.First())
                .OrderBy(v => v.SecondaryOrder)
                .Select(v => v.SecondaryOrder).ToArray());
        }
    }

    public class DijkstraTestData
    {
        #region Dijkstra hardcoded test data
        #region t1
        private static Tuple<int, int, int>[] t1 = new Tuple<int, int, int>[]
        {
            Tuple.Create( 1, 2, 1 ),
            Tuple.Create( 1, 3, 4 ),
            Tuple.Create( 2, 3, 2 ),
            Tuple.Create( 2, 4, 8 ),
            Tuple.Create( 3, 4, 3 )
        };

        private static Tuple<int, int>[] t1_paths = new Tuple<int, int>[]
        {
            Tuple.Create(1, 0),
            Tuple.Create(2, 1),
            Tuple.Create(3, 3),
            Tuple.Create(4, 6)
        };
        #endregion
        #region t2
        private static Tuple<int, int, int>[] t2 = new Tuple<int, int, int>[]
        {
            Tuple.Create(1, 2, 2),
            Tuple.Create(1, 3, 1),
            Tuple.Create(2, 3, 1),
            Tuple.Create(2, 4, 10),
            Tuple.Create(2, 6, 1),
            Tuple.Create(3, 4, 2),
            Tuple.Create(3, 5, 3),
            Tuple.Create(4, 5, 1),
            Tuple.Create(4, 6, 10),
            Tuple.Create(5, 7, 2),
            Tuple.Create(6, 5, 3),
            Tuple.Create(6, 7, 12)
        };

        private static Tuple<int, int>[] t2_paths = new Tuple<int, int>[]
        {
            Tuple.Create(1, 0),
            Tuple.Create(2, 2),
            Tuple.Create(3, 1),
            Tuple.Create(4, 3),
            Tuple.Create(5, 4),
            Tuple.Create(6, 3),
            Tuple.Create(7, 6),
        };
        #endregion
        #region t3
        private static Tuple<int, int, int>[] t3 = new Tuple<int, int, int>[]
        {
            Tuple.Create( 1, 2, 1),
            Tuple.Create( 1, 5, 3),
            Tuple.Create( 1, 8, 3),
            Tuple.Create( 2, 1, 1),
            Tuple.Create( 2, 3, 4),
            Tuple.Create( 2, 7, 2),
            Tuple.Create( 2, 8, 1),
            Tuple.Create( 3, 2, 4),
            Tuple.Create( 3, 4, 1),
            Tuple.Create( 3, 5, 2),
            Tuple.Create( 3, 7, 1),
            Tuple.Create( 3, 8, 3),
            Tuple.Create( 4, 3, 1),
            Tuple.Create( 5, 1, 3),
            Tuple.Create( 5, 3, 2),
            Tuple.Create( 5, 6, 1),
            Tuple.Create( 5, 7, 1),
            Tuple.Create( 6, 5, 1),
            Tuple.Create( 7, 2, 2),
            Tuple.Create( 7, 3, 1),
            Tuple.Create( 7, 5, 1),
            Tuple.Create( 7, 8, 2),
            Tuple.Create( 8, 1, 3),
            Tuple.Create( 8, 2, 1),
            Tuple.Create( 8, 3, 3),
            Tuple.Create( 8, 7, 2),
            Tuple.Create( 8, 9, 1),
            Tuple.Create( 9, 8, 1),
            Tuple.Create( 9, 10, 3),
            Tuple.Create( 9, 11, 2),
            Tuple.Create( 10, 9, 3),
            Tuple.Create( 10, 11, 3),
            Tuple.Create( 11, 9, 2),
            Tuple.Create( 11, 10, 3)
        };

        private static Tuple<int, int>[] t3_paths = new Tuple<int, int>[]
        {
            Tuple.Create( 1, 0),
            Tuple.Create( 2, 1),
            Tuple.Create( 3, 4),
            Tuple.Create( 4, 5),
            Tuple.Create( 5, 3),
            Tuple.Create( 6, 4),
            Tuple.Create( 7, 3),
            Tuple.Create( 8, 2),
            Tuple.Create( 9, 3),
            Tuple.Create( 10, 6),
            Tuple.Create(11, 5)
        };
        #endregion
        #region t4
        private static Tuple<int, int, int>[] t4 = new Tuple<int, int, int>[]
        {
            Tuple.Create( 1, 2, 1),
            Tuple.Create( 1, 8, 2),
            Tuple.Create( 2, 1, 1),
            Tuple.Create( 2, 3, 1),
            Tuple.Create( 3, 2, 1),
            Tuple.Create( 3, 4, 1),
            Tuple.Create( 4, 3, 1),
            Tuple.Create( 4, 5, 1),
            Tuple.Create( 5, 4, 1),
            Tuple.Create( 5, 6, 1),
            Tuple.Create( 6, 5, 1),
            Tuple.Create( 6, 7, 1),
            Tuple.Create( 7, 6, 1),
            Tuple.Create( 7, 8, 1),
            Tuple.Create( 8, 7, 1),
            Tuple.Create( 8, 1, 2)
        };
        private static Tuple<int, int>[] t4_paths = new Tuple<int, int>[]
        {
            Tuple.Create( 1, 0), // []
            Tuple.Create( 2, 1), // [2]
            Tuple.Create( 3, 2), // [2, 3]
            Tuple.Create( 4, 3), // [2, 3, 4]
            Tuple.Create( 5, 4), // [2, 3, 4, 5]
            Tuple.Create( 6, 4), // [8, 7, 6]
            Tuple.Create( 7, 3), // [8, 7]
            Tuple.Create( 8, 2)  // [8]
        };
        #endregion
        #endregion

        private static TestCaseData prepareTestData(Tuple<int, int, int>[] input, Tuple<int, int>[] output, string name_ext)
        {
            var data = new TestCaseData(input, output);
            data.TestName = $"{nameof(DijksgraGraphTest.DijkstraShortestPath_Test)}_{name_ext}";
            return data;
        }
        public static IEnumerable TestCases
        {
            get
            {
                yield return prepareTestData(t1, t1_paths, nameof(t1));
                yield return prepareTestData(t2, t2_paths, nameof(t2));
                yield return prepareTestData(t3, t3_paths, nameof(t3));
                yield return prepareTestData(t4, t4_paths, nameof(t4));
            }
        }
    }

    [TestFixture, Category(nameof(Algorithms.Graphs))]
    class DijksgraGraphTest
    {

        [Test, TestCaseSource(typeof(DijkstraTestData), nameof(DijkstraTestData.TestCases))]
        public void DijkstraShortestPath_Test(Tuple<int, int, int>[] input, Tuple<int, int>[] output)
        {
            var gr = new DijkstrasGraph<int>(input);
            gr.EnforceOrder = true;
            gr.CalcPathLen(1);
            Tuple<int,int>[] pathLengths = new Tuple<int,int>[gr.VerticesCount];
            for (int i = 0; i < gr.VerticesCount; i++)
            {
                var v = gr.Vertices[i];
                pathLengths[i] = Tuple.Create(v.Key, v.PathLen);
            }
            Assert.AreEqual(output, pathLengths);
        }
    }
}
