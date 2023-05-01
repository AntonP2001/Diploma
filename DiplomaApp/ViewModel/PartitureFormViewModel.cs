using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DiplomaUI.Infrastructure;
using DiplomaUI.Infrastructure.Commands;
using DiplomaUI.Model;

namespace DiplomaUI.ViewModel
{
    public class PartitureFormViewModel : NotifyPropertyChanged
    {
        private ICommand _command;
        public ICommand Command
        {
            get => new RelayCommand(o => {
                    var k = 12;
                }, o => true);
            set { }
        }

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
            //Partiture = null;
        }
    }
}
