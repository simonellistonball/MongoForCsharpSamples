using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoSamples
{
    [Serializable]
    class Developer
    {
        [BsonConstructor]
        public Developer(int personId, string firstName, string lastName)
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
        }

        [BsonId]
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [BsonIgnore]
        public Boolean TransientState { get; set; }

        [BsonIgnoreIfNull]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime DateOfBirth { get; set; }

        [BsonDefaultValue("Developer")]
        public string JobTitle { get; set; }

        public bool ShouldSerializeDateOfBirth()
        {
            return DateOfBirth > new DateTime(1900, 1, 1);
        }

        public Dictionary<int, Commit> commits;
    }
}
