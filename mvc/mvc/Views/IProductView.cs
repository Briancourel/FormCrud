using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Views
{
    interface IProductView
    {
        //Eventos 

        event EventHandler AddRequested;
        event EventHandler EditResquested;
        event EventHandler DeleteResquested;
        event EventHandler SaveResquested;
        event EventHandler CancelResquested;
        event EventHandler SearchResquested;

        //FeedBack
        Guid SelectedId();
        void Info(string msg);
        void Error(string msg);

        // Datos
        void BindProducts(IEnumerable<Product> products);
        void LoadEditor(Product product);
        Product ReadEditor();
        string searchText();



    }
}
