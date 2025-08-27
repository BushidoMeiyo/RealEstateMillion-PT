using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.Entities
{
    public class PropertyTrace
    {
        [BsonId]
        [BsonElement("idPropertyTrace")]
        public string? IdPropertyTrace { get; set; }

        [BsonElement("idProperty")]
        public string? IdProperty { get; set; }

        [BsonElement("dateSale")]
        public string? DateSale { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("value")]
        public decimal Value { get; set; }

        [BsonElement("tax")]
        public decimal Tax { get; set; }
    }
}
