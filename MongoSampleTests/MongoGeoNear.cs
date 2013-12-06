using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using System.Linq;
using MongoDB.Driver;

namespace MongoSampleTests
{
    [TestClass]
    public class MongoGeoNear: MongoSampleTestBase
    {
        [TestMethod]
        public void TestGeoNear()
        {
            database = server.GetDatabase("osm");
            var collection = database.GetCollection("map");
            var query = Query.EQ("properties.amenity", new BsonString("pub"));

            // coordinates for the excel centre
            double lon = 51.5060089;
            double lat = 0.0371037;

            var earthRadius = 6378.0; // km
            var rangeInKm = 3000.0; // km

            var options = GeoNearOptions
                    .SetMaxDistance(rangeInKm / earthRadius /* to radians */)
                    .SetSpherical(true);

            var results = collection.GeoNear(query, lat, lon, 10, options);

            Assert.AreEqual(10, results.Hits.Count);

        }

        protected override void PopulateDatabase() { }
    }
}
