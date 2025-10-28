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
        public ProductForm()
        {
            InitializeComponent();
            btnCrear.Click += (s, e) => AddRequested?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler AddRequested;
        public event EventHandler EditResquested;
        public event EventHandler DeleteResquested;
        public event EventHandler SaveResquested;
        public event EventHandler CancelResquested;
        public event EventHandler SearchResquested;

       

       

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public Guid SelectedId()
        {
            throw new NotImplementedException();
        }

        public void Info(string msg)
        {
            throw new NotImplementedException();
        }

        public void Error(string msg)
        {
            throw new NotImplementedException();
        }
/*
        public void BindProducts(IEnumerable<Product> products)
        {
            throw new NotImplementedException();
        }

        public void LoadEditor(Product product)
        {
            throw new NotImplementedException();
        }

        public Product ReadEditor()
        {
            throw new NotImplementedException();
        }

        public string searchText()
        {
            throw new NotImplementedException();
        }
*/

        private void ProductForm_Load(object sender, EventArgs e)
        {

        }
    }
}
