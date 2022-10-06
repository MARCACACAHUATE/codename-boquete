using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace codename_boquete.ViewModel {

    // Esta clase implementa las notificaciones de cambios de propiedades
    public abstract class ViewModelBase : INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;

        // Notifica a los cliente de enlace que un valor de propiedad a cambiado y que debe volver 
        // a evaluar sus valores.
        public void onPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
