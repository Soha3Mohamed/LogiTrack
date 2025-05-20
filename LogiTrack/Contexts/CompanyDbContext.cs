using LogiTrack.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiTrack.Contexts
{
    internal class CompanyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-7A6I7DSO\\SQL2022;Database = LTDatabase ; Integrated Security=True;Encrypt=False;Trust Server Certificate=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Configuring m2m between employee and project explicitly
            modelBuilder.Entity<EmployeeProject>()
                           .HasKey(EP => new { EP.EmployeeId, EP.ProjectId });


            modelBuilder.Entity<EmployeeProject>()
                        .HasOne(E => E.Employee)
                        .WithMany(Ep => Ep.EmployeeProjects)
                        .HasForeignKey(Ep => Ep.EmployeeId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EmployeeProject>()
                        .HasOne(P => P.Project)
                        .WithMany(Ep => Ep.EmployeeProjects)
                        .HasForeignKey(Ep => Ep.ProjectId)
                        .OnDelete(DeleteBehavior.NoAction);
            #endregion


            modelBuilder.Entity<Project>()
                        .HasOne(p => p.Manager)
                        .WithMany()
                        .HasForeignKey(p => p.ManagerId)
                        .OnDelete(DeleteBehavior.Restrict);
                     
            
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

    }
}
