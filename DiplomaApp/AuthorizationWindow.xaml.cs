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
using DiplomaCL.Model;
using System.Data.Entity;

namespace DiplomaUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        DipDbContext db;  

        public AuthorizationWindow()
        {
            InitializeComponent();
            db = new DipDbContext();
            db.Users.LoadAsync();
        }

        private void OnRegistrationClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var registrationWindow = new RegistrationWindow(db).ShowDialog();
            this.Show();
            if (registrationWindow == true) MessageBox.Show("Регистрация прошла успешно!");
        }

        private void OnAuthentificationClick(object sender, RoutedEventArgs e)
        {
            var authUser = db.Users.Where(x => x.Login == loginBox.Text && x.Password == pwdBox.Password).Count();
            if (authUser == 1)
            {
                this.Hide();
                var mainWindow = new MainWindow().ShowDialog();
            }
            else errorMessageLabel.Visibility = Visibility.Visible;
        }
    }
}
