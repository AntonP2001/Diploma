using DiplomaCL.Model;
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

namespace DiplomaUI
{
    /// <summary>
    /// Логика взаимодействия для CtaloguePasswordEnteringWindow.xaml
    /// </summary>
    public partial class CataloguePasswordEnteringWindow : Window
    {
        public CataloguePasswordEnteringWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Catalogue catalogue = this.DataContext as Catalogue;
            if (pwdTB.Text == catalogue.Password)
                this.DialogResult = true;
            this.Close();
        }
    }
}
