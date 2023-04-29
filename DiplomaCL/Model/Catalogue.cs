using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiplomaCL.Model
{
    public class Catalogue : NotifyPropertyChanged
    {
        public int Id { get; set; }

        private string name;
        public string Name 
        { 
            get => name; 
            set
            {
                name = value;
                OnPropertyChanged("Name");
            } 
        }

        private string password;
        public string Password { 
            get => password; 
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private bool isSelected;
        public virtual bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Catalogue> catalogues = new ObservableCollection<Catalogue>();
        public virtual ObservableCollection<Catalogue> Catalogues 
        { 
            get => catalogues;
            set
            {
                catalogues = value;
                OnPropertyChanged("Catalogues");
            }
        }

        private ObservableCollection<Partiture> partitures = new ObservableCollection<Partiture>();
        public virtual ObservableCollection<Partiture> Partitures 
        { 
            get => partitures; 
            set
            {
                partitures = value;
                OnPropertyChanged("Partitures");
            }
        }
    }
}
