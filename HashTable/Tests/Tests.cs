using HashTable;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Add_ChainHashTable_AddedOneItem()
        {
            var hashTable = new ChainHashTable<int, int>();

            hashTable.Add(1, 1);
            var val = hashTable.ToArray();

            Assert.AreEqual((1, (1,1)), (val.Length, val[0]));
        }

        [Test]
        public void Update_ChainHashTable_UpdatedOneItem()
        {
            var hashTable = new ChainHashTable<int, int>();
            hashTable.Add(1, 1);

            hashTable.Add(1, 15);
            var val = hashTable.ToArray();

            Assert.AreEqual((1, (1, 15)), (val.Length, val[0]));
        }

        [Test]
        public void Retrieve_ChainHashTable_FoundProperValue()
        {
            var hashTable = new ChainHashTable<int, int>();
            hashTable.Add(1, 1);
            hashTable.Add(2, 15);
            hashTable.Add(3, 13);
            hashTable.Add(4, 10);

            var result = hashTable.Retrieve(3, out int found);

            Assert.AreEqual((result, found), (true, 13));
        }

        [Test]
        public void Add_OpenLinearHashTable_AddedOneItem()
        {
            var hashTable = new OpenLinearHashTable<int, int>();

            hashTable.Add(1, 1);
            var val = hashTable.ToArray();

            Assert.AreEqual((1, 1, 1), (val.Length, val[0].Key, val[0].Value));
        }

        [Test]
        public void Update_OpenLinearHashTable_UpdatedOneItem()
        {
            var hashTable = new OpenLinearHashTable<int, int>();
            hashTable.Add(1, 1);

            hashTable.Add(1, 15);
            var val = hashTable.ToArray();

            Assert.AreEqual((1, 1, 15), (val.Length, val[0].Key, val[0].Value));
        }

        [Test]
        public void Retrieve_OpenLinearHashTable_FoundProperValue()
        {
            var hashTable = new OpenLinearHashTable<int, int>();
            hashTable.Add(1, 1);
            hashTable.Add(2, 15);
            hashTable.Add(3, 13);
            hashTable.Add(4, 10);

            var result = hashTable.Retrieve(3, out int found);

            Assert.AreEqual((result, found), (true, 13));
        }

        [Test]
        public void Remove_OpenLinearHashTable_FoundProperValue()
        {
            var hashTable = new OpenLinearHashTable<int, int>();
            hashTable.Add(1, 1);
            hashTable.Add(2, 15);
            hashTable.Add(3, 13);
            hashTable.Add(4, 10);

            hashTable.Remove(3);
            var result = hashTable.Retrieve(3, out int found);

            Assert.AreEqual((result, found), (false, 0));
        }
    }
}