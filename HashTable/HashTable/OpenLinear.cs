using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HashTable
{

    public class KVP<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }

    public class EmptyKVP<TKey, TValue> : KVP<TKey, TValue>
    {
        public EmptyKVP()
        {
            Key = default;
            Value = default;
        }
    }

    public class OpenLinearHashTable<TKey, TValue>
        : IEnumerable<KVP<TKey, TValue>>
    {
        // table size and data structure declaration
        public readonly int SIZE = 100;
        private readonly KVP<TKey, TValue>[] table;
        private readonly int stride = 1;
        public OpenLinearHashTable()
        {
            table = new KVP<TKey, TValue>[SIZE];
        }

        // hash function
        private int Code(TKey key) =>
            key.GetHashCode() % SIZE;

        // probe sequence generator
        // Linear probing uses stide to shift position to the right
        // Leads to clustering effect and O(N) time access.
        //
        // Quadratic probing - the algorithm adds the square of
        // the number of locations it has tried to create the
        // probe sequence. Quadratic probing reduces primary
        // clustering, but it can suffer from secondary
        // clustering.
        //
        // Pseudorandom probing is similar to linear probing,
        // except that the stride is given by a pseudorandom
        // function of the initially mapped location.
        // Prevents primary clustering.
        // may also skip over some unused entries and fail to
        // insert an item even though the table isn’t completely full.
        //
        // Double hashing is similar to pseudorandom probing. Instead
        // of using a pseudorandom function of the initial location
        // to create a stride value, it uses a
        // second hashing function to map the original value to a
        // stride.
        // Double hashing eliminates primary and secondary clustering. However, like
        // pseudorandom probing, double hashing may skip some unused
        // entries and fail to insert an item.
        private IEnumerable<int> ProbeSeq(TKey key)
        {
            // hash key
            int code = Code(key);
            int seen = 0; // keep track of array size
            // if we've seen every value, no poit digging further
            while (true && seen < SIZE)
            {
                if (table[code] == null)
                {
                    yield return code;
                    break;
                }
                else
                {
                    yield return code;
                    code += stride;
                    code %= SIZE; // wrap around array table
                    seen++;
                }    
            }
        }

        // Add or update data for a given key.
        // O(1)
        // O(N) worst case, if fill % is big
        public void Add(TKey key, TValue data)
        {
            foreach (var pos in ProbeSeq(key))
            {
                var kvp = table[pos];
                if (kvp is EmptyKVP<TKey,TValue> ||
                    kvp == null ||
                    kvp.Key.Equals(key))
                {
                    // Add new kvp
                    table[pos] = new KVP<TKey, TValue>()
                    {
                        Key = key,
                        Value = data
                    };
                    break;
                }
            }
        }


        // O(1)
        // O(N) worst case, if fill % is big
        // or a lot of EmptyKVP
        public bool Retrieve(TKey key, out TValue found)
        {
            foreach (var pos in ProbeSeq(key))
            {
                var kvp = table[pos];
                if (!(kvp is EmptyKVP<TKey, TValue>) &&
                    kvp != null &&
                    kvp.Key.Equals(key))
                {
                    found = kvp.Value;
                    return true;
                }
            }
            found = default;
            return false;
        }

        public void Remove(TKey key)
        {
            foreach (var pos in ProbeSeq(key))
            {
                var kvp = table[pos];
                if (!(kvp is EmptyKVP<TKey, TValue>) && 
                    kvp != null &&
                    kvp.Key.Equals(key))
                {
                    //table[pos] = null;
                    //can't set to null
                    //it can break probe seqof another hash
                    table[pos] = new EmptyKVP<TKey, TValue>();
                }
            }
        }

        public IEnumerator<KVP<TKey, TValue>> GetEnumerator()
        {
            foreach (var kvp in table)
            {
                if (kvp != null)
                {
                    yield return kvp;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}
