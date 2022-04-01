using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Configuration; 

namespace CRUD_LINQ
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DataClasses1DataContext dataContext; 

        public MainWindow()
        {
            InitializeComponent();

            //establezco la coexión a la base de datos
            string miConexion = ConfigurationManager.ConnectionStrings["CRUD_LINQ.Properties.Settings.CrudLinqSql"].ConnectionString;

            dataContext = new DataClasses1DataContext(miConexion);

            //llamo al método InsertarEmpresas para ejecutar
            InsertaEmpresas();

        }


        public void InsertaEmpresas()
        {
            //creo la instancia
            Empresa pildorasInformaticas = new Empresa();

            //establezco la propiedad del objeto
            pildorasInformaticas.Nombre = "PildorasInformáticas";

            //usando el mapeo dataContext, que en la tabla Empresa inserte el objeto
            dataContext.Empresa.InsertOnSubmit(pildorasInformaticas);

            //dataContext toma efecto, actualiza los cambios
            dataContext.SubmitChanges(); 

            //muestro el registro en el dataGrid de la aplicación gráfica
            Principal.ItemsSource = dataContext.Empresa;

        }

    }
}
