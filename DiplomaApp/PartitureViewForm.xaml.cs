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

        private void OnPartitureViewLoaded(object sender, RoutedEventArgs e)
        {
            var k = 0;
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            OnDeleteClick();
        }

        #region RoutedEvent

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

    }
}
