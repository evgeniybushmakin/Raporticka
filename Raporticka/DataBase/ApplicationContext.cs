using Raporticka.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raporticka.DataBase
{

    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection") { }

        public DbSet<Group> Groups { set; get; }
        public DbSet<Raport> Raports { set; get; }
        public DbSet<Status> Statuses { set; get; }
        public DbSet<Student> Students { set; get; }
    }
}