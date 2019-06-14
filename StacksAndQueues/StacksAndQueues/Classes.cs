using System;
using System.Collections;
using System.Collections.Generic;

namespace StacksAndQueues
{
    public class Cell<T>
        where T : struct, IComparable<T>
    {
        public T Value { get; }
        public Cell<T>? Next { get; set; }
        public Cell<T>? Prev { get; set; }
        public Cell(T value)
        {
            Value = value;
        }
    }

    // using linked list
    public class Stack<T> : IEnumerable<T>
        where T : struct, IComparable<T>
    {

        private readonly Cell<T> sentinel = new Cell<T>(default);

        // O(1)
        public void Push(T val)
        {
            var cell = new Cell<T>(val)
            {
                Next = sentinel.Next
            };
            sentinel.Next = cell;
        }

        // O(1)
        public T Pop()
        {
            if (sentinel.Next is null)
                throw new Exception("Stack is empty");

            T result = sentinel.Next.Value;
            sentinel.Next = sentinel.Next.Next;

            return result;
        }

        // O(N)
        public T[] Reverse(T[] values)
        {
            Stack<T> stack = new Stack<T>();
            // Push the values from the array onto the stack.
            foreach (var item in values)
            {
                stack.Push(item);
            }
            // Pop the items off the stack into the array.
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = stack.Pop();
            }
            return values;
        }

        // O(1)
        public T Peek()
        {
            if (sentinel.Next is null)
                throw new Exception("Stack is empty");
            return sentinel.Next.Value;
        }

        // O(N^2)
        public Stack<T> InsertionSort()
        {
            Stack<T> tempStack = new Stack<T>();

            while (sentinel.Next != null)
            {
                T newData = Pop();

                // while temporary stack is not empty and  
                // top of stack is greater than temp 
                while (tempStack.sentinel.Next != null && tempStack.Peek().CompareTo(newData) > 0)
                {
                    // pop from temporary stack and  
                    // push it to the input stack
                    Push(tempStack.Pop());
                }
                // push temp in tempory of stack  
                tempStack.Push(newData);
            }

            return tempStack;
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (sentinel.Next != null)
            {
                yield return Pop();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }

    // Array based
    public class ArrayStack<T> : IEnumerable<T>
        where T : struct, IComparable<T>
    {
        private int nextIndex;
        private T[] stack;
        public ArrayStack(int size)
        {
            stack = new T[size];
        }

        // O(1)
        public void Push(T val)
        {
            if (nextIndex == stack.Length)
                throw new Exception("Stack overflow.");

            // Add the new item.
            stack[nextIndex] = val;

            nextIndex++;
        }

        // O(1)
        public T Pop()
        {
            if (nextIndex == 0)
                throw new Exception("Stack is empty");

            nextIndex--;

            return stack[nextIndex];
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (nextIndex != 0)
            {
                yield return Pop();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        // O(N^2)
        public void SelectionSort()
        {
            int numOfElements = nextIndex;
            ArrayStack<T> tempStack = new ArrayStack<T>(numOfElements);

            // for each element
            for (int k = 0; k < numOfElements; k++)
            {
                // get kth unsorted elements from array
                // find min value
                T? min = null;
                for (int i = numOfElements - k; i > 0; i--)
                {
                    T val = Pop();
                    if (min is null)
                    {
                        min = val;
                    }
                    else if (val.CompareTo(min.Value) < 0)
                    {
                        tempStack.Push(min.Value);
                        min = val;
                    }
                    else
                    {
                        tempStack.Push(val);
                    }
                }
                // push min value to sorted part of stack
                Push(min.Value);
                // push unsorted items back to unsorted part
                foreach (var item in tempStack)
                {
                    Push(item);
                }
            }
        }
    }

    public class Queue<T> : IEnumerable<T>
        where T : struct, IComparable<T>
    {
        private readonly Cell<T> topSentinel = new Cell<T>(default);
        private readonly Cell<T> bottomSentinel = new Cell<T>(default);

        public int Size { get; private set; }

        public Queue()
        {
            topSentinel.Next = bottomSentinel;
            bottomSentinel.Prev = topSentinel;
        }

        // O(1)
        public void Enqueue(T val)
        {
            var cell = new Cell<T>(val);

            cell.Next = topSentinel.Next;
            cell.Next.Prev = cell;
            topSentinel.Next = cell;
            cell.Prev = topSentinel;
            Size++;
        }

        // O(1)
        public T Dequeue()
        {
            if (IsEmpty)
                throw new Exception("Stack is empty");
            // Get the bottom cell's value
            T val = bottomSentinel.Prev.Value;
            // Remove the bottom cell from the linked list.
            bottomSentinel.Prev = bottomSentinel.Prev.Prev;
            bottomSentinel.Prev.Next = bottomSentinel;

            Size--;
            return val;
        }

        // O(1)
        public T Peek()
        {
            if (IsEmpty)
                throw new Exception("Stack is empty");
            return bottomSentinel.Prev.Value;
        }

        public bool IsEmpty => topSentinel.Next == bottomSentinel;

        // O(N^2)
        public Queue<T> InsertionSort()
        {
            Queue<T> tempQueue = new Queue<T>();

            while (!IsEmpty)
            {
                // Get the value
                T val = Dequeue();
                // insert into proper position
                if (tempQueue.IsEmpty)
                {
                    tempQueue.Enqueue(val);
                    continue;
                }

                // move greater elements around
                // using sized loop prevents infinite loops
                for (int i = 0; i < tempQueue.Size; i++)
                {
                    if (tempQueue.Peek().CompareTo(val) > 0)
                    {
                        tempQueue.Enqueue(tempQueue.Dequeue());
                    }
                    else
                    {
                        break;
                    }
                }
                // put into proper position
                tempQueue.Enqueue(val);
                // move lesser elements around
                for (int i = 0; i < tempQueue.Size; i++)
                {
                    if (tempQueue.Peek().CompareTo(val) <= 0)
                    {
                        tempQueue.Enqueue(tempQueue.Dequeue());
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return tempQueue;
        }

        // O(N^2)
        public Queue<T> SelectionSort()
        {
            Queue<T> tempQueue = new Queue<T>();

            int size = Size;
            for (int k = 0; k < size; k++)
            {
                // get kth unsorted elements from queue
                // find min value
                T? max = null;
                for (int i = 0; i < size - k; i++)
                {
                    T val = Dequeue();
                    if (max is null)
                    {
                        max = val;
                    }
                    else if (val.CompareTo(max.Value) > 0)
                    {
                        Enqueue(max.Value);
                        max = val;
                    }
                    else
                    {
                        Enqueue(val);
                    }
                }
                tempQueue.Enqueue(max.Value);
            }
            return tempQueue;
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (!IsEmpty)
            {
                yield return Dequeue();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}
