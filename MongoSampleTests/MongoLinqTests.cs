using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using MongoDB.Driver.Linq;

namespace MongoSampleTests
{
    [TestClass]
    public class MongoLinqTests: MongoSampleTestBase
    {
        [TestMethod]
        public void TestALinqQuery()
        {
            var query =
                from e in collection.AsQueryable()
                where e.LastName == "Person"
                select e;

            foreach (var developer in query){
                Assert.IsNotNull(developer);
            }
        }
    }
}
