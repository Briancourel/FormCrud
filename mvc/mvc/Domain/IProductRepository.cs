using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    interface IProductRepository
    {
        void Add(Product p);
        void Update(Product p);
        void Delete(Product p);
        Product GetById(Guid id);
        IEnumerable<Product> GetAll(string search = null);
        bool ExistsCode(string code, Guid? excludeId = null);

    }
}
