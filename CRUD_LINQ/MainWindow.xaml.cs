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

            //llamo al método para ejecutar
            //InsertaEmpresas();
            InsertaEmpleados();

        }


        public void InsertaEmpresas()
        {

            //borro todas los datos de la tabla empresa
            //dataContext.ExecuteCommand("delete from Empresa");

            //creo la instancia de la Empresa 1
            Empresa pildorasInformaticas = new Empresa();

            //establezco la propiedad del objeto
            pildorasInformaticas.Nombre = "Pildoras Informáticas";

            //usando el mapeo dataContext, que en la tabla Empresa inserte el objeto
            dataContext.Empresa.InsertOnSubmit(pildorasInformaticas);

            //Agrego Empresa 2
            Empresa empresaGoogle = new Empresa();
            empresaGoogle.Nombre = "Google";
            dataContext.Empresa.InsertOnSubmit(empresaGoogle);


            //dataContext toma efecto, actualiza los cambios
            dataContext.SubmitChanges(); 

            //muestro el registro en el dataGrid de la aplicación gráfica
            Principal.ItemsSource = dataContext.Empresa;

        }


        public void InsertaEmpleados()
        {

            //creo objeto de tipo Empresa xq las tablas están relacionadas con el IdEmpresa
            Empresa pildorasInformaticas = dataContext.Empresa.First(em => em.Nombre.Equals("Pildoras Informáticas"));
            Empresa empresaGoogle = dataContext.Empresa.First(em => em.Nombre.Equals("Google"));

            //creo lista de empleados
            List<Empleado> listaEmpleados = new List<Empleado>();

            //agrego empleados a la lista
            listaEmpleados.Add(new Empleado { Nombre="Juan", Apellido="Diaz", EmpresaId=pildorasInformaticas.Id });
            listaEmpleados.Add(new Empleado { Nombre = "Ana", Apellido = "Martin", EmpresaId = empresaGoogle.Id });
            listaEmpleados.Add(new Empleado { Nombre = "Gimena", Apellido = "Binaghi", EmpresaId = empresaGoogle.Id });
            listaEmpleados.Add(new Empleado { Nombre = "Pablo", Apellido = "Broggi", EmpresaId = pildorasInformaticas.Id });

            //inserto los empleados 
            dataContext.Empleado.InsertAllOnSubmit(listaEmpleados);

            dataContext.SubmitChanges();

            Principal.ItemsSource = dataContext.Empleado;

        }

    }
}
