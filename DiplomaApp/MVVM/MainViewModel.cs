using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DiplomaCL.Model;

namespace DiplomaUI.MVVM
{
    public class MainViewModel : NotifyPropertyChanged
    {
        private DipDbContext dbContext;  

        private ObservableCollection<Partiture> _partitures;
        public ObservableCollection<Partiture> Partitures
        {
            get => _partitures;
            set
            {
                _partitures = value;
                OnPropertyChanged();
            }
        }

        public ICommand EditPartiture {
            get => new RelayCommand(o =>
            {
                var k = 12;
                k = 11;
            }, o => true);
            private set { }}
        public ICommand DeletePartiture { get; private set; }

        private ICommand _addPartitureCommand;
        public ICommand AddPartitureCommand
        {
            get => new RelayCommand(o =>
            {
                var treeViewModel = o;
            });
        }

        public MainViewModel()
        {
            dbContext = new DipDbContext();
            Partitures = new ObservableCollection<Partiture>(dbContext.Partitures.Select(x => x));
        }
    }
}
