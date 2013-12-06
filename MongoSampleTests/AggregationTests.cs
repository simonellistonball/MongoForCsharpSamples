using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using MongoSamples;

namespace MongoSampleTests
{
    [TestClass]
    public class AggregationTests : MongoSampleTestBase
    {

        [TestMethod]
        public void TestGroupAndSum()
        {
            // add a different title for variety
            var tester = new Developer(3, "Evil", "Tester", "Tester");
            collection.Insert(tester);

            var group = new BsonDocument 
                { 
                    { "$group", 
                        new BsonDocument 
                            { 
                                { "_id", new BsonDocument 
                                             { 
                                                 { "JobTitle","$JobTitle" }
                                             } 
                                }, 
                                { "Count", new BsonDocument 
                                                 { 
                                                     { "$sum", 1 } 
                                                 } 
                                } 
                            } 
                  } 
                };

            var project = new BsonDocument 
            { 
                { 
                    "$project", 
                    new BsonDocument 
                        { 
                            {"_id", "$_id.JobTitle"}, 
                            {"Count", "$Count"}, 
                        } 
                } 
            };
 
            var pipeline = new[] { group, project }; 
            var result = collection.Aggregate(pipeline);

            var matchingExamples = result.ResultDocuments.ToList();
            Assert.AreEqual(matchingExamples.Count, 3);
        }

        [TestMethod]
        public void TestComplexPipeline()
        {
        }

    }
}
