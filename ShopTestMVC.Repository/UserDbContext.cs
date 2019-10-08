using System;
using System.Data.Entity;
using ShopTestMVC.Model;
using System.Data.Entity.Validation;
using System.Data.SqlClient;

namespace ShopTestMVC.Repository
{
   public  class UserDbContext : DbContext
    {
        public UserDbContext() : base("DbContext")
        {
            try
            {
                Database.SetInitializer(new DropCreateDatabaseIfModelChanges<UserDbContext>());
            }
            catch(Exception ex)
            {
                if (ex.GetBaseException() is SqlException)
                {
                   // to do
                }
                if (ex.GetBaseException() is DbEntityValidationException)
                {
                  //to do
                }
            }
        }

        public DbSet<Users> UsersData { get; set; }
        public DbSet<Relatives> RelativesData { get; set; }


    }
}
