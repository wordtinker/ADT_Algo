using NUnit.Framework;
using StacksAndQueues;
using System;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Ctor_Stack_empty()
        {
            var stack = new Stack<int>();

            Assert.Throws<Exception>(() => stack.Pop());
        }

        [Test]
        public void Pop_Stack857_7()
        {
            var stack = new Stack<int>();
            stack.Push(8);
            stack.Push(5);
            stack.Push(7);

            var result = stack.Pop();

            Assert.AreEqual(7, result);
        }

        [Test]
        public void Ctor_ArrayStack_empty()
        {
            var stack = new ArrayStack<int>(10);

            Assert.Throws<Exception>(() => stack.Pop());
        }

        [Test]
        public void Pop_ArrayStack857_7()
        {
            var stack = new ArrayStack<int>(10);
            stack.Push(8);
            stack.Push(5);
            stack.Push(7);

            var result = stack.Pop();

            Assert.AreEqual(7, result);
        }

        [Test]
        public void Reverse_85741_14758()
        {
            var stack = new Stack<int>();
            var arr = new int[] { 8, 5, 7, 4, 1 };

            var result = stack.Reverse(arr);

            Assert.AreEqual(new int[] { 1, 4, 7, 5, 8 }, result);
        }

        [Test]
        public void InsertionSort_85741_87541()
        {
            var stack = new Stack<int>();
            stack.Push(8);
            stack.Push(5);
            stack.Push(7);
            stack.Push(4);
            stack.Push(1);

            var result = stack.InsertionSort().ToArray();

            Assert.AreEqual(
                new int[] { 8, 7, 5, 4, 1 },
                result);
        }

        [Test]
        public void SelectionSort_85741_14578()
        {
            var stack = new ArrayStack<int>(10);
            stack.Push(8);
            stack.Push(5);
            stack.Push(7);
            stack.Push(4);
            stack.Push(1);

            stack.SelectionSort();
            var result = stack.ToArray();

            Assert.AreEqual(
                new int[] { 8, 7, 5, 4, 1 },
                result);
        }

        [Test]
        public void Dequeu_857_8()
        {
            var queue = new Queue<int>();
            queue.Enqueue(8);
            queue.Enqueue(5);
            queue.Enqueue(7);


            var result = queue.Dequeue();

            Assert.AreEqual(8, result);
        }

        [Test]
        public void InsertionSort_Queue888811745_87541()
        {
            var queue = new Queue<int>();
            queue.Enqueue(8);
            queue.Enqueue(8);
            queue.Enqueue(8);
            queue.Enqueue(8);
            queue.Enqueue(1);
            queue.Enqueue(1);
            queue.Enqueue(7);
            queue.Enqueue(4);
            queue.Enqueue(5);

            var result = queue.InsertionSort().ToArray();

            Assert.AreEqual(
                new int[] { 8, 8, 8, 8, 7, 5, 4, 1, 1 },
                result);
        }

        [Test]
        public void SelectionSort_Queue888811745_87541()
        {
            var queue = new Queue<int>();
            queue.Enqueue(8);
            queue.Enqueue(8);
            queue.Enqueue(8);
            queue.Enqueue(8);
            queue.Enqueue(1);
            queue.Enqueue(1);
            queue.Enqueue(7);
            queue.Enqueue(4);
            queue.Enqueue(5);

            var result = queue.SelectionSort().ToArray();

            Assert.AreEqual(
                new int[] { 8, 8, 8, 8, 7, 5, 4, 1, 1 },
                result);
        }
    }
}