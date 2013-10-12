using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoSamples;

namespace MongoSampleTests
{
    [TestClass]
    public class MongoInsertTests : MongoSampleTestBase
    {
        [TestMethod]
        public void TestBasicInsert()
        {
            var developer = new Developer(3, "Another", "Person", "Developer");
            collection.Insert(developer);

            BsonDocument document = new BsonDocument();
            document.Add(new BsonElement("name", "Another Name"))
                .Add(new BsonElement("number", new BsonInt32(43)));

            documentCollection.Insert(document);
            
            Assert.IsNotNull(document["_id"]);
        }
    }
}
