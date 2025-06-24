using GIBS.Module.RestaurantMenu.Migrations.EntityBuilders;
using GIBS.Module.RestaurantMenu.Repository;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;


namespace GIBS.Module.RestaurantMenu.Server.Migrations
{
    [DbContext(typeof(RestaurantMenuContext))]
    [Migration("GIBS.Module.RestaurantMenu.01.00.09.00")]
    public class AddMenuItem : MultiDatabaseMigration
    {
        public AddMenuItem(IDatabase database) : base(database)
        {
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var menuItemEntityBuilder = new MenuItemEntityBuilder(migrationBuilder, ActiveDatabase);
            menuItemEntityBuilder.Create();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var menuItemEntityBuilder = new MenuItemEntityBuilder(migrationBuilder, ActiveDatabase);
            menuItemEntityBuilder.Drop();
        }
    }
}
