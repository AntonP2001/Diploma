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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        DipDbContext db;

        public RegistrationWindow()
        {
            InitializeComponent();
        }

        public RegistrationWindow(DipDbContext db) : this()
        {
            this.db = db;
        }

        private void OnRegistrationButtonClick(object sender, RoutedEventArgs e)
        {
            User u = new User()
            {
                FullName = FullNameTB.Text,
                CourseNum = Convert.ToInt32(CourseIUD.Text),
                Login = LoginTB.Text,
                Password = PasswordTB.Text,
            };
            db.Users.Add(u);
            db.SaveChanges();
            this.DialogResult = true;
            this.Close();
        }
    }
}
