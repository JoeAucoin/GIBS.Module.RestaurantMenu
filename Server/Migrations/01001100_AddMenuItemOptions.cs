using GIBS.Module.RestaurantMenu.Migrations.EntityBuilders;
using GIBS.Module.RestaurantMenu.Repository;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;

namespace GIBS.Module.RestaurantMenu.Migrations
{
    [DbContext(typeof(RestaurantMenuContext))]
    [Migration("GIBS.Module.RestaurantMenu.01.00.11.00")]
    public class AddMenuItemOptions : MultiDatabaseMigration
    {
        public AddMenuItemOptions(IDatabase database) : base(database)
        {
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var entityBuilder = new MenuOptionEntityBuilder(migrationBuilder, ActiveDatabase);
            entityBuilder.Create();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var entityBuilder = new MenuOptionEntityBuilder(migrationBuilder, ActiveDatabase);
            entityBuilder.Drop();
        }
    }
}