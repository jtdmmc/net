using DotnetSoftwarePlatform.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.DAL.EF
{
    public class DotnetSoftwarePlatformContext : DbContext
    {
        private static string _sqlConnection = null;

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Power> Powers { get; set; }
        public virtual DbSet<RolePower> RolePowers { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        public DotnetSoftwarePlatformContext()
        {
        }

        public DotnetSoftwarePlatformContext(string sqlConnection) 
        {
            _sqlConnection = sqlConnection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(string.IsNullOrEmpty(_sqlConnection))
            {
                optionsBuilder.UseMySql("server=localhost;port=3306;database=MVI.DSP;user=root;password=123456", new MySqlServerVersion(new Version(5, 7, 21)));
            }
            else
            {
                optionsBuilder.UseMySql(_sqlConnection, new MySqlServerVersion(new Version(5, 7, 21)));
            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(x => x.UserName).IsUnique();
            modelBuilder.Entity<RolePower>().HasKey(rp => new { rp.PowerId, rp.RoleId });

            modelBuilder.Entity<RolePower>()
                .HasOne<Role>(rp => rp.Role)
                .WithMany(r => r.RolePowers)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne<User>(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

        }

        /// <summary>
        /// 重载，以便自动更新CreatedAt和UpdatedAt时戳
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }


        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is EntityBase &&
                (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    var createdAt = DateTime.UtcNow;
                    ((EntityBase)entity.Entity).CreatedAt = createdAt;
                    ((EntityBase)entity.Entity).UpdatedAt = createdAt;
                }
                else if (entity.State == EntityState.Modified)
                {
                    ((EntityBase)entity.Entity).UpdatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
