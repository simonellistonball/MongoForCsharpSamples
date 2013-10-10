using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoSamples
{
    [Serializable]
    class Commit : ISupportInitialize
    {
        public string CommitId { get; set; }
        
        public string NewField { get; set; }

        [BsonExtraElements]
        public IDictionary<string, object> ExtraElements { get; set; }

        void ISupportInitialize.BeginInit()
        {
            // nothing to do at begin
        }

        void ISupportInitialize.EndInit()
        {
            object oldFieldValue;
            if (!ExtraElements.TryGetValue("OldField", out oldFieldValue))
            {
                return;
            }
            var oldField = (string)oldFieldValue;

            // remove the OldField element so that it doesn't get persisted back to the database
            ExtraElements.Remove("OldField");

            // new field is some function of old field
            NewField = TransformOldFieldToNew(oldField);
        }

        private string TransformOldFieldToNew(string oldField)
        {
            return oldField.ToLower();
        }
    }
}
