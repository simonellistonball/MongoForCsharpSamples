using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoSamples;
using MongoDB.Driver.Builders;

namespace MongoSampleTests
{

    [TestClass]
    public class MongoUpdateOperations : MongoSampleTestBase
    {

        private Developer GetADeveloper()
        {
            return collection.FindOneAs<Developer>();
        }

        [TestMethod]
        public void TestBasicUpdateOperations()
        {
            Developer developer = GetADeveloper();

            developer.LastName = "Something-Else";
            collection.Save(developer);

            try
            {
                collection.Save(developer, new MongoInsertOptions
                {
                    WriteConcern = WriteConcern.WMajority
                });
            }
            catch (WriteConcernException)
            {
                // the above will throw an exception if we're not using a replicated mongo instance
            }

        }

        [TestMethod]
        public void TestMongoUpdateWithSet()
        {
            // a proper server-side update
            var update = new UpdateDocument {
                { "$set", new BsonDocument("LastName", "A new name") }
            };
            var query = new QueryDocument {
                { "LastName", "Person" }
            };

            collection.Update(query, update);
        }

        
        [TestMethod]
        public void TestMongoMultiUpdate()
        {
            var update = new UpdateDocument {
                { "$set", new BsonDocument("Role", "Senior Developer") }
            };
            var query = new QueryDocument {
                { "Role", "Developer" }
            };

            collection.Update(query, update, new MongoUpdateOptions
            {
                Flags = UpdateFlags.Multi
            });
        }

        [TestMethod]
        public void TestMongoUpsert()
        {
            var update = new UpdateDocument {
                { "$set", new BsonDocument("LastName", "A new name") }
            };
            var query = Query<Developer>.EQ(d => d.PersonId, 10);

            collection.Update(query, update, new MongoUpdateOptions
            {
                Flags = UpdateFlags.Upsert
            });
        }
    }
}
