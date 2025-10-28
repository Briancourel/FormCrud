using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class Product
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool Active { get; set; }

    }
}
