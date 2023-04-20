using Microsoft.Win32;
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
using DiplomaCL.Model;
using System.IO;

namespace DiplomaUI
{
    /// <summary>
    /// Логика взаимодействия для PartitureForm.xaml
    /// </summary>
    public partial class PartitureForm : Window
    {

        DipDbContext db;
        Partiture p; 

        public PartitureForm()
        {
            p = new Partiture();
            db = new DipDbContext();
            InitializeComponent();
        }

        private void OnLoadingFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF-файл (*.pdf)|*.pdf";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true)
            {
                p.File = File.ReadAllBytes(openFileDialog.FileName);
                var button = sender as Button;
                button.Content = openFileDialog.FileName;
            }
        }

        private void OnApplyingChangesClick (object sender, RoutedEventArgs e)
        {
            p.Name = NameTB.Text;
            p.Author = AuthorTB.Text;
            p.WorkType = WorkTypeTB.Text;
            p.Instrumentation = InstrumentationTB.Text;
            p.Language = LanguageTB.Text;
            db.Partitures.Add(p);
            db.SaveChanges();
        }
    }
}
