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
using DiplomaUI;
using Xceed.Wpf.Toolkit;

namespace DiplomaUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void OnRegistrationClick(object sender, RoutedEventArgs e)
        {
            var registrationWindow = new RegistrationWindow().ShowDialog();
        }

        private void OnAuthentificationClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
