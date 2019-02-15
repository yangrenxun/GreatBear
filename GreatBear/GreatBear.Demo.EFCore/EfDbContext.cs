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
    public class EfDbContext : DbContextBase
    {
        private readonly string _id;

        public EfDbContext(DbContextOptions<EfDbContext> options)
            : base(options)
        {
            _id = Guid.NewGuid().ToString();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
           .Where(type => !String.IsNullOrEmpty(type.Namespace))
           .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
            //...or do it manually below. For example,
            //modelBuilder.Configurations.Add(new LanguageMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string str = "data source=.; Initial Catalog=GreatBear ; uid=sa; pwd=123456";
            optionsBuilder.UseSqlServer(str);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
