using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.DTOs
{
    public class PropertyTraceDto
    {
        public string? IdPropertyTrace { get; set; }
        public string? IdProperty { get; set; }
        public string? DateSale { get; set; }
        public string? Name { get; set; }
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
    }
}
