
using GIBS.Module.RestaurantMenu.Migrations.EntityBuilders;
using GIBS.Module.RestaurantMenu.Repository;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;

namespace GIBS.Module.RestaurantMenu.Migrations
{
    [DbContext(typeof(RestaurantMenuContext))]
    [Migration("GIBS.Module.RestaurantMenu.01.00.04.00")]
    public class AddMenuCategory : MultiDatabaseMigration // Renamed class to 'InitializeModuleMigration'  
    {
        public AddMenuCategory(IDatabase database) : base(database)
        {
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var categoryentityBuilder = new MenuCategoryEntityBuilder(migrationBuilder, ActiveDatabase);
            categoryentityBuilder.Create();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var categoryentityBuilder = new MenuCategoryEntityBuilder(migrationBuilder, ActiveDatabase);
            categoryentityBuilder.Drop();

        }
    }
}
