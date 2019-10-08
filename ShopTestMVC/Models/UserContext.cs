using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTestMVC
{
    public class UserContext : DbContext
    {
        public UserContext(): base("DbContext")
        {
            //to do
        }

        public DbSet<Users> UsersData { get; set; }
        public DbSet<Relatives> RelativesData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
