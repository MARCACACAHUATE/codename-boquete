using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace codename_boquete.ViewModel
{
    public class FugaViewModel: ViewModelBase
    {
        // Fields
        private List<Registro> _listaRegistro = new List<Registro>();
        // Comandos 
        public ICommand AddRegistroNuevo { get; }

        // Propiedades
        public List<Registro> ListaRegistro
        {
            get
            {
                return _listaRegistro;
            }  
            
            set
            {
                _listaRegistro = value;
                onPropertyChanged(nameof(ListaRegistro));
            }
        }

        // Constructor
        public FugaViewModel()
        {
            AddRegistroNuevo = new ViewModelCommand(ExecuteAddRegistroNuevo);

            ListaRegistro.Add(new Registro
            {
                Clientes = "",
                Adoran = "",
                Comer = "",
                Aqui = ""
            });

        }

        // Metodos
        public void ExecuteAddRegistroNuevo(object obj)
        {
            ListaRegistro.Add(new Registro
            {
                Clientes = "Tu Pinche cola",
                Adoran = "adoran",
                Comer = "comer",
                Aqui = "aquji"
            });
        }
    }

    public class Registro
    {
        public string Clientes { get; set; }
        public string Adoran { get; set; }
        public string Comer { get; set; }
        public string Aqui { get; set; }
    }
}
