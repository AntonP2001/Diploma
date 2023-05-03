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
using Microsoft.Win32;
using System.IO;
using Aspose.Pdf;

namespace DiplomaUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        #region Свойства
          
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
            var dbCatalogues = new ObservableCollection<Catalogue>(dbContext.Catalogues.Select(x => x)
                .Include(x => x.Partitures)
                .ToList());
            Items = new ObservableCollection<Catalogue>();
            Items.Add(dbCatalogues[0]);
            catalogueTreeView.ItemsSource = Items;
            Partitures = new ObservableCollection<Partiture>(dbContext.Partitures.Where(x => x.Catalogue.Name == "Root"));
            partituresList.ItemsSource = Partitures;
            Catalogue = new Catalogue();
            catalogueName.DataContext = Catalogue;
            Partiture = new Partiture();
            partitureGridView.DataContext = Partiture;
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            CurrentCatalogue = e.NewValue as Catalogue;
            if(CurrentCatalogue.Password != null)
            {
                CataloguePasswordEnteringWindow window = new CataloguePasswordEnteringWindow();
                window.DataContext = CurrentCatalogue;
                var result = window.ShowDialog();
                if (result == false)
                    MessageBox.Show("Неверный пароль!", "");
                else
                {
                    Partitures = CurrentCatalogue.Partitures;
                    partituresList.ItemsSource = Partitures;
                }
            }
            Partitures = CurrentCatalogue.Partitures;
            partituresList.ItemsSource = Partitures;
        }

        private void OnAddingCatalogue(object sender, RoutedEventArgs e)
        {
            Catalogue.Name = catalogueName.Text;
            catalogueName.Text = null;
            if (cataloguePassword.Text != "")
                Catalogue.Password = cataloguePassword.Text;
            if (CurrentCatalogue.Catalogues == null)
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

        private void OnAddingPartiture(object sender, RoutedEventArgs e)
        {
            if (CurrentCatalogue.Partitures == null)
                CurrentCatalogue.Partitures = new ObservableCollection<Partiture>();
            var dataContext = partitureGridView;
            CurrentCatalogue.Partitures.Add(partitureGridView.DataContext as Partiture);
            dbContext.Entry(CurrentCatalogue).State = EntityState.Modified;
            dbContext.SaveChanges();
            Partitures = CurrentCatalogue.Partitures;
        }

        private void PartitureViewForm_DeleteClick(object sender, RoutedEventArgs e)
        {
            var partitureForm = sender as Control;
            Partitures.Remove(partitureForm.DataContext as Partiture);
            dbContext.Entry(partitureForm.DataContext as Partiture).State = EntityState.Deleted;
            dbContext.SaveChanges();
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
            Application.Current.Shutdown();
        }

        private async void OnLoadingFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "PDF-файл (*.pdf)|*.pdf",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == true)
            {
                var partiture = Partiture;
                partiture.File = File.ReadAllBytes(openFileDialog.FileName);
                await AsyncCreateFirstPagePDFImage(openFileDialog.FileName, partiture);
            }
        }

        public async Task AsyncCreateFirstPagePDFImage(string filename, Partiture partiture)
        {
            Task.Run(() => CreateFirstPagePDFImage(filename, partiture));
        }

        public async Task CreateFirstPagePDFImage(string file, Partiture partiture)
        {
            Document document = new Document(file);
            partiture.Image = document.Pages[1].AsByteArray(new Aspose.Pdf.Devices.Resolution(100));
        }

        private void OnAddingCatalogueButton(object sender, RoutedEventArgs e)
        {
            if (partitureGridView.Visibility == Visibility.Collapsed)
                partitureGridView.Visibility = Visibility.Visible;
            else
                partitureGridView.Visibility = Visibility.Collapsed;
        }

        private void filterPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (partitureGridView.Visibility == Visibility.Visible)
                filterPanel.Height = 85;
            else
                filterPanel.Height = 55;
        }

        private void filterPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            filterPanel.Height = 10;
        }

        private void filterPanel_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filteredPartitures = Partitures.ToList();

            filteredPartitures = filteredPartitures
                .FindAll(x => x.Name.ToLower().StartsWith(nameFilter.Text.ToLower()))
                .FindAll(x => x.Author.ToLower().StartsWith(authorFilter.Text.ToLower()))
                .FindAll(x => x.Style.ToLower().StartsWith(styleFilter.Text.ToLower()))
                .FindAll(x => x.WorkType.ToLower().StartsWith(workTypeFilter.Text.ToLower()))
                .FindAll(x => x.Instrumentation.ToLower().StartsWith(instrumentationFilter.Text.ToLower()))
                .FindAll(x => x.Language.ToLower().StartsWith(languageFilter.Text.ToLower()));

            var partitures = new ObservableCollection<Partiture>(filteredPartitures);
            partituresList.ItemsSource = partitures;
        }
    }
}
