using NUnit.Framework;
using Sorting;
using System;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void BubbleSort_57104839n1_n101345789()
        {
            int[] xs = new int[] {5,7,1,0,4,8,3,9,-1 };

            xs.BubbleSort();

            Assert.AreEqual(
                new int[] {-1, 0, 1, 3, 4, 5, 7, 8, 9 },
                xs);
        }

        [Test]
        public void HeapSort_57104839n1_n101345789()
        {
            int[] xs = new int[] { 5, 7, 1, 0, 4, 8, 3, 9, -1 };

            xs.HeapSort();

            Assert.AreEqual(
                new int[] { -1, 0, 1, 3, 4, 5, 7, 8, 9 },
                xs);
        }

        [Test]
        public void QuickSortStack_57104839n1_n101345789()
        {
            int[] xs = new int[] { 5, 7, 1, 0, 4, 8, 3, 9, -1 };

            xs.QuickSortStack();

            Assert.AreEqual(
                new int[] { -1, 0, 1, 3, 4, 5, 7, 8, 9 },
                xs);
        }

        [Test]
        public void MergeSort_57104839n1_n101345789()
        {
            int[] xs = new int[] { 5, 7, 1, 0, 4, 8, 3, 9, -1 };

            xs.MergeSort();

            Assert.AreEqual(
                new int[] { -1, 0, 1, 3, 4, 5, 7, 8, 9 },
                xs);
        }

        [Test]
        public void CountingSort_571048391_011345789()
        {
            int[] xs = new int[] { 5, 7, 1, 0, 4, 8, 3, 9, 1 };

            xs.CountingSort(9);

            Assert.AreEqual(
                new int[] { 0, 1, 1, 3, 4, 5, 7, 8, 9 },
                xs);
        }

        [Test]
        public void Ctor_Peek_Exception()
        {
            var heap = new Heap<int>();

            Assert.Throws<IndexOutOfRangeException>(() => heap.Peek());
        }

        [Test]
        public void Push10Peek_EmptyHeap_10()
        {
            var heap = new Heap<int>();
            heap.Push(10);

            int val = heap.Peek();

            Assert.AreEqual(10, val);
        }

        [Test]
        public void Push11Peek_Heap10_10()
        {
            var heap = new Heap<int>();
            heap.Push(10);

            heap.Push(11);
            int val = heap.Peek();

            Assert.AreEqual(10, val);
        }

        [Test]
        public void Push9Peek_Heap10_9()
        {
            var heap = new Heap<int>();
            heap.Push(10);

            heap.Push(9);
            int val = heap.Peek();

            Assert.AreEqual(9, val);
        }

        [Test]
        public void Pop_Heap645_4()
        {
            var heap = new Heap<int>();
            heap.Push(6);
            heap.Push(4);
            heap.Push(5);

            int val = heap.Pop();

            Assert.AreEqual(4, val);
        }
    }
}