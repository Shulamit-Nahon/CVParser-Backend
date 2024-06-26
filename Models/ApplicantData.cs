using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CVParserAPI.Models
{
    public class ApplicantData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string PersonalId { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string LinkedInUrl { get; set; }

        public string RawData { get; set; }
        public string OriginalFileName { get; set; }

    }
}