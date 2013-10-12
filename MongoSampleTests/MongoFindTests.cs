using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoSamples;
using System.Linq;
using System.Collections.Generic;

namespace MongoSampleTests
{
    [TestClass]
    public class MongoFindTests: MongoSampleTestBase
    {
        [TestMethod]
        public void TestBasicFindAlls()
        {
            // read back
            MongoCursor<BsonDocument> documentResults = documentCollection.FindAll();
            MongoCursor<Developer> developerResults = collection.FindAllAs<Developer>();
            // MongoCursor is an IEnumarable. The query is only actually run when we enumerate
            try
            {
                // note that this requires the extension methods in System.Linq
                List<Developer> allDevelopers = developerResults.ToList<Developer>();
            }
            catch (Exception e)
            {
            }
        }

        [TestMethod]
        public void TestCursorModification()
        {
            var cursor = collection.FindAll();
            cursor.Skip = 100;
            cursor.Limit = 10;

            foreach (var developer in cursor) {
                Console.WriteLine(developer.LastName);
            }
        }

        [TestMethod]
        public void TestBasicFindOne()
        {
            var documentId = 1;

            BsonDocument documentRead = documentCollection.FindOne(new QueryDocument {
                { "_id", documentId }
            });

            var readQuery = Query<Developer>.EQ(n => n.PersonId, 2);
            Developer developerRead = collection.FindOne(readQuery);
        }
    }
}
