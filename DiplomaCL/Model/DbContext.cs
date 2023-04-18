using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DiplomaCL.Model
{
    public class DipDbContext : DbContext  
    {
        public DipDbContext() : base("DipConnection") { }
        public DbSet<User> Users { get; set; }
    }
}
