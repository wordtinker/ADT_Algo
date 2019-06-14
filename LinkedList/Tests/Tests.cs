using LinkedList;
using NUnit.Framework;
using System;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Ctor_Cell_HasValue()
        {
            Cell<int> cell = new Cell<int>(5);

            int val = cell.Value;

            Assert.AreEqual(5, val);
        }

        [Test]
        public void Ctor_Cell_NextIsNull()
        {
            Cell<int> cell = new Cell<int>(5);

            Cell<int>? next = cell.Next;

            Assert.IsNull(next);
        }

        [Test]
        public void Ctor_SingleLinkedList_IsEmpty()
        {
            SingleLinkedList<int> emptyList = new SingleLinkedList<int>();

            int[] numbers = emptyList.ToArray();

            Assert.AreEqual(0, numbers.Length);
        }

        [Test]
        public void InsertFront_EmptySingleLinkedList_HasOneValue()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertFront(10);

            int[] numbers = list.ToArray();

            Assert.AreEqual(1, numbers.Length);
        }

        [Test]
        public void InsertFront_EmptySingleLinkedList_ReturnsProperValue()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertFront(10);

            int[] numbers = list.ToArray();

            Assert.AreEqual(10, numbers[0]);
        }

        [Test]
        public void Iterate_SingleLinkedList_InProperOrder()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertFront(1);
            list.InsertFront(2);
            list.InsertFront(3);

            int[] numbers = list.ToArray();

            Assert.AreEqual(new int[] { 3, 2, 1 }, numbers);
        }
        [Test]
        public void InsertAtEnd_EmptySingleLinkedList_HasOneValue()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertAtEnd(10);

            int[] numbers = list.ToArray();

            Assert.AreEqual(1, numbers.Length);
        }

        [Test]
        public void InsertAtEnd_EmptySingleLinkedList_ReturnsProperValue()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertAtEnd(10);

            int[] numbers = list.ToArray();

            Assert.AreEqual(10, numbers[0]);
        }

        [Test]
        public void IterateAfterInsertAtEnd_SingleLinkedList_InProperOrder()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertAtEnd(1);
            list.InsertAtEnd(2);
            list.InsertAtEnd(3);

            int[] numbers = list.ToArray();

            Assert.AreEqual(new int[] { 1, 2, 3 }, numbers);
        }

        [Test]
        public void Find2_123_ReturnsCellWith2()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertAtEnd(1);
            list.InsertAtEnd(2);
            list.InsertAtEnd(3);

            Cell<int>? cell = list.Find(2);

            Assert.AreEqual(2, cell?.Value);
        }

        [Test]
        public void Find4_123_ReturnsNull()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertAtEnd(1);
            list.InsertAtEnd(2);
            list.InsertAtEnd(3);

            Cell<int>? cell = list.Find(4);

            Assert.IsNull(cell);
        }

        [Test]
        public void Delete2_123_Returns13()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertAtEnd(1);
            list.InsertAtEnd(2);
            list.InsertAtEnd(3);

            list.Delete(2);

            int[] numbers = list.ToArray();
            Assert.AreEqual(new int[] {1, 3}, numbers);
        }

        [Test]
        public void Delete4_123_Returns123()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertAtEnd(1);
            list.InsertAtEnd(2);
            list.InsertAtEnd(3);

            list.Delete(4);

            int[] numbers = list.ToArray();
            Assert.AreEqual(new int[] { 1, 2, 3 }, numbers);
        }

        [Test]
        public void Clone_123_Equals123()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertAtEnd(1);
            list.InsertAtEnd(2);
            list.InsertAtEnd(3);

            var copy = list.Clone();

            int[] numbers = list.ToArray();
            int[] copyNumbers = copy.ToArray();
            Assert.AreEqual(numbers, copyNumbers);
        }

        [Test]
        public void Insert_OrderedList_IteratedInOrder()
        {
            SortedSingleLinkedList<int> list = new SortedSingleLinkedList<int>();

            list.Insert(4);
            list.Insert(10);
            list.Insert(7);
            list.Insert(5);
            list.Insert(6);
            list.Insert(7);
            list.Insert(10);


            int[] numbers = list.ToArray();
            Assert.AreEqual(new int[] { 10, 10, 7, 7, 6, 5, 4 }, numbers);
        }

        [Test]
        public void Max_911_9()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertAtEnd(9);
            list.InsertAtEnd(1);
            list.InsertAtEnd(1);

            int? max = list.Max().GetValueOrDefault(0);

            Assert.AreEqual(9, max);
        }

        [Test]
        public void Max_191_9()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertAtEnd(1);
            list.InsertAtEnd(9);
            list.InsertAtEnd(1);

            int? max = list.Max().GetValueOrDefault(0);

            Assert.AreEqual(9, max);
        }

        [Test]
        public void Max_119_9()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertAtEnd(1);
            list.InsertAtEnd(1);
            list.InsertAtEnd(9);

            int? max = list.Max().GetValueOrDefault(0);

            Assert.AreEqual(9, max);
        }

        [Test]
        public void Max_Empty_null()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();

            int? max = list.Max();

            Assert.IsNull(max);
        }

        [Test]
        public void InsertionSort_LinkedList_Ordered()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertAtEnd(15);
            list.InsertAtEnd(4);
            list.InsertAtEnd(21);
            list.InsertAtEnd(3);
            list.InsertAtEnd(400);
            list.InsertAtEnd(21);

            var sorted = list.InsertionSort();

            int[] numbers = sorted.ToArray();
            Assert.AreEqual(new int[] {400, 21, 21, 15, 4, 3 }, numbers);
        }

        [Test]
        public void SelectionSort_LinkedList_Ordered()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertAtEnd(15);
            list.InsertAtEnd(4);
            list.InsertAtEnd(21);
            list.InsertAtEnd(3);
            list.InsertAtEnd(400);
            list.InsertAtEnd(21);

            var sorted = list.SelectionSort();

            int[] numbers = sorted.ToArray();
            Assert.AreEqual(new int[] { 3, 4, 15, 21, 21, 400 }, numbers);
        }

        [Test]
        public void RemoveDuplicates_71554887_71548()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertAtEnd(7);
            list.InsertAtEnd(1);
            list.InsertAtEnd(5);
            list.InsertAtEnd(5);
            list.InsertAtEnd(4);
            list.InsertAtEnd(8);
            list.InsertAtEnd(8);
            list.InsertAtEnd(7);

            var cleared = list.RemoveDuplicates();

            int[] numbers = cleared.ToArray();
            Assert.AreEqual(new int[] { 7, 1, 5, 4, 8 }, numbers);
        }

        [TestCase(0, 4)]
        [TestCase(1, 5)]
        [TestCase(2, 1)]
        [TestCase(3, 7)]
        public void KToLast_7154_Expected(int k, int expect)
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertAtEnd(7);
            list.InsertAtEnd(1);
            list.InsertAtEnd(5);
            list.InsertAtEnd(4);

            int kth = list.KthToLast(k);

            Assert.AreEqual(expect, kth);
        }

        [Test]
        public void TwoToLast_EmptyList_error()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();

            Assert.Throws<IndexOutOfRangeException>(
                () => list.KthToLast(2)
                );
        }

        [Test]
        public void FiveToLast_EmptyList_error()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertAtEnd(7);
            list.InsertAtEnd(1);
            list.InsertAtEnd(5);
            list.InsertAtEnd(4);

            Assert.Throws<IndexOutOfRangeException>(
                () => list.KthToLast(5)
                );
        }

        [Test]
        public void ContainsLoop_7154c52874_true()
        {
            SingleLinkedList<int> list = SingleLinkedList<int>.GenerateCyclic(
                new int[] {7,1,5,4 },
                new int[] {5,2,8,7,4 }
                );

            var cyclic = list.ContainsLoop();

            Assert.IsTrue(cyclic);
        }

        [Test]
        public void ContainsLoop_7154_false()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertAtEnd(7);
            list.InsertAtEnd(1);
            list.InsertAtEnd(5);
            list.InsertAtEnd(4);

            var cyclic = list.ContainsLoop();

            Assert.IsFalse(cyclic);
        }

        [Test]
        public void LoopStart_7154c52874_5()
        {
            SingleLinkedList<int> list = SingleLinkedList<int>.GenerateCyclic(
                new int[] { 7, 1, 5, 4 },
                new int[] { 5, 2, 8, 7, 4 }
                );

            var cycleStart = list.LoopStart();

            Assert.AreEqual(5, cycleStart?.Value);
        }

        [Test]
        public void ContainsLoopHash_7154c52874_true()
        {
            SingleLinkedList<int> list = SingleLinkedList<int>.GenerateCyclic(
                new int[] { 7, 1, 5, 4 },
                new int[] { 5, 2, 8, 7, 4 }
                );

            var cyclic = list.ContainsLoopHashSet();

            Assert.IsTrue(cyclic);
        }

        [Test]
        public void ContainsLoopHash_7154_false()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.InsertAtEnd(7);
            list.InsertAtEnd(1);
            list.InsertAtEnd(5);
            list.InsertAtEnd(4);

            var cyclic = list.ContainsLoopHashSet();

            Assert.IsFalse(cyclic);
        }

        [Test]
        public void LoopStartHash_7154c52874_5()
        {
            SingleLinkedList<int> list = SingleLinkedList<int>.GenerateCyclic(
                new int[] { 7, 1, 5, 4 },
                new int[] { 5, 2, 8, 7, 4 }
                );

            var cycleStart = list.LoopStartHash();

            Assert.AreEqual(5, cycleStart?.Value);
        }
    }
}