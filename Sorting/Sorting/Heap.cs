using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/Heap_(data_structure)
    /// </summary>
    public class Heap<T>
        where T: IComparable<T>
    {
        private T[] tree;
        private int size;
        public Heap() : this(1024) { }
        public Heap(int maxSize)
        {
            tree = new T[maxSize];
            size = -1;
        }

        // O(1)
        public T Peek() =>
            (size >= 0) ? tree[0] : throw new IndexOutOfRangeException();
 
        // O(lgN)
        public void Push(T val)
        {
            // keep tree complete
            tree[++size] = val;

            // Keep heap property
            int pos = size;
            while (pos != 0)
            {
                // compare with parent
                int parent = (pos - 1) / 2;
                if (tree[parent].CompareTo(val) >= 0)
                {
                    // swap
                    tree[pos] = tree[parent];
                    tree[parent] = val;
                    // keep consistency up the tree
                    pos = parent;
                }
                else
                {
                    break;
                }
            }
        }

        public T Pop()
        {
            if (size < 0)
                throw new IndexOutOfRangeException();

            // Save value for later
            T val = tree[0];
            // Move the last item to the root.
            tree[0] = tree[size--];
            // Restore the heap property.
            int index = 0;
            while (true)
            {
                // Find the child indices.
                int child1 = 2 * index + 1;
                int child2 = 2 * index + 2;
                // If a child index is off the end of the tree,
                // use the parent's index.
                if (child1 > size)
                    child1 = index;
                if (child2 > size)
                    child2 = index;
                // If the heap property is satisfied,
                // we're done, so break out of the While loop.
                if (tree[index].CompareTo(tree[child1]) <= 0 && tree[index].CompareTo(tree[child2]) <= 0)
                    break;

                // Get the index of the child with the lowest value.
                int swap = tree[child1].CompareTo(tree[child2]) < 0 ? child1 : child2;
                // Swap with the lowest child.
                T temp = tree[index];
                tree[index] = tree[swap];
                tree[swap] = temp;
                // Move to the child node.
                index = swap;
            }
            return val;
        }
    }
}
