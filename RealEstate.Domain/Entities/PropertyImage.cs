using MongoDB.Bson.Serialization.Attributes;

namespace RealEstate.Domain.Entities
{
    public class PropertyImage
    {
        [BsonId]
        [BsonElement("idPropertyImage")]
        public string IdPropertyImage { get; set; } = null!;

        [BsonElement("idProperty")]
        public string IdProperty { get; set; } = null!;

        [BsonElement("file")]
        public string File { get; set; } = null!;

        [BsonElement("enabled")]
        public bool Enabled { get; set; }
    }

}
