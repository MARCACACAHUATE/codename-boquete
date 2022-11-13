using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Data.SqlClient;
using codename_boquete.Model;
using System.Data;

namespace codename_boquete.ViewModel
{
    public class RegistroViewModel : ViewModelBase
    {
        // Fields
        private List<CsrfReporteDeFuga> _ListaReporteDeFugas;
        private DateTime _fechaInicio = DateTime.Today;
        private DateTime _fechaTermino = DateTime.Today;
        private string _linea = "";
        private string _turno = "";
        private string _coil = "";
        private string _soldadura = "";
        private List<string> _listLinea = new List<string>() { "1", "2", "3" };
        private List<string> _listTurno = new List<string>() { "1", "2", "3" };
        private List<string> _listCoil = new List<string>() { "A coil 3x24 Cyclone" };
        private List<string> _listSoldadura = new List<string>() { "AutoBrazer" };


        // Propiedades
        public List<CsrfReporteDeFuga> ListaReporteDeFugas
        {
            get
            {
                return _ListaReporteDeFugas;
            }
            set
            {
                _ListaReporteDeFugas = value;
                onPropertyChanged(nameof(ListaReporteDeFugas));
            }
        }
        public DateTime FechaInicio
        {
            get => _fechaInicio;
            set
            {
                _fechaInicio = value;
                onPropertyChanged(nameof(FechaInicio));
            }
        }
        public DateTime FechaTermino
        {
            get => _fechaTermino;
            set
            {
                _fechaTermino = value;
                onPropertyChanged(nameof(FechaTermino));
            }
        }
        public string Linea
        {
            get => _linea;
            set
            {
                _linea = value;
                onPropertyChanged(nameof(Linea));
            }
        }
        public string Turno
        {
            get => _turno;
            set
            {
                _turno = value;
                onPropertyChanged(nameof(Turno));
            }
        }
        public string Coil
        {
            get => _coil;
            set
            {
                _coil = value;
                onPropertyChanged(nameof(Coil));
            }
        }
        public string Soldadura
        {
            get => _soldadura;
            set
            {
                _soldadura = value;
                onPropertyChanged(nameof(Soldadura));
            }
        }
        public List<string> ListLinea
        {
            get => _listLinea;
            set
            {
                _listLinea = value;
                onPropertyChanged(nameof(ListLinea));
            }
        }
        public List<string> ListTurno
        {
            get => _listTurno;
            set
            {
                _listTurno = value;
                onPropertyChanged(nameof(ListTurno));
            }
        }
        public List<string> ListCoil
        {
            get => _listCoil;
            set
            {
                _listCoil = value;
                onPropertyChanged(nameof(ListCoil));
            }
        }
        public List<string> ListSoldadura
        {
            get => _listSoldadura;
            set
            {
                _listSoldadura = value;
                onPropertyChanged(nameof(ListSoldadura));
            }

        }



        // Comandos
        public ICommand BusquedaConSPViewCommand { get; }

        // Constructor
        public RegistroViewModel()
        {
            BusquedaConSPViewCommand = new ViewModelCommand(ExecuteBusquedaConSPViewCommand);
            ExecuteListaRegistroViewCommand(null);
        }

        // Metodos
        private void ExecuteListaRegistroViewCommand(object obj)
        {
            using (Model.DESProyectoContext db = new Model.DESProyectoContext())
            {
                List<CsrfReporteDeFuga> lista = (from d in db.CsrfReporteDeFugas
                                                 select d).ToList();
                ListaReporteDeFugas = lista;
            }
        }

        public void ExecuteBusquedaConSPViewCommand(object obj)
        {
            SqlConnection sqlConn = null;
            SqlDataReader sqlDr = null;
            ListaReporteDeFugas = new List<CsrfReporteDeFuga>();

            try
            {
                // Abre la conexion a la DB
                sqlConn = new SqlConnection("Server=127.0.0.1;Database=DESProyecto;User=SA;Password=arribaLasChivas10;MultipleActiveResultSets=true;");
                sqlConn.Open();

                // Crea un objeto commando con parametros para el sp
                SqlCommand sqlCmd = new SqlCommand("dbo.spOut_CSRF_FilterDataGrid", sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@SPTurno", SqlDbType.NVarChar).Value = Turno;
                sqlCmd.Parameters.AddWithValue("@SPInfechaInicio", SqlDbType.NVarChar).Value = FechaInicio;
                sqlCmd.Parameters.AddWithValue("@SPInfechaFinal", SqlDbType.NVarChar).Value = FechaTermino;
                sqlCmd.Parameters.AddWithValue("@SPLinea", SqlDbType.NVarChar).Value = Linea;
                sqlCmd.Parameters.AddWithValue("@SPCoil", SqlDbType.NVarChar).Value = Coil;
                sqlCmd.Parameters.AddWithValue("@SPSoldadura", SqlDbType.NVarChar).Value = Soldadura;

                // Ejecutar el commad y pasar la data al reader
                sqlDr = sqlCmd.ExecuteReader();

                // Iterar en el reader y printear la data 
                while (sqlDr.Read())
                {
                    ListaReporteDeFugas.Add(new CsrfReporteDeFuga
                    {
                        NumSerie = (string)sqlDr["NumSerie"],
                        NombreCoil = (string)sqlDr["NombreCoil"],
                        Soldador = (string)sqlDr["Soldador"],
                        Tipo = (string)sqlDr["Tipo"],
                        Area = (string)sqlDr["Area"],
                        Seccion = (string)sqlDr["Seccion"],
                        Numero = (string)sqlDr["Numero"],
                        PiezaRetrabajada = (string)sqlDr["PiezaRetrabajada"],
                        FugaFalsa = (string)sqlDr["FugaFalsa"],
                        Linea = (int)sqlDr["Linea"],
                        Turno = (int)sqlDr["Turno"],
                        Operador = (string)sqlDr["Operador"],
                        Fecha = (DateTime)sqlDr["Fecha"],
                        TipoExtra = (string)sqlDr["TipoExtra"]
                    });
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("La Ejecución del Store Procedure fallo");
                Debug.WriteLine(e.Message);
            }
            finally 
            {
                if(sqlConn != null) sqlConn.Close();

                if(sqlDr != null) sqlDr.Close();
            }

        }
    }
}
