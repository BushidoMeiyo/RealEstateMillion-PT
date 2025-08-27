using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Settings
{
    public class MongoOptions
    {
        public string ConnectionString { get; set; } = default!;
        public string Database { get; set; } = default!;
    }
}
