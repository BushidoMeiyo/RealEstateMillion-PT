using MongoDB.Bson.Serialization.Attributes;

namespace RealEstate.Domain.Entities;

public class Property
{
    [BsonId]
    [BsonElement("idProperty")]
    public string IdProperty { get; set; } = null!;

    [BsonElement("name")]
    public string Name { get; set; } = null!;

    [BsonElement("address")]
    public string Address { get; set; } = null!;

    [BsonElement("price")]
    public decimal Price { get; set; }

    [BsonElement("codeInternal")]
    public string CodeInternal { get; set; } = null!;

    [BsonElement("year")]
    public int Year { get; set; }

    [BsonElement("idOwner")]
    public string IdOwner { get; set; } = null!;

    [BsonIgnore]
    public string Image { get; set; } = string.Empty;
}