using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Views;
using Domain;


namespace Controller
{
    class ProductController
    {
        private readonly IProductView _view;
        private readonly IProductRepository _repo;

        public ProductController (IProductRepository repo, IProductView view)
        {
            _view = view;
            _repo = repo;
        }
        public void Init()
        {
            _view.AddRequested += (s, e) => StartAdd();
        }
        public void StartAdd()
        {

        }
        private void Refresh()
        {
            var list = _repo.GetAll(_view.searchText());
            _view.BindProducts(list);
        }
        private string Validate(Product p)
        {
            string msg = "";
            if (string.IsNullOrWhiteSpace(p.Name))
                msg = "El nombre es obligatorio";
            if (string.IsNullOrWhiteSpace(p.Code))
                msg = "El codigo es obligatorio";
            if (_repo.ExistsCode(p.Code, p.id))
                msg = "El producto ya existe";
            if (p.Price < 0)
                msg = "EL precio debe ser mayor a 0";
            if (p.Price > 0)
                msg = "El precio no puede ser menor a 0";

            return msg;
        }
        private void Seed()
        {
            _repo.Add(new Product 
            {   
                Code = "A001",
                Active = true,
                Price = 1000,
                Name = "Teclado Gamer",
                Stock = 120 
            });

            _repo.Add(new Product
            {
                Code = "A002",
                Active = true,
                Price = 500,
                Name = "Mouse Gamer",
                Stock = 80
            });
        }
    }
}
