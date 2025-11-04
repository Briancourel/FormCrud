using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Views;
using Controller;
using Domain;

namespace mvc
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Configurar el patrón MVC
            var repository = new InMemoryProductRepository();
            var view = new ProductForm();
            var controller = new ProductController(repository, view);
            
            // Inicializar el controlador (conecta eventos y carga datos)
            controller.Init();
            
            Application.Run(view);
        }
    }
}
