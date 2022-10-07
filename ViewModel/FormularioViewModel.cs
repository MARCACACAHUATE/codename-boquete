using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using codename_boquete.View;

namespace codename_boquete.ViewModel
{
    public class FormularioViewModel: ViewModelBase
    {
        // Fields
        private FugaView _fugaView;

        // Comandos
        public ICommand ShowFugaDetailViewCommand { get; }

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

        // Constructor
        public FormularioViewModel()
        {
            ShowFugaDetailViewCommand = new ViewModelCommand(ExecuteShowFugaViewCommand);
        }

        // Metodos
        public void ExecuteShowFugaViewCommand(object obj)
        {
            FugaView = new FugaView();
            FugaView.Show();
        }
    }
}
