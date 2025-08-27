using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.DTOs
{
    public class OwnerDto
    {
        public string IdOwner { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? Photo { get; set; }
        public string Birthday { get; set; } = null!;
    }
}
