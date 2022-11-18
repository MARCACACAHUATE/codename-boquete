using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;
using codename_boquete.View;
using codename_boquete.Model;
using System.Security.Principal;
using codename_boquete.Services;
using codename_boquete.ViewModel;

namespace codename_boquete.ViewModel
{
    public class FormularioViewModel: ViewModelBase
    {
        // TestObservable
        public event PubSubEventHandler<object> AddRegistroHandler;

        // Fields
        private FugaView _fugaView;
        private string _numSerie;
        private string _coilSelect;
        private bool _coilParaScrap;
        private bool _fugaFalsa;
        private int _linea;
        private int _turno;
        private string _retrabajadorSelect;
        private List<string> _retrabajadores;
        private List<string> _nombreCoil;


        // Propiedades
        public FugaView FugaView
        {
            get
            {
                return _fugaView;
            }  
            set
            {
                _fugaView = value;
                onPropertyChanged(nameof(FugaView));
            }
        }

        public List<string> NombreCoil 
        {
            get
            {
                return _nombreCoil;
            }

            set
            {
                 _nombreCoil = value;
                onPropertyChanged(nameof(NombreCoil));
            }  
        }

        public List<string> Retrabajadores 
        {
            get 
            { 
                return _retrabajadores;
            }
            set
            {
                _retrabajadores = value;
                onPropertyChanged(nameof(Retrabajadores));
            }   
        }

        public string NumSerie 
        { 
            get => _numSerie; 
            set
            {
                _numSerie = value;
                onPropertyChanged(nameof(NumSerie));
            }  
        }
        public bool CoilParaScrap 
        { 
            get => _coilParaScrap; 
            set
            {
                _coilParaScrap = value;
                onPropertyChanged(nameof(CoilParaScrap));
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
        public string CoilSelect 
        { 
            get => _coilSelect; 
            set
            {
                _coilSelect = value;
                onPropertyChanged(nameof(CoilSelect));
            }  
        }
        public string RetrabajadorSelect 
        { 
            get => _retrabajadorSelect; 
            set
            {
                _retrabajadorSelect = value;
                onPropertyChanged(nameof(RetrabajadorSelect));
            }  
        }

        // Comandos
        public ICommand ShowFugaDetailViewCommand { get; }
        public ICommand CoilScrapViewCommand { get;  }
        public ICommand FugaFalsaViewCommand { get; }
        public ICommand LineaViewCommand { get; }
        public ICommand TurnoViewCommand { get; }

        // Constructor
        public FormularioViewModel()
        {
            // Test
            PubSub<object>.AddEvent("AddRegistro", AddRegistroHandler);

            ShowFugaDetailViewCommand = new ViewModelCommand(ExecuteShowFugaViewCommand);
            CoilScrapViewCommand = new ViewModelCommand(ExecuteCoilScrapViewCommand);
            FugaFalsaViewCommand = new ViewModelCommand(ExecuteFugaFalsaViewCommand);
            LineaViewCommand = new ViewModelCommand(ExecuteLineaViewCommand);
            TurnoViewCommand = new ViewModelCommand(ExecuteTurnoViewCommand);


            ExecuteListCoilsNames(null);
        }

        // Metodos
        public void ExecuteShowFugaViewCommand(object obj)
        {
            RegistroFuga registro = new RegistroFuga
            {
                NumSerie = NumSerie,
                CoilParaScrap = CoilParaScrap,
                FugaFalsa = FugaFalsa,
                Linea = Linea,
                Turno = Turno,
                RetrabajadorSelect = RetrabajadorSelect,
                NombreCoil = CoilSelect
            };

            PubSub<object>.RaiseEvent("AddRegistro", this, new PubSubEventArgs<object>(registro));

            //Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(registro), null);

            // Prueba de threads
            LocalDataStoreSlot slot = Thread.GetNamedDataSlot("registro");
            Thread.SetData(slot, registro);
            //

            FugaView = new FugaView();
            FugaView.ShowDialog();

            //System.Diagnostics.Debug.WriteLine("Numero series " + NumSerie);
            //System.Diagnostics.Debug.WriteLine("Coil para Scrap " + CoilParaScrap);
            //System.Diagnostics.Debug.WriteLine("Fuga " + FugaFalsa);
            //System.Diagnostics.Debug.WriteLine("Linea " + Linea);
            //System.Diagnostics.Debug.WriteLine("Turno " + Turno);
            //System.Diagnostics.Debug.WriteLine("retrabajador " + RetrabajadorSelect);
            //System.Diagnostics.Debug.WriteLine("nombre Coil " + CoilSelect);
        }

        public void ExecuteCoilScrapViewCommand(object obj)
        {

            CoilParaScrap = (string)obj == "Si" ? true : false;
        }

        public void ExecuteFugaFalsaViewCommand(object obj)
        {
            FugaFalsa = (string)obj == "Si" ? true : false;
        }

        public void ExecuteLineaViewCommand(object obj)
        {
            Linea = int.Parse((string)obj);
        }

        public void ExecuteTurnoViewCommand(object obj)
        {
            Turno = int.Parse((string)obj);
        }

        public void ExecuteListCoilsNames(object obj)
        {
            using(FimeContext db = new FimeContext())
            {
                List<string> listModelos = (from data in db.CsrfDataNumbers 
                                            select data.Modelo).ToList();

                List<string> listRetrabajadores = (from data in db.CsrfRetrabajadores
                                                   where data.Nombre != null
                                                   select data.Nombre).ToList();

                NombreCoil = listModelos;
                Retrabajadores = listRetrabajadores;
            }
        }
    }
}
