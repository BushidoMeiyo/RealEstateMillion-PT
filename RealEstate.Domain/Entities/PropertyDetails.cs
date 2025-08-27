using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.Entities
{
    public class PropertyDetails
    {
        public required Property Property { get; set; }
        public required Owner Owner { get; set; }
        public required List<PropertyImage> Images { get; set; }
        public required List<PropertyTrace> Traces { get; set; }
    }
}
