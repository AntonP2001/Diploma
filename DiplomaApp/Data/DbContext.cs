using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DiplomaUI.Model;

namespace DiplomaUI.Data
{
    public class DipDbContext : DbContext  
    {
        public DipDbContext() : base("DipConnection") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Partiture> Partitures { get; set; }  
        public DbSet<Catalogue> Catalogues { get; set; }
    }
}
