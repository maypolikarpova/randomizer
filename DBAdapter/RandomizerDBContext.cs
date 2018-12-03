using Randomizer.DBAdapter.Migrations;
using Randomizer.DBModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randomizer.DBAdapter
{
    class RandomizerDBContext : DbContext
    {

        static RandomizerDBContext()
        {
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
            if (type == null)
                throw new Exception("Do not remove, ensures static reference to System.Data.Entity.SqlServer");
        }

        public RandomizerDBContext() : base("NewRandomizerDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RandomizerDBContext, Configuration>(true));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new User.UserEntityConfiguration());
            modelBuilder.Configurations.Add(new Request.RequestEntityConfiguration());
        }
    }
}
