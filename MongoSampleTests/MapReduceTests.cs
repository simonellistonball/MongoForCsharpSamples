using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MongoSampleTests
{
    [TestClass]
    public class MapReduceTests : MongoSampleTestBase
    {
        [TestMethod]
        public void TestMapReduce()
        {
            var map =
                "function() {" +
                "    for (var key in this) {" +
                "        emit(key, { count : 1 });" +
                "    }" +
                "}";

            var reduce =
                "function(key, emits) {" +
                "    total = 0;" +
                "    for (var i in emits) {" +
                "        total += emits[i].count;" +
                "    }" +
                "    return { count : total };" +
                "}";

            var mr = collection.MapReduce(map, reduce);

            Assert.IsNotNull(mr.GetResults());

        }
    }
}
