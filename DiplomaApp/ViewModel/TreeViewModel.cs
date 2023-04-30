using DiplomaCL.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;

namespace DiplomaUI.MVVM
{
    public class TreeViewModel : NotifyPropertyChanged
    {
        private DipDbContext dbContext;

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

        public TreeViewModel()
        {
            dbContext = new DipDbContext();
            //List<Catalogue> catalogues = dbContext.Catalogues.Include(x => x.Catalogues)
            Items = new ObservableCollection<Catalogue>(dbContext.Catalogues.Where(x => x.Name == "Root").Include(x => x.Catalogues).ToList());
            Catalogue = new Catalogue();
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

        private ICommand _addChildCommand;
        public ICommand AddChildCommand
        {
            get
            {
                return _addChildCommand ?? (new RelayCommand(parameter =>
                {
                    CurrentCatalogue = Walk(Items[0]);
                    CurrentCatalogue.Catalogues.Add(Catalogue);
                    dbContext.Entry(CurrentCatalogue).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    Catalogue = new Catalogue();
                }));
            }
        }

        public Catalogue Walk(Catalogue catalogues)
        {
            Queue<Catalogue> queueCatalogues = new Queue<Catalogue>();

            queueCatalogues.Enqueue(catalogues);
            while(queueCatalogues.Count > 0)
            {
                Catalogue catalogue = queueCatalogues.Dequeue();
                if (catalogue.IsSelected == true) return catalogue;
                else
                {
                    foreach(var c in catalogue.Catalogues)
                    {
                        queueCatalogues.Enqueue(c);
                    }
                }
            }
            return null;
        }
    }
}