using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
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
        public ICommand ShowFugaDetailViewCommand { get; }
        public ICommand ShowTablaDeRegistroViewCommand { get; }

        // Constructor
        public MainViewModel()
        {
            // Inicializamos los comandos para mostrar las vistas
            ShowFormularioViewCommand = new ViewModelCommand(ExecuteShowFormularioViewCommand);
            ShowFugaDetailViewCommand = new ViewModelCommand(ExecuteShowFugaDetailViewCommand);
            ShowTablaDeRegistroViewCommand = new ViewModelCommand(ExecuteShowTablaDeRegistroViewCommand);

            // View por Defecto
            ExecuteShowFormularioViewCommand(null);

        }


        // Metodos
        private void ExecuteShowFormularioViewCommand(object obj)
        {
            CurrentChildView = new FormularioViewModel();

        }

        public void ExecuteShowFugaDetailViewCommand(object obj)
        {
            CurrentChildView = new FugaDetailViewModel();
        }

        private void ExecuteShowTablaDeRegistroViewCommand(object obj)
        {
            CurrentChildView = new RegistroViewModel();
        }

    }
}
