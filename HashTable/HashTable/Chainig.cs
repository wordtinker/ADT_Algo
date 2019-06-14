using System;
using System.Collections;
using System.Collections.Generic;

namespace HashTable
{
    public class ChainHashTable<TKey, TValue>
        : IEnumerable<(TKey, TValue)>
    {
        // table size and data structure declaration
        public readonly int SIZE = 7;
        private readonly LinkedList<(TKey, TValue)>[] table;
        public ChainHashTable()
        {
            table = new LinkedList<(TKey, TValue)>[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                table[i] = new LinkedList<(TKey, TValue)>();
            }
        }

        // hash function
        private int Code(TKey key) =>
            key.GetHashCode() % SIZE;

        // Add or update data for a given key.
        public void Add(TKey key, TValue data)
        {
            // hash key
            int code = Code(key);
            // check if we have to update
            foreach (var kvp in table[code])
            {
                if (kvp.Item1.Equals(key))
                {
                    table[code].Remove(kvp);
                    break;
                }
            }
            // add new or updated
            table[code].AddLast((key, data));
            // no need of collision resolution policy
            // linked list can grow any times
        }

        public bool Retrieve(TKey key, out TValue found)
        {
            // hash key
            int code = Code(key);
            // find the value
            foreach (var kvp in table[code])
            {
                if (kvp.Item1.Equals(key))
                {
                    found = kvp.Item2;
                    return true;
                }
            }
            found = default;
            return false;
        }

        public IEnumerator<(TKey, TValue)> GetEnumerator()
        {
            foreach (var linkedList in table)
            {
                foreach (var kvp in linkedList)
                {
                    yield return kvp;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}
