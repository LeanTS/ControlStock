using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BE_Datos
{
    public class AplicationDbContext: DbContext
    {
        public AplicationDbContext()
            : base(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lean\BE_Datos.AplicationDbContext.mdf;Integrated Security=True;Connect Timeout=30")
        {
        }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }

    }
}
