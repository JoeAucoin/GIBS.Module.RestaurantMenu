using GIBS.Module.RestaurantMenu.Migrations.EntityBuilders;
using GIBS.Module.RestaurantMenu.Repository;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;

namespace GIBS.Module.RestaurantMenu.Migrations
{
    [DbContext(typeof(RestaurantMenuContext))]
    [Migration("GIBS.Module.RestaurantMenu.01.00.10.00")]

    public class AddMenuItemAttribute : MultiDatabaseMigration // Renamed class to 'AddMenuItemAttribute'  
    {
        public AddMenuItemAttribute(IDatabase database) : base(database)
        {
        }
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var entityBuilder = new MenuItemAttributeEntityBuilder(migrationBuilder, ActiveDatabase);
            entityBuilder.Create();
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var entityBuilder = new MenuItemAttributeEntityBuilder(migrationBuilder, ActiveDatabase);
            entityBuilder.Drop();
        }
    }
    
}
