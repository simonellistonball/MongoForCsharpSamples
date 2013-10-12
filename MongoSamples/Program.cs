using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MongoSamples
{
    class Program
    {
        private const string connectionString = "mongodb://localhost";

        static void Main(string[] args)
        {
            var client = new MongoClient(connectionString);
            var server = client.GetServer(); 
            var database = server.GetDatabase("osm");
            var collection = database.GetCollection("map");
            
            var query = Query.EQ("properties.amenity", new BsonString("pub"));
            
            double lon = 54.9117468;
            double lat = -1.3737675;

            var earthRadius = 6378.0; // km
            var rangeInKm = 3000.0; // km

            var options = GeoNearOptions
                    .SetMaxDistance(rangeInKm / earthRadius /* to radians */)
                    .SetSpherical(true);

            var results = collection.GeoNear(query, lat, lon, 10, options);

            foreach (var result in results.Hits)
            {
                Console.WriteLine(result.Document["properties"]["name"]);
            }
            Console.ReadKey();

        }
    }
}
