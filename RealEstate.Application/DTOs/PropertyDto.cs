using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.DTOs
{
    public class PropertyDto
    {
        public string IdProperty { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
        public string? CodeInternal { get; set; }
        public string IdOwner { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}
