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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using DiplomaUI;

namespace DiplomaUI.View
{
    /// <summary>
    /// Логика взаимодействия для PartitureForm.xaml
    /// </summary>
    public partial class PartitureFormView : UserControl
    {
        public PartitureFormView()
        {
            InitializeComponent();
        }

        //private void OnDeletingRecordClick(object sender, RoutedEventArgs e)
        //{
        //    db.Entry(this.DataContext).State = EntityState.Deleted;
        //    db.SaveChanges();
        //}

        //private void OnEditingRecordClick(object sender, RoutedEventArgs e)
        //{
        //    PartitureForm partitureForm = new PartitureForm(this.DataContext as Partiture);
        //    if(partitureForm.ShowDialog() == true)
        //    {
        //        //db.Entry(this.DataContext).State = EntityState.Modified;
        //        //db.SaveChanges();
        //    }
        //}
    }
}
