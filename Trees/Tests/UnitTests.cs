using NUnit.Framework;
using System.Linq;
using Trees;

namespace Tests
{
    [TestFixture]
    public class SortedTree
    {
        [Test]
        public void AddLower_Root_AddedToTheLeft()
        {
            var root = new SortedTree<int>(60);

            root.Add(58);

            Assert.AreEqual(58, root.Left.Value);
        }

        [Test]
        public void AddHigher_Root_AddedToTheRight()
        {
            var root = new SortedTree<int>(60);

            root.Add(61);

            Assert.AreEqual(61, root.Right.Value);
        }

        [Test]
        public void FindNode_tree_NotFound()
        {
            var root = new SortedTree<int>(60);
            root.Add(61);
            root.Add(59);

            var result = root.FindNode(20);

            Assert.IsNull(result);
        }

        [Test]
        public void FindNode_tree_Found()
        {
            var root = new SortedTree<int>(60);
            root.Add(61);
            root.Add(59);

            var result = root.FindNode(59);

            Assert.AreEqual(59, result.Value);
        }

        [Test]
        public void Delete_tree_NotFound()
        {
            var root = new SortedTree<int>(60);
            root.Add(35);
            root.Add(76);
            root.Add(21);
            root.Add(42);
            root.Add(71);
            root.Add(89);
            root.Add(17);
            root.Add(24);
            root.Add(68);
            root.Add(11);
            root.Add(23);
            root.Add(63);
            root.Add(69);

            root.Delete(0);

            var tree = root.DepthFirst().ToArray();
            Assert.AreEqual(
                new int[] {60, 35, 76, 21, 42, 71, 89, 17, 24, 68, 11, 23, 63,69 },
                tree);
        }

        [Test]
        public void DeleteLeaf_tree_Culled()
        {
            var root = new SortedTree<int>(60);
            root.Add(35);
            root.Add(76);
            root.Add(21);
            root.Add(42);
            root.Add(71);
            root.Add(89);
            root.Add(17);
            root.Add(24);
            root.Add(68);
            root.Add(11);
            root.Add(23);
            root.Add(63);
            root.Add(69);

            root.Delete(89);

            var tree = root.DepthFirst().ToArray();
            Assert.AreEqual(
                new int[] { 60, 35, 76, 21, 42, 71, 17, 24, 68, 11, 23, 63, 69 },
                tree);
        }

        [Test]
        public void DeleteLeafWithLeftChild_tree_Culled()
        {
            var root = new SortedTree<int>(60);
            root.Add(35);
            root.Add(76);
            root.Add(21);
            root.Add(42);
            root.Add(71);
            root.Add(89);
            root.Add(17);
            root.Add(24);
            root.Add(68);
            root.Add(11);
            root.Add(23);
            root.Add(63);
            root.Add(69);

            root.Delete(71);

            var tree = root.DepthFirst().ToArray();
            Assert.AreEqual(
                new int[] { 60, 35, 76, 21, 42, 68, 89, 17, 24, 63, 69, 11, 23 },
                tree);
        }

        [Test]
        public void DeleteLeafWithRightChild_tree_Culled()
        {
            var root = new SortedTree<int>(60);
            root.Add(35);
            root.Add(76);
            root.Add(80);
            root.Add(82);

            root.Delete(80);

            var tree = root.DepthFirst().ToArray();
            Assert.AreEqual(
                new int[] { 60, 35, 76, 82 },
                tree);
        }

        [Test]
        public void DeleteLeafWithTwoChildrenLCaseOne_tree_Culled()
        {
            var root = new SortedTree<int>(60);
            root.Add(35);
            root.Add(76);
            root.Add(21);
            root.Add(42);
            root.Add(68);
            root.Add(17);
            root.Add(24);
            root.Add(63);
            root.Add(69);
            root.Add(11);
            root.Add(23);

            root.Delete(21);

            var tree = root.DepthFirst().ToArray();
            Assert.AreEqual(
                new int[] { 60, 35, 76, 17, 42, 68, 11, 24, 63, 69, 23 },
                tree);
        }

        [Test]
        public void DeleteLeafWithTwoChildrenLCaseTwoA_tree_Culled()
        {
            var root = new SortedTree<int>(60);
            root.Add(35);
            root.Add(76);
            root.Add(17);
            root.Add(42);
            root.Add(68);
            root.Add(11);
            root.Add(24);
            root.Add(63);
            root.Add(69);
            root.Add(23);

            root.Delete(35);

            var tree = root.DepthFirst().ToArray();
            Assert.AreEqual(
                new int[] { 60, 24, 76, 17, 42, 68, 11, 23, 63, 69 },
                tree);
        }

        [Test]
        public void DeleteLeafWithTwoChildrenLCaseTwoB_tree_Culled()
        {
            var root = new SortedTree<int>(60);
            root.Add(35);
            root.Add(76);
            root.Add(17);
            root.Add(42);
            root.Add(68);
            root.Add(11);
            root.Add(24);
            root.Add(63);
            root.Add(69);

            root.Delete(35);

            var tree = root.DepthFirst().ToArray();
            Assert.AreEqual(
                new int[] { 60, 24, 76, 17, 42, 68, 11, 63, 69 },
                tree);
        }
    }
    [TestFixture]
    public class BinaryNode
    {

        [Test]
        public void Preorder()
        {
            var d = new BinaryNode<string>("D");
            var b = new BinaryNode<string>("B");
            var e = new BinaryNode<string>("E");
            var a = new BinaryNode<string>("A");
            var c = new BinaryNode<string>("C");
            d.Left = b;
            d.Right = e;
            b.Left = a;
            b.Right = c;

            var order = d.PreorderTraversal().ToArray();

            Assert.AreEqual(
                new string[] { "D", "B", "A", "C", "E"},
                order);
        }

        [Test]
        public void Inorder()
        {
            var d = new BinaryNode<string>("D");
            var b = new BinaryNode<string>("B");
            var e = new BinaryNode<string>("E");
            var a = new BinaryNode<string>("A");
            var c = new BinaryNode<string>("C");
            d.Left = b;
            d.Right = e;
            b.Left = a;
            b.Right = c;

            var order = d.InorderTraversal().ToArray();

            Assert.AreEqual(
                new string[] { "A", "B", "C", "D", "E" },
                order);
        }

        [Test]
        public void Postorder()
        {
            var d = new BinaryNode<string>("D");
            var b = new BinaryNode<string>("B");
            var e = new BinaryNode<string>("E");
            var a = new BinaryNode<string>("A");
            var c = new BinaryNode<string>("C");
            d.Left = b;
            d.Right = e;
            b.Left = a;
            b.Right = c;

            var order = d.PostorderTraversal().ToArray();

            Assert.AreEqual(
                new string[] { "A", "C", "B", "E", "D" },
                order);
        }

        [Test]
        public void DepthFirst()
        {
            var d = new BinaryNode<string>("D");
            var b = new BinaryNode<string>("B");
            var e = new BinaryNode<string>("E");
            var a = new BinaryNode<string>("A");
            var c = new BinaryNode<string>("C");
            d.Left = b;
            d.Right = e;
            b.Left = a;
            b.Right = c;

            var order = d.DepthFirst().ToArray();

            Assert.AreEqual(
                new string[] { "D", "B", "E", "A", "C" },
                order);
        }
    }
}