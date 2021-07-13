using DCBMS_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCBMS_API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Dedails About Under Method-https://entityframework.net/knowledge-base/39798317/identityuserlogin-string---requires-a-primary-key-to-be-defined-error-while-adding-migration
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });


            // https://eamonkeane.dev/computed-columns-in-entity-framework-core/
            // Create Full Name with Is Persisted YES
            //builder.Entity<Student>()
            //    .Property(s => s.FullName)
            //    .HasComputedColumnSql("[FirstName] + ' ' + [LastName]PERSISTED");


            //Note-https://www.learnentityframeworkcore.com/configuration/fluent-api/ondelete-method

            //builder.Entity<Staff>()
            //   .HasOne(b => b.PostOffice)
            //   .WithMany(a => a.Staff)
            //   .OnDelete(DeleteBehavior.Restrict);

   

        }

        public virtual DbSet<TestType> TestTypes { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<ApplicationRole> ApplicationRole { get; set; }
        public virtual DbSet<ApplicationUserRole> ApplicationUserRole { get; set; }
    }
}

