using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            _view.EditRequested += (s, e) => StartEdit();
            _view.DeleteRequested += (s, e) => StartDelete();
            _view.SaveRequested += (s, e) => Save();
            _view.CancelRequested += (s, e) => Cancel();
            _view.SearchRequested += (s, e) => Refresh();
            
            Seed();
            Refresh();
        }

        public void StartAdd()
        {
            _view.LoadEditor(null);
        }

        public void StartEdit()
        {
            var id = _view.SelectedId();
            if (id == Guid.Empty)
            {
                _view.Error("Por favor seleccione un producto para editar");
                return;
            }

            var product = _repo.GetById(id);
            if (product != null)
            {
                _view.LoadEditor(product);
            }
            else
            {
                _view.Error("Producto no encontrado");
            }
        }

        public void StartDelete()
        {
            var id = _view.SelectedId();
            if (id == Guid.Empty)
            {
                _view.Error("Por favor seleccione un producto para eliminar");
                return;
            }

            var product = _repo.GetById(id);
            if (product != null)
            {
                var result = MessageBox.Show(
                    $"¿Está seguro de eliminar el producto '{product.Name}'?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _repo.Delete(product);
                    _view.Info("Producto eliminado correctamente");
                    Refresh();
                }
            }
            else
            {
                _view.Error("Producto no encontrado");
            }
        }

        public void Save()
        {
            var product = _view.ReadEditor();
            
            // Generar código automático si está vacío
            if (string.IsNullOrWhiteSpace(product.Code))
            {
                var count = _repo.GetAll().Count() + 1;
                product.Code = $"AUTO{count:D3}";
            }
            
            var validationError = Validate(product);

            if (!string.IsNullOrEmpty(validationError))
            {
                _view.Error(validationError);
                return;
            }

            var existingProduct = _repo.GetById(product.Id);
            if (existingProduct == null)
            {
                // Nuevo producto
                _repo.Add(product);
                _view.Info("Producto agregado correctamente");
            }
            else
            {
                // Actualizar producto existente
                _repo.Update(product);
                _view.Info("Producto actualizado correctamente");
            }

            Refresh();
            _view.LoadEditor(null);
        }

        public void Cancel()
        {
            _view.LoadEditor(null);
        }

        private void Refresh()
        {
            var list = _repo.GetAll(_view.searchText());
            _view.BindProducts(list);
        }
        private string Validate(Product p)
        {
            if (string.IsNullOrWhiteSpace(p.Name))
                return "El nombre es obligatorio";
            if (string.IsNullOrWhiteSpace(p.Code))
                return "El codigo es obligatorio";
            if (_repo.ExistsCode(p.Code, p.Id))
                return "El código ya existe para otro producto";
            if (p.Price < 0)
                return "El precio no puede ser negativo";

            return "";
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
