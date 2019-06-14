using System;
using System.Collections.Generic;

namespace Sorting
{
    public static class Algos
    {
        //  if an array is not sorted, it must
        // contain two adjacent elements that are out of order.
        // The algorithm repeatedly passes through the array,
        // swapping items that are out of order, until it
        // can’t find any more swaps.
        // O(N^2)
        public static void BubbleSort<T>(this T[] arr)
            where T: IComparable<T>
        {

            if (arr.Length <= 1)
                return;

            bool notSorted = true;
            while (notSorted)
            {
                // assume we found no pairs to swap
                notSorted = false;
                // Search the array for adjacent items that are out of order.
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    if(arr[i].CompareTo(arr[i + 1]) > 0)
                    {
                        T temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                        // The array isn't sorted after all.
                        notSorted = true;
                    }
                }
            }
        }

        // The algorithm builds a heap. It then repeatedly swaps the first
        // and last items in the heap, and rebuilds the heap excluding the last item.
        // O(N log N)
        public static void HeapSort<T>(this T[] arr)
            where T : IComparable<T>
        {
            Heap<T> heap = new Heap<T>(arr.Length);
            // N log N
            foreach (T item in arr)
            {
                heap.Push(item);
            }
            // + N log N
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = heap.Pop();
            }
        }

        // The quicksort algorithm works by subdividing an array
        // into two pieces and then calling itself recursively to sort the pieces
        // The tree is log N levels tall, and each level requires N steps, so the algorithm’s
        // total run time is O(N log N).
        // the algorithm’s worst-case behavior is O(N2)
        // Mitagation methods:
        // - randomize array before quicksort
        // - pick 3 points and choose middle point
        // - pick random element
        // Also. Algorith could be done in-place
        public static void QuickSortStack<T>(this T[] arr)
            where T : IComparable<T>
        {
            static void QuickSort(T[] arr, int start, int end)
            {
                if (end <= start)
                    return;

                Stack<T> before = new Stack<T>();
                Stack<T> after = new Stack<T>();
                T divider = arr[start]; // see worst case and mitigation methods

                // Gather the items before and after the dividing item.
                for (int i = start + 1; i <= end; i++)
                {
                    if (arr[i].CompareTo(divider) < 0)
                    {
                        before.Push(arr[i]);
                    }
                    else
                    {
                        after.Push(arr[i]);
                    }
                }
                // <Move items in the "before" stack back into the array.>
                int beforeSize = before.Count;
                for (int i = 0; i < beforeSize; i++)
                {
                    arr[start + i] = before.Pop();
                }
                // <Add the dividing item to the array.>
                arr[start + beforeSize] = divider;
                // <Move items in the "after" stack back into the array.>
                int afterSize = after.Count;
                for (int i = 0; i < afterSize; i++)
                {
                    arr[start + beforeSize + 1 + i] = after.Pop();
                }
                // Recursively sort the two halves of the array.
                QuickSort(arr, start, beforeSize - 1);
                QuickSort(arr, start + beforeSize + 1, end);
            }

            QuickSort(arr, 0, arr.Length - 1);
        }

        // O(N log N)
        public static void MergeSort<T>(this T[] arr)
            where T : IComparable<T>
        {
            void Sort(T[] arr, T[] scratch, int start, int end)
            {
                // If the array contains only one item, it is already sorted.
                if (start == end)
                    return;

                // Break the array into left and right halves.
                int midpoint = (start + end) / 2;
                
                // Call Mergesort to sort the two halves.
                Sort(arr, scratch, start, midpoint);
                Sort(arr, scratch, midpoint + 1, end);

                // Merge the two sorted halves.
                int leftIndex = start;
                int rightIndex = midpoint + 1;
                int scratchIndex = leftIndex;
                while ((leftIndex <= midpoint) && (rightIndex <= end))
                {
                    if (arr[leftIndex].CompareTo(arr[rightIndex]) <= 0)
                    {
                        scratch[scratchIndex] = arr[leftIndex];
                        leftIndex++;
                    }
                    else
                    {
                        scratch[scratchIndex] = arr[rightIndex];
                        rightIndex++;
                    }
                    scratchIndex++;
                }
                // Finish copying whichever half is not empty.
                for (int i = leftIndex; i <= midpoint; i++)
                {
                    scratch[scratchIndex] = arr[i];
                    scratchIndex++;
                }
                for (int i = rightIndex; i <= end; i++)
                {
                    scratch[scratchIndex] = arr[i];
                    scratchIndex++;
                }
                // Copy the values back into the original values array.
                for (int i = start; i <= end; i++)
                {
                    arr[i] = scratch[i];
                }
            }

            Sort(arr, new T[arr.Length], 0, arr.Length - 1);
        }

        // The basic idea behind countingsort is to count the number
        // of items in the array that have each value.Then it is
        // relatively easy to copy each value, in order,
        // the required number of times back into the array.
        public static void CountingSort(this int[] arr, int max)
        {
            // Make an array to hold the counts.
            int[] counts = new int[max + 1];
            // Count the items with each value.
            foreach (var item in arr)
            {
                counts[item] = counts[item] + 1;
            }
            // Copy the values back into the array.
            int index = 0;
            for (int i = 0; i <= max; i++)
            {
                for (int j = 0; j < counts[i]; j++)
                {
                    arr[index++] = i;
                }
            }

        }
    }
}
