using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;

namespace MongoSampleTests
{
    [TestClass]
    public class MongoDeleteTests : MongoSampleTestBase
    {
        [TestMethod]
        public void TestDelete()
        {
            var query = new QueryDocument {
                { "LastName", "Person" }
            };
            collection.Remove(query);
        }

        [TestMethod]
        public void TestDeleteOptions()
        {
            var query = new QueryDocument {
                { "LastName", "Person" }
            };
            collection.Remove(query, RemoveFlags.Single, WriteConcern.Unacknowledged);
        }

        [TestMethod]
        public void TestRemoveAll()
        {
            collection.RemoveAll();
        }
    }
}
