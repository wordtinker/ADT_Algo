using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedList
{
    public class Cell<T>
        where T : struct, IComparable<T>
    {
        public T Value { get; }
        public Cell<T>? Next { get; set; }
        public bool Visited { get; set; }
        public Cell(T value)
        {
            Value = value;
            Next = null;
        }
    }

    public abstract class LinkedList<T> : IEnumerable<T>
        where T : struct, IComparable<T>
    {
        // Fake head of the list.
        // Simplifies algorithms.
        // Cell value is not relevant.
        protected readonly Cell<T> sentinel = new Cell<T>(default);

        // O(N)
        public IEnumerator<T> GetEnumerator()
        {
            Cell<T>? cell = sentinel.Next;
            while (cell != null)
            {
                yield return cell.Value;
                cell = cell.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // O(N)
        public void Delete(T target)
        {
            Cell<T> cell = sentinel;

            while (cell.Next != null && !target.Equals(cell.Next.Value))
            {
                cell = cell.Next;
            }

            cell.Next = cell.Next?.Next;
        }

        // O(N)
        public Cell<T>? Find(T target)
        {
            Cell<T>? cell = sentinel.Next;
            while (cell != null)
            {
                T val = cell.Value;
                if (target.Equals(val))
                    return cell;
                cell = cell.Next;
            }
            return null;
        }

        // O(N)
        public T? Max()
        {
            Cell<T>? beforeMax = sentinel;
            Cell<T>? cell = sentinel.Next;

            while (cell != null && cell.Next != null)
            {
                if (beforeMax.Next?.Value.CompareTo(cell.Next.Value) < 0)
                {
                    beforeMax = cell;
                }
                cell = cell.Next;
            }

            T? max = beforeMax.Next?.Value;
            beforeMax.Next = beforeMax.Next?.Next;
            return max;
        }

        public abstract LinkedList<T> Clone();
        // O(N)
        protected LinkedList<T> Clone(LinkedList<T> newList)
        {
            // Keep track of the last item we've added so far.
            Cell<T> lastAdded = newList.sentinel;
            // Skip old sentinel
            Cell<T>? oldCell = sentinel.Next;
            // Copy items
            while (oldCell != null)
            {
                // make a new item
                lastAdded.Next = new Cell<T>(oldCell.Value);
                // move to the next item
                lastAdded = lastAdded.Next;
                // Get ready to copy next cell
                oldCell = oldCell.Next;
            }
            return newList;
        }

        // O(N)
        public LinkedList<T> RemoveDuplicates()
        {
            HashSet<T> set = new HashSet<T>();
            Cell<T> cell = sentinel;

            while (cell.Next != null)
            {
                if (set.Contains(cell.Next.Value))
                {
                    cell.Next = cell.Next.Next;
                }
                else
                {
                    set.Add(cell.Next.Value);
                    cell = cell.Next;
                }
            }

            return this;
        }

        // O(N)
        public T KthToLast(int k)
        {
            if (sentinel.Next is Cell<T> head)
            {
                Cell<T> leftGuard = head;
                Cell<T> rightGuard = head;

                // move right guard k times
                for (int i = 0; i < k; i++)
                {
                    if (rightGuard.Next is null)
                        throw new IndexOutOfRangeException();
                    rightGuard = rightGuard.Next;
                }

                // move 'window' to the right till the last element
                while (rightGuard.Next != null)
                {
                    rightGuard = rightGuard.Next;
                    leftGuard = leftGuard.Next;
                }

                return leftGuard.Value;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
    }

    public class SingleLinkedList<T> : LinkedList<T>
        where T : struct, IComparable<T>
    {
        // O(1)
        public void InsertFront(T newVal)
        {
            Cell<T> newCell = new Cell<T>(newVal)
            {
                Next = sentinel.Next
            };
            sentinel.Next = newCell;
        }

        // O(N)
        public void InsertAtEnd(T newVal)
        {
            Cell<T> newCell = new Cell<T>(newVal);
            
            // Find the last cell
            Cell<T> tail = sentinel;
            while (tail?.Next != null)
            {
                tail = tail.Next;
            }
            // Add the new cell at the end.
            if (tail != null)
                tail.Next = newCell;
        }

        public override LinkedList<T> Clone() =>
            Clone(new SingleLinkedList<T>());

        public static SingleLinkedList<T> GenerateCyclic(T[] head, T[] cycle)
        {
            var list = new SingleLinkedList<T>();
            var cell = list.sentinel;
            foreach (var item in head)
            {
                cell.Next = new Cell<T>(item);
                cell = cell.Next;
            }

            var cPoint = new Cell<T>(cycle[0]);
            cell.Next = cPoint;
            cell = cell.Next;

            for (int i = 1; i < cycle.Length; i++)
            {
                cell.Next = new Cell<T>(cycle[i]);
                cell = cell.Next;
            }

            cell.Next = cPoint;

            return list;
        }

        private Cell<T>? MarkVisit(bool mark)
        {
            Cell<T>? cell = sentinel;
            while (cell != null)
            {
                cell.Visited = mark;
                if (cell.Next?.Visited == mark)
                {
                    return cell.Next;
                }
                // Move to next cell
                cell = cell.Next;
            }
            return null;
        }

        private Cell<T>? CheckHash()
        {
            var set = new HashSet<Cell<T>>();
            Cell<T>? cell = sentinel;
            while (cell != null)
            {
                set.Add(cell);
                if (cell.Next != null && set.Contains(cell.Next))
                {
                    return cell.Next;
                }
                // Move to next cell
                cell = cell.Next;
            }
            return null;
        }

        public bool ContainsLoop()
        {
            bool has_loop = false;
            // traverse the list and mark cells as visited
            if (MarkVisit(true) is Cell<T>)
            {
                has_loop = true;
            }

            // traverse the list again and unmark
            MarkVisit(false);

            return has_loop;
        }

        public bool ContainsLoopHashSet()
        {
            if (CheckHash() is Cell<T>)
            {
                return true;
            }

            return false;
        }

        public Cell<T>? LoopStart()
        {
            // traverse the list and mark cells as visited
            var start = MarkVisit(true);
            // traverse the list again and unmark
            MarkVisit(false);

            return start;
        }

        public Cell<T>? LoopStartHash() =>
            CheckHash();
    }

    public class SortedSingleLinkedList<T> : LinkedList<T>
        where T : struct, IComparable<T>
    {
        // O(N)
        public void Insert(T newVal)
        {
            Cell<T> newCell = new Cell<T>(newVal);

            // Find the cell before where the new cell belongs.
            Cell<T> cell = sentinel;
            while (cell.Next != null && cell.Next.Value.CompareTo(newCell.Value) > 0)
            {
                cell = cell.Next;
            }
            // Add the new cell after cell.
            newCell.Next = cell.Next;
            cell.Next = newCell;
        }

        public override LinkedList<T> Clone() =>
            Clone(new SortedSingleLinkedList<T>());
    }

    public static class Sorting
    {
        // O(N^2)
        // Insertion sort
        // The basic idea behind insertionsort is to take an item
        // from the input list and insert it into the proper position
        // in a sorted output list(which initially starts empty).
        public static SortedSingleLinkedList<T> InsertionSort<T>(this SingleLinkedList<T> unsorted)
            where T : struct, IComparable<T>
        {
            var sorted = new SortedSingleLinkedList<T>();
            foreach (var item in unsorted) // O(N)
            {
                sorted.Insert(item); // O(N)
            }
            return sorted;
        }

        // O(N^2)
        // The basic idea behind the selectionsort algorithm is to
        // search the input list for the largest item it contains
        // and then add it to the front of a growing sorted list.
        public static SingleLinkedList<T> SelectionSort<T>(this SingleLinkedList<T> unsorted)
            where T : struct, IComparable<T>
        {
            var sorted = new SingleLinkedList<T>();
            T? max;
            while ((max = unsorted.Max()) != null) // O(N) * O(N)
            {
                sorted.InsertFront(max.Value);
            }
            return sorted;
        }
    }
}
