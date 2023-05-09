using DiplomaCL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiplomaUI
{
    /// <summary>
    /// Логика взаимодействия для PartitureForm.xaml
    /// </summary>
    public partial class PartitureViewForm : UserControl
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public PartitureViewForm()
        {
            InitializeComponent();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            OnDeleteClick();
        }

        #region RoutedEvent

        #region DeleteClick

        public static readonly RoutedEvent DeleteClickEvent = EventManager.RegisterRoutedEvent(
            "DeleteClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PartitureViewForm));

        public event RoutedEventHandler DeleteClick
        {
            add { AddHandler(DeleteClickEvent, value); }
            remove { RemoveHandler(DeleteClickEvent, value); }
        }

        void RaiseDeleteClickEvent()
        {
            RaiseEvent(new RoutedEventArgs(DeleteClickEvent));
        }

        protected void OnDeleteClick()
        {
            RaiseDeleteClickEvent();
        }

        #endregion

        #region EditClick

        public static readonly RoutedEvent EditClickEvent = EventManager.RegisterRoutedEvent(
            "EditClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PartitureViewForm));

        public event RoutedEventHandler EditClick
        {
            add { AddHandler(EditClickEvent, value); }
            remove { RemoveHandler(EditClickEvent, value); }
        }

        void RaiseEditClickEvent()
        {
            RaiseEvent(new RoutedEventArgs(EditClickEvent));
        }

        public void OnEditClick()
        {
            RaiseEditClickEvent();
        }

        #endregion

        #region AboutClick 

        public static readonly RoutedEvent AboutClickEvent = EventManager.RegisterRoutedEvent(
            "AboutClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PartitureViewForm));
        
        public event RoutedEventHandler AboutClick
        {
            add { AddHandler(AboutClickEvent, value); }
            remove { RemoveHandler(AboutClickEvent, value); }
        }

        public void RaiseAboutClickEvent()
        {
            RaiseEvent(new RoutedEventArgs(AboutClickEvent));
        } 

        public void OnAboutClick()
        {
            RaiseAboutClickEvent();
        } 

        #endregion

        #endregion

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            OnEditClick();
        }

        private void about_Click(object sender, RoutedEventArgs e)
        {
            OnAboutClick();
        }
    }
}
