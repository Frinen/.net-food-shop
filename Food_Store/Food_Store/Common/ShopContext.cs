using Food_Store.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Food_Store.Common
{
    public class ShopContext : DbContext
    {
        public DbSet<Item> Items{ get; set; }
        public DbSet<ItType> Types { get; set; }
        public DbSet<ItemType> itemTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public ShopContext(DbContextOptions<ShopContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";

            // добавляем роли
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemType>().HasKey(t => new { t.ItemId, t.ItTypeId});

            modelBuilder.Entity<ItemType>()
            .HasOne(pt => pt.Item)
            .WithMany(t => t.ItemType)
            .HasForeignKey(pt => pt.ItemId);

            modelBuilder.Entity<ItemType>()
            .HasOne(pt => pt.Type)
            .WithMany(t => t.ItemType)
            .HasForeignKey(pt => pt.ItTypeId);
        }

        internal Guid FirstOrDefault()
        {
            throw new NotImplementedException();
        }
    }
}
