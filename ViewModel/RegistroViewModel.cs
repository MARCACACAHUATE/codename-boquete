using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codename_boquete.Model;

namespace codename_boquete.ViewModel
{
    public class RegistroViewModel: ViewModelBase
    {
        // Fields
        private List<CsrfReporteDeFuga> _ListaReporteDeFugas;


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


        // Comandos

        // Constructor
        public RegistroViewModel()
        {
            ExecuteListaRegistroViewCommand(null);
        }

        // Metodos
        private void ExecuteListaRegistroViewCommand(object obj)
        {
            using(Model.DESProyectoContext db = new Model.DESProyectoContext())
            {
                List<CsrfReporteDeFuga> lista = (from d in db.CsrfReporteDeFugas
                             select d).ToList();
                ListaReporteDeFugas = lista;
            }

            
        }
    }
}
