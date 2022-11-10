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

namespace codename_boquete.ViewModel
{
    public class FugaViewModel: ViewModelBase
    {
        // Fields
        private string _nombreCoil;
        private string _seccion;
        private string _numero;
        private string _soldador;
        private string _tipo;
        private string _area;
        private string _comentarios;
        private List<string> _listSecciones;

        // Propiedades
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
            }
        }
        public string Numero 
        { 
            get => _numero; 
            set
            {
                _numero = value;
                onPropertyChanged(nameof(Numero));
            }  
        }
        public string Soldador 
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
        public List<string> ListSecciones 
        { 
            get => _listSecciones;
            set
            {
                _listSecciones = value;
                onPropertyChanged(nameof(ListSecciones));
            }  
        }


        // Comandos 
        public ICommand AddRegistroNuevo { get; }
        public ICommand EspecificacionFugaViewCommand { get; }


        // Constructor
        public FugaViewModel()
        {
            PubSub<object>.RegisterEvent("AddRegistro", AddRegistroHandler);
            AddRegistroNuevo = new ViewModelCommand(ExecuteAddRegistroNuevo);
            EspecificacionFugaViewCommand = new ViewModelCommand(ExecuteEspecificacionFugaViewCommand);
            //ExecuteFugaDataNumber(null);
        }


        // Metodos
        public void ExecuteAddRegistroNuevo(object obj)
        {
        }


        public void ExecuteEspecificacionFugaViewCommand(object obj)
        {
            SqlConnection sqlConn = null;
            SqlDataReader sqlDr = null;

            try
            {
                // Abre la conexion a la DB
                sqlConn = new SqlConnection("Server=127.0.0.1;Database=DESProyecto;User=SA;Password=arribaLasChivas10;MultipleActiveResultSets=true;");
                sqlConn.Open();

                // Crea un objeto commando con parametros para el sp
                SqlCommand sqlCmd = new SqlCommand("dbo.spOut_CSRF_Soldadores", sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@SPSeccion", SqlDbType.NVarChar).Value = "Azul";
                sqlCmd.Parameters.AddWithValue("@SPNumero", SqlDbType.Int).Value = 1;
                sqlCmd.Parameters.AddWithValue("@SPNombreCoil", SqlDbType.NVarChar).Value = NombreCoil;

                // Ejecutar el commad y pasar la data al reader
                sqlDr = sqlCmd.ExecuteReader();

                // Iterar en el reader y printear la data 
                while (sqlDr.Read())
                {
                    Debug.WriteLine("Soldador " + sqlDr["Soldador"]);
                    Debug.WriteLine("TipoDeFuga " + sqlDr["TipoDeFuga"]);
                    Debug.WriteLine("NumeroProveedores " + sqlDr["NumeroProveedores"]);
                }
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
            NombreCoil = (string)arg.Item;
        }
    }
}
