using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace codename_boquete.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        // Fields
        private ViewModelBase _currentChildView;

        // Propiedades
        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }

            set
            {
                _currentChildView = value;
                onPropertyChanged(nameof(CurrentChildView));
            }
        }
        // Comandos mamalones
        public ICommand ShowFormularioViewCommand { get; }

        // Constructor
        public MainViewModel()
        {
            // Inicializamos los comandos para mostrar las vistas
            ShowFormularioViewCommand = new ViewModelCommand(ExecuteShowFormularioViewCommand);

            // View por Defecto
            ExecuteShowFormularioViewCommand(null);

        }


        // Metodos
        private void ExecuteShowFormularioViewCommand(object obj)
        {
            CurrentChildView = new FormularioViewModel();

        }
    }
}
