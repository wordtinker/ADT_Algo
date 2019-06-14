using System;
using System.Collections.Generic;

namespace Trees
{

    public abstract class Node<T, U>
        where T : class
    {
        public U Value { get; protected set; }
        public T? Left { get; set; }
        public T? Right { get; set; }
        public Node(U value)
        {
            Value = value;
        }
    }

    public class BinaryNode<U> : Node<BinaryNode<U>, U>
    {
        public BinaryNode(U value) : base(value) { }
    }

    public class SortedTree<U> : Node<SortedTree<U>, U>
        where U : IComparable
    {
        public SortedTree<U>? Parent { get; set; }
        public SortedTree(U value) : base(value) { }

        public void Add(U newValue)
        {
            if (newValue.CompareTo(Value) < 0)
            {
                // The new value is smaller. Add it to the left subtree.
                if (Left is null)
                {
                    Left = new SortedTree<U>(newValue);
                    Left.Parent = this;
                }
                else
                {
                    Left.Add(newValue);
                }
            }
            else
            {
                if (Right is null)
                {
                    Right = new SortedTree<U>(newValue);
                    Right.Parent = this;
                }
                else
                {
                    Right.Add(newValue);
                }
            }
        }

        public SortedTree<U>? FindNode(U target)
        {
            // If we've found the target value, return this node.
            if (target.Equals(Value))
                return this;
            // See if the desired value is in the left or right subtree.
            if (target.CompareTo(Value) < 0)
            {
                // Search the left subtree.
                return Left?.FindNode(target);
            }
            else
            {
                return Right?.FindNode(target);
            }
        }

        public void Delete(U target)
        {
            static void Reparent(SortedTree<U>? oldChild, SortedTree<U>? child)
            {
                var parent = oldChild?.Parent;
                if (parent != null)
                {
                    if (oldChild?.Value.CompareTo(parent.Value) >= 0)
                    {
                        parent.Right = child;
                    }
                    else
                    {
                        parent.Left = child;
                    }
                }
                if (child != null)
                    child.Parent = parent;
            }

            var node = FindNode(target);
            if (node == null)
                return;

            // No children
            if (node.Left == null && node.Right == null)
            {
                Reparent(node, null);
            }
            else if (node.Left != null && node.Right != null)
            {
                // the general strategy is to replace the node with its left child
                if (node.Left.Right == null)
                {
                    Reparent(node, node.Left);
                    node.Left.Right = node.Right;
                }
                else
                {
                    // find the rightmost node below the target node’s left child
                    var rightmost = node.Left.Right;
                    while (rightmost.Right != null)
                    {
                        rightmost = rightmost.Right;
                    }
                    if (rightmost.Left == null)
                    {
                        rightmost.Parent.Right = null;
                        Reparent(node, rightmost);
                        rightmost.Left = node.Left;
                        rightmost.Right = node.Right;
                    }
                    else
                    {
                        node.Value = rightmost.Value;
                        rightmost.Value = rightmost.Left.Value;
                        rightmost.Left = null;
                    }
                }
            }
            // only one leaf LEFT
            else if (node.Left != null)
            {
                Reparent(node, node.Left);
            }
            // only one leaf RIGHT
            else
            {
                Reparent(node, node.Right);
            }
        }
    }

    public static class Traversal
    {
        /// <summary>
        /// Return node, left, right
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<U> PreorderTraversal<T, U>(this Node<T, U> root)
            where T : class
        {
            yield return root.Value;
            if (root.Left != null)
            {
                foreach (U item in PreorderTraversal(root.Left as Node<T, U>))
                {
                    yield return item;
                }
            }
            if (root.Right != null)
            {
                foreach (var item in PreorderTraversal(root.Right as Node<T, U>))
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Return left, node, right
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<U> InorderTraversal<T, U>(this Node<T, U> root)
            where T : class
        {
            if (root.Left != null)
            {
                foreach (var item in InorderTraversal(root.Left as Node<T, U>))
                {
                    yield return item;
                }
            }
            yield return root.Value;
            if (root.Right != null)
            {
                foreach (var item in InorderTraversal(root.Right as Node<T, U>))
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Return left, right, node
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<U> PostorderTraversal<T, U>(this Node<T, U> root)
            where T : class
        {
            if (root.Left != null)
            {
                foreach (var item in PostorderTraversal(root.Left as Node<T, U>))
                {
                    yield return item;
                }
            }
            if (root.Right != null)
            {
                foreach (var item in PostorderTraversal(root.Right as Node<T, U>))
                {
                    yield return item;
                }
            }
            yield return root.Value;
        }

        /// <summary>
        /// In a depth-first traversal, the algorithm processes all
        /// the nodes at a given level of the tree in left-to-right
        /// order before processing the nodes at the next level.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<U> DepthFirst<T, U>(this Node<T, U> root)
            where T : class
        {
            var queue = new Queue<Node<T, U>>();
            if (root != null)
                queue.Enqueue(root);

            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                if (node.Left != null)
                    queue.Enqueue(node.Left as Node<T, U>);
                if (node.Right != null)
                    queue.Enqueue(node.Right as Node<T, U>);
                yield return node.Value;
            }
        }
    }
}
