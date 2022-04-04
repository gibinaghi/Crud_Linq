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
            //InsertaEmpleados();
            //InsertaCargos();
            //InsertaEmpleadoCargo();
            //ActualizaEmpleado();
            EliminaEmpleado();

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


        public void InsertaCargos()
        {
            //dataContext.ExecuteCommand("delete from Cargo");

            //inserto los cargos
            dataContext.Cargo.InsertOnSubmit(new Cargo { NombreCargo = "Director/a" });
            dataContext.Cargo.InsertOnSubmit(new Cargo { NombreCargo = "Administrativo/a" });

            dataContext.SubmitChanges();

            Principal.ItemsSource = dataContext.Cargo;
        }


        public void InsertaEmpleadoCargo()
        {
            /*// forma 1
            Empleado Juan = dataContext.Empleado.First(em => em.Nombre.Equals("Juan"));
            Empleado Ana = dataContext.Empleado.First(em => em.Nombre.Equals("Ana"));

            Cargo cDirector = dataContext.Cargo.First(cg => cg.NombreCargo.Equals("Director/a"));
            Cargo cAdtvo = dataContext.Cargo.First(cg => cg.NombreCargo.Equals("Administrativo/a"));

            CargoEmpleado cargoJuan = new CargoEmpleado();

            //relaciono las tabla Empleado y Cargo
            cargoJuan.Empleado = Juan;
            cargoJuan.CargoId = cDirector.Id;

            cargoJuan.Empleado = Ana;
            cargoJuan.CargoId = cAdtvo.Id;*/


            /* //forma 2
             //insertar Empleados y Cragos a una lista sabiendo los id de empleados y cargos
             List<CargoEmpleado> listaCargosEmpleados = new List<CargoEmpleado>();
             listaCargosEmpleados.Add(new CargoEmpleado { CargoId = 5, EmpleadoId = 1 });
             listaCargosEmpleados.Add(new CargoEmpleado { CargoId = 6, EmpleadoId = 2 });
             listaCargosEmpleados.Add(new CargoEmpleado { CargoId = 5, EmpleadoId = 3 });

             dataContext.CargoEmpleado.DeleteAllOnSubmit(listaCargosEmpleados);*/

            //forma 3
            Empleado Juan = dataContext.Empleado.First(em => em.Nombre.Equals("Juan"));
            Empleado Ana = dataContext.Empleado.First(em => em.Nombre.Equals("Ana"));
            Empleado Gimena = dataContext.Empleado.First(em => em.Nombre.Equals("Gimena"));
            Empleado Pablo = dataContext.Empleado.First(em => em.Nombre.Equals("Pablo"));

            Cargo cDirector = dataContext.Cargo.First(cg => cg.NombreCargo.Equals("Director/a"));
            Cargo cAdtvo = dataContext.Cargo.First(cg => cg.NombreCargo.Equals("Administrativo/a"));

            List<CargoEmpleado> listaCargosEmpleados = new List<CargoEmpleado>();
            listaCargosEmpleados.Add(new CargoEmpleado { Empleado = Juan, Cargo = cDirector });
            listaCargosEmpleados.Add(new CargoEmpleado { Empleado=Ana, Cargo=cDirector });
            listaCargosEmpleados.Add(new CargoEmpleado { Empleado = Gimena, Cargo = cAdtvo });
            listaCargosEmpleados.Add(new CargoEmpleado { Empleado = Pablo, Cargo = cDirector });

            dataContext.SubmitChanges();

            Principal.ItemsSource = dataContext.CargoEmpleado;
        }


        public void ActualizaEmpleado()
        {

            Empleado Juan = dataContext.Empleado.First(em => em.Nombre.Equals("Juan"));
            Juan.Nombre = "Juan Pedro";

            dataContext.SubmitChanges();

            Principal.ItemsSource = dataContext.Empleado;
        }


        public void EliminaEmpleado()
        {

            Empleado Gimena = dataContext.Empleado.First(em => em.Nombre.Equals("Gimena"));

            dataContext.Empleado.DeleteOnSubmit(Gimena);

            dataContext.SubmitChanges();

            Principal.ItemsSource = dataContext.Empleado;
        }

    
    }
}
