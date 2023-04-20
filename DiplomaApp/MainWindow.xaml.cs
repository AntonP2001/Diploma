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

namespace DiplomaUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DipDbContext db;

        public MainWindow()
        {
            db = new DipDbContext();
            db.Partitures.Load();
            InitializeComponent();
            List<PartitureViewForm> forms = new List<PartitureViewForm>()
            { 
                new PartitureViewForm(),
                new PartitureViewForm(),
                new PartitureViewForm()
            }; 
            foreach (var form in forms) partiturePanel.Children.Add(form);
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
    }
}
