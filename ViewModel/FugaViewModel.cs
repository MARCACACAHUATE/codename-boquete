using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using codename_boquete.Services;
using codename_boquete.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Collections.ObjectModel;

namespace codename_boquete.ViewModel
{
    public class FugaViewModel : ViewModelBase
    {
        // Fields
        private string _especificacionFuga;
        private string _nombreCoil;
        private string _seccion;
        private string _numero;
        private string _soldadorSelect;
        private ObservableCollection<string> _soldador = new ObservableCollection<string>();
        private string _tipo;
        private string _area;
        private string _comentarios;
        private string _totalNumeros;
        private RegistroFuga _registro = new RegistroFuga();
        private List<string> _listSecciones;
        private List<string> _listNumeros;
        private List<CsrfDataNumber> _listDataNumber;
        private ObservableCollection<DetallesFuga> _listDetallesFuga = new ObservableCollection<DetallesFuga>();
        private List<string> _listTipos = new List<string>(){ "Header", "TXV", "Distribuidor", "Capilar", "RCD"};
        private List<string> _listAreas = new List<string>(){ "MCHX 1", "LEA", "Velo", "Evap"};

        private string _numeroSerie;
        private bool _coilScrap;
        private bool _fugaFalsa;
        private int _linea;
        private int _turno;
        private string _retrabajador;

        // Propiedades
        public string SoldadorSelect
        {
            get => _soldadorSelect;
            set
            {
                _soldadorSelect = value;
                onPropertyChanged(nameof(SoldadorSelect));
            }
        }
        public ObservableCollection<DetallesFuga> ListDetallesFuga
        {
            get => _listDetallesFuga;
            set
            {
                _listDetallesFuga = value;
                onPropertyChanged(nameof(ListDetallesFuga));
            }
        }
        public RegistroFuga Registro
        {
            get => _registro;
            set
            {
                _registro = value;
                NombreCoil = _registro.NombreCoil;
                onPropertyChanged(nameof(Registro));
            }
        }
        public string EspecificacionFuga
        {
            get => _especificacionFuga;
            set
            {
                _especificacionFuga = value;
                onPropertyChanged(nameof(EspecificacionFuga));
            }
        }
        public List<string> ListSecciones
        {
            get => _listSecciones;
            set
            {
                _listSecciones = value;
                onPropertyChanged(nameof(ListSecciones));
            }
        }
        public List<string> ListNumeros
        {
            get => _listNumeros;
            set
            {
                _listNumeros = value;
                onPropertyChanged(nameof(ListNumeros));
            }
        }
        public List<CsrfDataNumber> ListDataNumber
        {
            get => _listDataNumber;
            set
            {
                _listDataNumber = value;
                onPropertyChanged(nameof(ListDataNumber));
            }
        }
        public string TotalNumeros
        {
            get => _totalNumeros;
            set
            {
                _totalNumeros = value;
                onPropertyChanged(nameof(TotalNumeros));
                List<int> numeros = Enumerable.Range(1, int.Parse(value)).ToList();
                ListNumeros = numeros.ConvertAll<string>(x => x.ToString());
            }
        }
        public string NombreCoil
        {
            get => _nombreCoil;
            set
            {
                _nombreCoil = value;
                onPropertyChanged(nameof(NombreCoil));

            }
        }
        public string Seccion
        {
            get => _seccion;
            set
            {
                _seccion = value;
                onPropertyChanged(nameof(Seccion));
                TotalNumeros = (from data in ListDataNumber
                                where data.Color == value
                                select data.Cantidad).ToList()[0];
            }
        }
        public string Numero
        {
            get => _numero;
            set
            {
                _numero = value;
                onPropertyChanged(nameof(Numero));
                ExecuteEspecificacionFugaViewCommand(null);
            }
        }
        public ObservableCollection<string> Soldador
        {
            get => _soldador;
            set
            {
                _soldador = value;
                onPropertyChanged(nameof(Soldador));
            }
        }
        public string Tipo
        {
            get => _tipo;
            set
            {
                _tipo = value;
                onPropertyChanged(nameof(Tipo));
            }
        }
        public string Area
        {
            get => _area;
            set
            {
                _area = value;
                onPropertyChanged(nameof(Area));

            }
        }
        public string Comentarios
        {
            get => _comentarios;
            set
            {
                _comentarios = value;
                onPropertyChanged(nameof(Comentarios));
            }
        }
        public List<string> ListTipos
        {
            get => _listTipos;
            set
            {
                _listTipos = value;
                onPropertyChanged(nameof(ListTipos));
            }
        }
        public List<string> ListAreas
        {
            get => _listAreas;
            set
            {
                _listAreas = value;
                onPropertyChanged(nameof(ListAreas));
            }
        }


        public string NumeroSerie 
        { 
            get => _numeroSerie; 
            set
            {
                _numeroSerie = value;
                onPropertyChanged(nameof(NumeroSerie));
            }  
        }
        public bool CoilScrap 
        { 
            get => _coilScrap; 
            set
            {
                _coilScrap = value;
                onPropertyChanged(nameof(CoilScrap));
            }
        }
        public bool FugaFalsa 
        { 
            get => _fugaFalsa; 
            set
            {
                _fugaFalsa = value;
                onPropertyChanged(nameof(FugaFalsa));
            }
        }
        public int Linea 
        { 
            get => _linea;
            set
            {
                _linea = value;
                onPropertyChanged(nameof(Linea));
            }
        }
        public int Turno 
        { 
            get => _turno; 
            set
            {
                _turno = value;
                onPropertyChanged(nameof(Turno));
            }
        }
        public string Retrabajador 
        { 
            get => _retrabajador; 
            set
            {
                _retrabajador = value;
                onPropertyChanged(nameof(Retrabajador));
            }
        }

        // Comandos 
        public ICommand AddRegistroNuevo { get; }
        public ICommand EspecificacionFugaViewCommand { get; }
        public ICommand GuardarRegistroFugaViewCommand { get; }


        // Constructor
        public FugaViewModel()
        {
            EspecificacionFuga = "Especificación de Fuga";
            //PubSub<object>.RegisterEvent("AddRegistro", AddRegistroHandler);

            AddRegistroNuevo = new ViewModelCommand(ExecuteAddRegistroNuevo);
            EspecificacionFugaViewCommand = new ViewModelCommand(ExecuteEspecificacionFugaViewCommand);
            GuardarRegistroFugaViewCommand = new ViewModelCommand(ExecuteGuardarRegistrosFugaViewCommand);

            LocalDataStoreSlot slot = Thread.GetNamedDataSlot("registro");
            Registro = (RegistroFuga)Thread.GetData(slot);
            NumeroSerie = Registro.NumSerie;
            CoilScrap = Registro.CoilParaScrap;
            FugaFalsa = Registro.FugaFalsa;
            Linea = Registro.Linea;
            Turno = Registro.Turno;
            Retrabajador = Registro.RetrabajadorSelect;

            ExecuteFugaDataNumber(null);
        }


        // Metodos
        public void ExecuteAddRegistroNuevo(object obj)
        {
            ListDetallesFuga.Add(new DetallesFuga
            {
                Seccion = Seccion,
                Numero = Numero,
                Soldador = SoldadorSelect,
                Tipo = Tipo,
                Area = Area,
                Comentarios = Comentarios,
            });
        }

        public void ExecuteFugaDataNumber(object obj)
        {
            using (DESProyectoContext db = new DESProyectoContext())
            {
                ListDataNumber = (from data in db.CsrfDataNumbers
                                             select data).ToList();

                ListSecciones = (from seccion in ListDataNumber
                                select seccion.Color).ToList();
            }

        }

        public void ExecuteEspecificacionFugaViewCommand(object obj)
        {
            SqlConnection sqlConn = null;
            SqlDataReader sqlDr = null;
            Soldador = new ObservableCollection<string>();

            try
            {
                // Abre la conexion a la DB
                sqlConn = new SqlConnection("Server=127.0.0.1;Database=DESProyecto;User=SA;Password=arribaLasChivas10;MultipleActiveResultSets=true;");
                sqlConn.Open();

                Debug.WriteLine("lista de soldadoes -> " + Registro.NumSerie);

                // Crea un objeto commando con parametros para el sp
                SqlCommand sqlCmd = new SqlCommand("dbo.spOut_CSRF_Soldadores", sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@SPNombreCoil", SqlDbType.NVarChar).Value = "A coil 3x24 Cyclone";
                sqlCmd.Parameters.AddWithValue("@SPSeccion", SqlDbType.NVarChar).Value = Seccion;
                sqlCmd.Parameters.AddWithValue("@SPNumero", SqlDbType.Int).Value = Numero;

                // Ejecutar el commad y pasar la data al reader
                sqlDr = sqlCmd.ExecuteReader();

                // Iterar en el reader y printear la data 
                while (sqlDr.Read())
                {
                    Soldador.Add((string)sqlDr["Soldador"]);
                    EspecificacionFuga = (string)sqlDr["TipoDeFuga"];
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

        public void ExecuteGuardarRegistrosFugaViewCommand(object obj)
        {

            SqlConnection sqlConn = null;
            SqlDataReader sqlDr = null;

            try
            {
                // Abre la conexion a la DB
                sqlConn = new SqlConnection("Server=127.0.0.1;Database=DESProyecto;User=SA;Password=arribaLasChivas10;MultipleActiveResultSets=true;");
                sqlConn.Open();


                // iteramos la lista de detalles
                foreach (DetallesFuga detalle in ListDetallesFuga) {

                    // Crea un objeto commando con parametros para el sp
                    SqlCommand sqlCmd = new SqlCommand("spIn_CSRF_RegistroDeFugas", sqlConn);
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.Parameters.AddWithValue("@SPNumSerie", SqlDbType.NVarChar).Value = Registro.NumSerie != null ? Registro.NumSerie : "null";
                    sqlCmd.Parameters.AddWithValue("@SPTipo", SqlDbType.NVarChar).Value = detalle.Tipo != null ? detalle.Tipo : "null";
                    sqlCmd.Parameters.AddWithValue("@SPArea", SqlDbType.NVarChar).Value = detalle.Area != null ? detalle.Area : "null";
                    sqlCmd.Parameters.AddWithValue("@SPSoldador", SqlDbType.NVarChar).Value = detalle.Soldador != null ? detalle.Soldador : "null";
                    sqlCmd.Parameters.AddWithValue("@SPSeccion", SqlDbType.NVarChar).Value = detalle.Seccion != null ? detalle.Seccion : "null";
                    sqlCmd.Parameters.AddWithValue("@SPNumero", SqlDbType.NVarChar).Value = detalle.Numero != null ? detalle.Numero : "null";
                    sqlCmd.Parameters.AddWithValue("@SPNombreCoil", SqlDbType.NVarChar).Value = Registro.NombreCoil != null ? Registro.NombreCoil : "null";
                    sqlCmd.Parameters.AddWithValue("@SPCoilRechazado", SqlDbType.NVarChar).Value = "null";
                    sqlCmd.Parameters.AddWithValue("@SPFugaFalsa", SqlDbType.NVarChar).Value = Registro.FugaFalsa;
                    sqlCmd.Parameters.AddWithValue("@SPFecha", SqlDbType.DateTime).Value = DateTime.Today;
                    sqlCmd.Parameters.AddWithValue("@SPLinea", SqlDbType.Int).Value = Registro.Linea;
                    sqlCmd.Parameters.AddWithValue("@SPTurno", SqlDbType.Int).Value = Registro.Turno;
                    sqlCmd.Parameters.AddWithValue("@SPOperador", SqlDbType.NVarChar).Value = "null";
                    sqlCmd.Parameters.AddWithValue("@SPComentarios", SqlDbType.NVarChar).Value = detalle.Comentarios != null ? detalle.Comentarios : "null";
                    sqlCmd.Parameters.AddWithValue("@SPPzaRetrabajada", SqlDbType.NVarChar).Value = Registro.RetrabajadorSelect != null ? Registro.RetrabajadorSelect : "null";
                    sqlCmd.Parameters.AddWithValue("@SPTipoExtra", SqlDbType.NVarChar).Value = "null";
                    sqlCmd.Parameters.AddWithValue("@CostXUnit", SqlDbType.NVarChar).Value = "null";

                    sqlDr = sqlCmd.ExecuteReader();
                }

                ListDetallesFuga = new ObservableCollection<DetallesFuga>();

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

        // Method Handler 
        public void AddRegistroHandler(object sender, PubSubEventArgs<object> arg)
        {
            Registro = (RegistroFuga)arg.Item;

            NumeroSerie = Registro.NumSerie;
            NombreCoil = Registro.NombreCoil;
            FugaFalsa = Registro.FugaFalsa;
            Linea = Registro.Linea;
            Turno = Registro.Turno;
            Retrabajador = Registro.RetrabajadorSelect;
        }
    }
}
