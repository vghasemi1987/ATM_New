using DomainEntities.ApplicationUserAggregate;
using DomainEntities.TransactionFileAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Data.Commons
{
    public class AtmContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, IdentityUserClaim<int>,
        ApplicationUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        //private readonly int _user;
        public AtmContext(DbContextOptions<AtmContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
            //_user = userService.GetUser();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                         .Where(t => t.GetInterfaces().Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                builder.ApplyConfiguration(configurationInstance);
            }
            //view for excel export
            builder.Entity<TransactionToExelAll>().ToView("VW_TransactionToExelAll");

            builder.Entity<IdentityUserClaim<int>>(entity => entity.ToTable("ApplicationUser_Claims"));
            builder.Entity<IdentityUserLogin<int>>(entity => entity.ToTable("ApplicationUser_Logins"));
            builder.Entity<IdentityUserToken<int>>(entity => entity.ToTable("ApplicationUser_Tokens"));
        }
    }
}