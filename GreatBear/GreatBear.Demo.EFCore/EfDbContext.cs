using GreatBear.Demo.EFCore.Mapping;
using GreatBear.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace GreatBear.Demo.EFCore
{
    /// <summary>
    /// Data access context
    /// </summary>
    public class EfDbContext : DbContext
    {
        public EfDbContext(DbContextOptions<EfDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(type=> type.GetInterfaces().Any(i=> i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
            foreach (var type in types)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
            //...or do it manually below. For example,
            //modelBuilder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string str = "data source=.;Initial Catalog=GreatBear;uid=sa;pwd=123456";
            optionsBuilder.UseSqlServer(str);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
