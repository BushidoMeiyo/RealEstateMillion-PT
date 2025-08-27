using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.Entities
{
    public class Owner
    {
        [BsonId]
        [BsonElement("idOwner")]
        public string IdOwner { get; set; } = null!;

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("address")]
        public string Address { get; set; } = null!;

        [BsonElement("photo")]
        public string? Photo { get; set; }

        [BsonElement("birthday")]
        public string Birthday { get; set; } = null!;
    }
}
