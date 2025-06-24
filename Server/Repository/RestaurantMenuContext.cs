using GIBS.Module.RestaurantMenu.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Oqtane.Infrastructure;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Repository.Databases.Interfaces;

namespace GIBS.Module.RestaurantMenu.Repository
{
    public class RestaurantMenuContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.RestaurantMenu> RestaurantMenu { get; set; }
        public virtual DbSet<Models.MenuCategory> MenuCategory { get; set; }
        public virtual DbSet<Models.MenuAttribute> MenuAttribute { get; set; }
        public virtual DbSet<Models.MenuItem> MenuItem { get; set; }
        public virtual DbSet<Models.MenuItemAttribute> MenuItemAttribute { get; set; }
        public virtual DbSet<MenuOption> MenuOption { get; set; }

        public RestaurantMenuContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Map models to table names
            builder.Entity<Models.RestaurantMenu>().ToTable(ActiveDatabase.RewriteName("GIBSRestaurantMenu"));
            builder.Entity<Models.MenuCategory>().ToTable(ActiveDatabase.RewriteName("GIBSMenuCategory"));
            builder.Entity<Models.MenuAttribute>().ToTable(ActiveDatabase.RewriteName("GIBSMenuAttribute"));
            builder.Entity<Models.MenuItem>().ToTable(ActiveDatabase.RewriteName("GIBSMenuItem"));
            builder.Entity<Models.MenuItemAttribute>().ToTable(ActiveDatabase.RewriteName("GIBSMenuItemAttribute"));
            builder.Entity<Models.MenuOption>().ToTable(ActiveDatabase.RewriteName("GIBSMenuOption"));

            // MenuItem -> MenuCategory (many-to-one)
            builder.Entity<Models.MenuItem>()
               .HasOne(i => i.MenuCategory)
               .WithMany(c => c.MenuItems)
               .HasForeignKey(i => i.CategoryId)
               .OnDelete(DeleteBehavior.NoAction);

            // MenuItem -> RestaurantMenu (many-to-one)
            builder.Entity<Models.MenuItem>()
                .HasOne(i => i.RestaurantMenu)
                .WithMany(m => m.MenuItems)
                .HasForeignKey(i => i.RestaurantMenuId)
                .OnDelete(DeleteBehavior.Cascade);

            // MenuCategory -> RestaurantMenu (many-to-one)
            builder.Entity<Models.MenuCategory>()
                .HasOne(c => c.RestaurantMenu)
                .WithMany(m => m.MenuCategories)
                .HasForeignKey(c => c.RestaurantMenuId)
                .OnDelete(DeleteBehavior.Cascade);

            // MenuItem <-> MenuItemAttribute (one-to-many, explicit)
            builder.Entity<MenuItemAttribute>()
                .HasOne(mia => mia.MenuItem)
                .WithMany(mi => mi.MenuItemAttributes)
                .HasForeignKey(mia => mia.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // MenuItemAttribute -> MenuAttribute (many-to-one)
            builder.Entity<MenuItemAttribute>()
                .HasOne(mia => mia.MenuAttribute)
                .WithMany()
                .HasForeignKey(mia => mia.AttributeId)
                .OnDelete(DeleteBehavior.Cascade);

            // MenuOption -> MenuItem (many-to-one)
            builder.Entity<MenuOption>()
                .HasOne(option => option.MenuItem)
                .WithMany(item => item.MenuOptions)
                .HasForeignKey(option => option.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}