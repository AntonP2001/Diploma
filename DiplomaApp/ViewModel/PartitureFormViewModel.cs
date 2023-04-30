using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaCL.Model;

namespace DiplomaUI.MVVM
{
    public class PartitureFormViewModel : NotifyPropertyChanged
    {
        private Partiture _partiture;
        public Partiture Partiture
        {
            get => _partiture;
            set
            {
                _partiture = value;
                OnPropertyChanged();
            }
        }

        public PartitureFormViewModel() {
            Partiture = null;
        }
    }
}
