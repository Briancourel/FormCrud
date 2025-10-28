using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain
{
    class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _data = new List<Product>();

        public void Add(Product p)
        {
            _data.Add(p);
        }

        public void Delete(Product p)
        {
            _data.Remove(p);
        }

        public bool ExistsCode(string code, Guid? excludeId = null)
        {
            return _data.Any(p => p.Code.Equals(code, StringComparison.OrdinalIgnoreCase) && (!excludeId.HasValue || p.id != excludeId.Value) );
        }

        public IEnumerable<Product> GetAll(string search = null)
        {
            var q = _data.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLowerInvariant();
                q = q.Where(p => p.Name.ToLowerInvariant().Contains(s) || p.Code.ToLowerInvariant().Contains(s));

            }
            return q.OrderBy(p => p.Name).ToList();
        }

        public Product GetById(Guid id)
        {
            return _data.FirstOrDefault(p => p.id == id);
        }

        public void Update(Product p)
        {
            var i = _data.FindIndex(x => x.id == p.id);
            _data[i] = p;
        }
    }
}
