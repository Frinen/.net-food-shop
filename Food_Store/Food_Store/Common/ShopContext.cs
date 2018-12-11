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
        public DbSet<UsersItems> UsersItems { get; set; }
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
            Role adminRole = new Role { Id =  new Guid("3e134d8d-b208-4a36-a4b6-732d06756907"), Name = adminRoleName };
            Role userRole = new Role { Id = new Guid("dbd077aa-7215-4b8f-a2a3-5feca43817fb"), Name = userRoleName };
            User adminUser = new User { Id = new Guid("5334cf34-29aa-412b-8a1e-0a8d6c488cc8"), Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemType>().HasKey(t => new { t.ItemId, t.ItTypeId});
            modelBuilder.Entity<UsersItems>().HasKey(t => new { t.ItemId, t.UserId});

            modelBuilder.Entity<ItemType>()
            .HasOne(pt => pt.Item)
            .WithMany(t => t.ItemType)
            .HasForeignKey(pt => pt.ItemId);

            modelBuilder.Entity<ItemType>()
            .HasOne(pt => pt.Type)
            .WithMany(t => t.ItemType)
            .HasForeignKey(pt => pt.ItTypeId);

            //---------------------------------

            modelBuilder.Entity<UsersItems>()
            .HasOne(pt => pt.Item)
            .WithMany(t => t.UsersItems)
            .HasForeignKey(pt => pt.ItemId);

            modelBuilder.Entity<UsersItems>()
            .HasOne(pt => pt.User)
            .WithMany(t => t.UsersItems)
            .HasForeignKey(pt => pt.UserId);

        }

        internal Guid FirstOrDefault()
        {
            throw new NotImplementedException();
        }
    }
}
