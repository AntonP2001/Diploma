using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.Entity;
using DiplomaCL.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DiplomaUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        #region Свойства

        private Catalogue _catalogue;
        public Catalogue Catalogue
        {
            get => _catalogue;
            set
            {
                _catalogue = value;
                if (_catalogue != value)
                    OnPropertyChanged();
            }
        }

        private Catalogue _currentCatalogue;
        public Catalogue CurrentCatalogue
        {
            get => _currentCatalogue;
            set {
                _currentCatalogue = value;
                if (_currentCatalogue != value)
                    OnPropertyChanged();
            }    
        }

        private ObservableCollection<Partiture> _partitures;
        public ObservableCollection<Partiture> Partitures
        {
            get => _partitures;
            set
            {
                _partitures = value;
                if (_partitures != value)
                    OnPropertyChanged();
            }
        }

        private ObservableCollection<Catalogue> _items;
        public ObservableCollection<Catalogue> Items
        {
            get => _items;
            set
            {
                _items = value;
                if (_items != value)
                    OnPropertyChanged();
            }
        }

        #endregion

        DipDbContext dbContext;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public MainWindow()
        {
            dbContext = new DipDbContext();
            InitializeComponent();
        }

        private void OnMainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Завершить работу", MessageBoxButton.OKCancel, MessageBoxImage.None)
                == MessageBoxResult.OK) Application.Current.Shutdown();
            else e.Cancel = true;
        }

        private void OnAddingRecordClick(object sender, RoutedEventArgs e)
        {
            PartitureForm partitureForm = new PartitureForm();
            if (partitureForm.ShowDialog() == true) MessageBox.Show("Запись добавлена!");
        }

        private void OnWindowInit(object sender, EventArgs e)
        {
            var dbCatalogues = new ObservableCollection<Catalogue>(dbContext.Catalogues.Select(x => x).ToList());
            Items = new ObservableCollection<Catalogue>();
            Items.Add(dbCatalogues[0]);
            catalogueTreeView.ItemsSource = Items;
            Partitures = new ObservableCollection<Partiture>(dbContext.Partitures.Where(x => x.Catalogue.Name == "Root"));
            partituresList.ItemsSource = Partitures;
            Catalogue = new Catalogue();
            catalogueName.DataContext = Catalogue;
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            CurrentCatalogue = e.NewValue as Catalogue;
        }

        private void OnAddingCatalogue(object sender, RoutedEventArgs e)
        {
            Catalogue.Name = catalogueName.Text;
            catalogueName.Text = null; 
            if(CurrentCatalogue.Catalogues == null)
                CurrentCatalogue.Catalogues = new ObservableCollection<Catalogue>();
            CurrentCatalogue.Catalogues.Add(Catalogue);
            dbContext.Entry(CurrentCatalogue).State = EntityState.Modified;
            dbContext.SaveChanges();
            var dbCatalogues = new ObservableCollection<Catalogue>(dbContext.Catalogues.Select(x => x).ToList());
            Items = new ObservableCollection<Catalogue>();
            Items.Add(dbCatalogues[0]);
            catalogueTreeView.ItemsSource = Items;
        }

        private void OnDeletingCatalogue(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as Control;
            CurrentCatalogue.ParentCatalogue.Catalogues.Remove(menuItem.DataContext as Catalogue);
            dbContext.Entry(CurrentCatalogue.ParentCatalogue).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
