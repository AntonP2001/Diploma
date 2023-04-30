using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaCL.Model
{
    public class User
    {
        public User()
        {
            FullName = null;
            CourseNum = 0;
            Login = null;
            Password = null;
            AccessLevel = 2;
        }
        public int Id { get; set; }
        public string FullName { get; set; }
        public int CourseNum { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int AccessLevel { get; set; }
    }
}
