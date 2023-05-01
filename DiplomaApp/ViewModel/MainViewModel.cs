using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Aspose.Pdf;
using DiplomaUI.Data;
using DiplomaUI.Infrastructure;
using DiplomaUI.Infrastructure.Commands;
using DiplomaUI.Model;
using Microsoft.Win32;

namespace DiplomaUI.ViewModel
{
    public class MainViewModel : NotifyPropertyChanged
    {
        private DipDbContext dbContext;

        class CatalogueTreeViewItem : NotifyPropertyChanged
        {
            private Catalogue _catalogue;
            public Catalogue Catalogue
            {
                get => _catalogue;
                set
                {
                    _catalogue = value;
                    OnPropertyChanged();
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

            public CatalogueTreeViewItem() {; }

            public CatalogueTreeViewItem(Catalogue catalogue, bool isSelected)
            {
                Catalogue = catalogue;
                IsSelected = isSelected;
            }
        }

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

        private Catalogue catalogue;
        public Catalogue Catalogue
        {
            get => catalogue;
            set
            {
                catalogue = value;
                OnPropertyChanged();
            }
        }

        private Catalogue currentCatalogue;
        public Catalogue CurrentCatalogue
        {
            get => currentCatalogue;
            set
            {
                currentCatalogue = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Catalogue> items;
        public ObservableCollection<Catalogue> Items
        {
            get => items;
            set
            {
                items = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private ICommand _getCataloguePartitures;
        public ICommand GetCataloguePartitures
        {
            get => new RelayCommand(o =>
            {
                if (Walk(Items[0]) != null) Partitures = new ObservableCollection<Partiture>(Walk(Items[0]).Partitures);
            });
        }

        public ICommand EditPartiture {
            get => new RelayCommand(o =>
            {
                var k = 12;
                k = 11;
            }, o => true);
            private set { }}
        public ICommand DeletePartiture 
        {
            get => new RelayCommand(o => {
                Partitures.Remove(o as Partiture);
                dbContext.Partitures.Remove(o as Partiture);
                dbContext.SaveChanges();
            }, o=> true);
            private set {; } 
        }

        private ICommand _addPartitureCommand;
        public ICommand AddPartitureCommand
        {
            get => new RelayCommand(o =>
            {
                CurrentCatalogue = Walk(Items[0]);
                if (CurrentCatalogue == null) Items[0].Partitures.Add(o as Partiture);
                else CurrentCatalogue.Partitures.Add(o as Partiture);
                dbContext.Entry(o as Partiture).State = EntityState.Added;
                dbContext.SaveChanges();
                Catalogue = new Catalogue();
            });
        }

        private ICommand _addCatalogueCommand;
        public ICommand AddCatalogueCommand
        {
            get => new RelayCommand(o => 
            {
                CurrentCatalogue = Walk(Items[0]);
                CurrentCatalogue.Catalogues.Add(Catalogue);
                CurrentCatalogue.IsSelected = false;
                dbContext.Entry(CurrentCatalogue).State = EntityState.Modified;
                dbContext.SaveChanges();
                Catalogue = new Catalogue();
            }, o=>true);
            private set {; }
        }

        private ICommand _deleteCatalogueCommand;
        public ICommand DeleteCatalogueCommand
        {
            get => new RelayCommand(o => 
            {
                CurrentCatalogue = Walk(Items[0]);
                foreach(var partiture in CurrentCatalogue.Partitures)
                {
                    dbContext.Entry(partiture).State = EntityState.Deleted;
                }
                dbContext.Entry(currentCatalogue).State = EntityState.Deleted;
                dbContext.SaveChanges();
                Items = new ObservableCollection<Catalogue>(dbContext.Catalogues.Where(x => x.Name == "Root").Include(x => x.Catalogues).ToList());
            }, o => true);
            private set {; }
        }

        private ICommand _loadFileCommand;
        public ICommand LoadFileCommand
        {
            get => new RelayCommand(async o => 
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "PDF-файл (*.pdf)|*.pdf";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == true)
                {
                    var partiture = o as Partiture;
                    partiture.File = File.ReadAllBytes(openFileDialog.FileName);
                    await Task.Run(() => CreateFirstPagePDFImage(openFileDialog.FileName, partiture));
                }
            }, o => true);
        }

        public async Task CreateFirstPagePDFImage(string file, Partiture partiture)
        {
            Document document = new Document(file);
            partiture.Image = document.Pages[1].AsByteArray(new Aspose.Pdf.Devices.Resolution(100));
        }

        public MainViewModel()
        {
            dbContext = new DipDbContext();
            Partitures = new ObservableCollection<Partiture>(dbContext.Partitures.Where(x => x.Catalogue.Name == "Root"));
            Items = new ObservableCollection<Catalogue>(dbContext.Catalogues.Where(x => x.Name == "Root").ToList());       //.Include(x => x.Catalogues).ToList());
            Catalogue = new Catalogue();
            CurrentCatalogue = Items[0];
            Partiture = new Partiture();
        }

        public Catalogue Walk(Catalogue catalogues)
        {
            Queue<Catalogue> queueCatalogues = new Queue<Catalogue>();

            queueCatalogues.Enqueue(catalogues);
            while (queueCatalogues.Count > 0)
            {
                Catalogue catalogue = queueCatalogues.Dequeue();
                if (catalogue.IsSelected == true) return catalogue;
                else
                {
                    foreach (var c in catalogue.Catalogues)
                    {
                        queueCatalogues.Enqueue(c);
                    }
                }
            }
            return null;
        }
    }
}
