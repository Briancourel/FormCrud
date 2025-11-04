using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Views
{
    public partial class ProductForm : Form, IProductView
    {
        private Guid? _currentProductId = null;

        public ProductForm()
        {
            InitializeComponent();
            InitializeListView();
            SetupEvents();
        }

        private void InitializeListView()
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Columns.Add("Código", 100);
            listView1.Columns.Add("Nombre", 200);
            listView1.Columns.Add("Precio", 100);
            listView1.Columns.Add("Stock", 80);
            listView1.Columns.Add("Activo", 80);
            listView1.Columns.Add("Id", 0); // Oculto pero presente
        }

        private void SetupEvents()
        {
            btnCrear.Click += (s, e) => AddRequested?.Invoke(this, EventArgs.Empty);
            btnGuardar.Click += (s, e) => SaveRequested?.Invoke(this, EventArgs.Empty);
            btnBorrar.Click += (s, e) => DeleteRequested?.Invoke(this, EventArgs.Empty);
            btnCancelar.Click += (s, e) => CancelRequested?.Invoke(this, EventArgs.Empty);
            listView1.DoubleClick += (s, e) => EditRequested?.Invoke(this, EventArgs.Empty);
            textBox5.TextChanged += (s, e) => SearchRequested?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler AddRequested;
        public event EventHandler EditRequested;
        public event EventHandler DeleteRequested;
        public event EventHandler SaveRequested;
        public event EventHandler CancelRequested;
        public event EventHandler SearchRequested;

        public Guid SelectedId()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var idString = listView1.SelectedItems[0].SubItems[5].Text;
                if (Guid.TryParse(idString, out Guid id))
                    return id;
            }
            return Guid.Empty;
        }

        public void Info(string msg)
        {
            MessageBox.Show(msg, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Error(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void BindProducts(IEnumerable<Product> products)
        {
            listView1.Items.Clear();
            foreach (var product in products)
            {
                var item = new ListViewItem(new[]
                {
                    product.Code ?? "",
                    product.Name ?? "",
                    product.Price.ToString("C"),
                    product.Stock.ToString(),
                    product.Active ? "Sí" : "No",
                    product.Id.ToString()
                });
                listView1.Items.Add(item);
            }
        }

        public void LoadEditor(Product product)
        {
            if (product == null)
            {
                ClearEditor();
                return;
            }

            _currentProductId = product.Id;
            textBox1.Text = product.Name ?? "";
            textBox3.Text = product.Code ?? "";
            textBox6.Text = product.Price.ToString("F2");
            textBox4.Text = product.Stock.ToString();
            checkBox1.Checked = product.Active;
            textBox7.Text = product.Id.ToString();
        }

        public Product ReadEditor()
        {
            Product product;
            
            if (_currentProductId.HasValue)
            {
                // Producto existente - mantener el Id original
                product = new Product { Id = _currentProductId.Value };
            }
            else
            {
                // Nuevo producto - dejar que se genere el Id automáticamente
                product = new Product();
            }

            product.Name = textBox1.Text?.Trim();
            product.Code = textBox3.Text?.Trim();
            
            if (decimal.TryParse(textBox6.Text, out decimal price))
                product.Price = price;
            else
                product.Price = 0;
            
            if (int.TryParse(textBox4.Text, out int stock))
                product.Stock = stock;
            else
                product.Stock = 0;
            
            product.Active = checkBox1.Checked;

            return product;
        }

        public string searchText()
        {
            return textBox5.Text?.Trim() ?? "";
        }

        private void ClearEditor()
        {
            _currentProductId = null;
            textBox1.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox6.Clear();
            textBox7.Clear();
            checkBox1.Checked = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Este método parece estar conectado al botón Crear pero ya está manejado en SetupEvents
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Handler vacío para el evento Click del label
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            // Este evento puede ser usado para inicialización adicional
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
