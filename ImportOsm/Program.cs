using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ImportOsm
{
    /// <summary>
    /// A very quick program to import OSM data into a mongo collection, node by node
    /// </summary>
    class Program
    {
        private const string connectionString = "mongodb://localhost";
        private const string databaseName = "test";
        private const string collectionName = "osm";

        static void Main(string[] args)
        {
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(databaseName);
            var documentCollection = database.GetCollection(collectionName);


            XmlDocument xml = new XmlDocument();
            xml.Load("map.osm");

            foreach (XmlNode node in xml.GetElementsByTagName("node"))
            {
                BsonDocument doc = new BsonDocument();
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    BsonValue value;
                    switch (attribute.Name) {
                        case "lat":
                            goto case "lon";
                        case "lon":
                            value = new BsonDouble(Convert.ToDouble(attribute.Value));
                            break;
                        default:
                            value = new BsonString(attribute.Value);
                            break;
                    }
                    doc.Add(attribute.Name, value);
                }

                BsonArray tags = new BsonArray();
                foreach (XmlNode tag in node.ChildNodes)
                {
                    var k = tag.Attributes["k"].Value;
                    var v = tag.Attributes["v"].Value;
                    var bv = new BsonString(v);
                    var element = new BsonElement(k, bv);

                    tags.Add(new BsonDocument(element));
                }

                doc.Add("tags", tags);

                documentCollection.Insert(doc);
            }
        }
    }
}
