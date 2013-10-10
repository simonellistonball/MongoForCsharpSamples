using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase("test");

            // two choices of collection type:
            // One based on generic BsonDocumnets
            var documentCollection = database.GetCollection("team");
            // And another tied to our domain model
            var developerCollection = database.GetCollection<Developer>("team");

            // create some entries 
            var Developer = new Developer(1,"Test", "Person");
            developerCollection.Insert(Developer);
            var Developer2 = new Developer(2,"Another", "Developer");
            developerCollection.Insert(Developer2);

            BsonDocument document = new BsonDocument();
            document.Add(new BsonElement("name", "Testing"))
                .Add(new BsonElement("number", new BsonInt32(42)));

            documentCollection.Insert(document);

            // read back
            MongoCursor<BsonDocument> documentResults = documentCollection.FindAll();
            MongoCursor<Developer> developerResults = developerCollection.FindAll();

            // MongoCursor is an IEnumarable. The query is only actually run when we enumerate
            List<Developer> allDevelopers = developerResults.ToList<Developer>();

            // update



            // upsert

            // delete 



            
        }
    }
}
