
using GIBS.Module.RestaurantMenu.Migrations.EntityBuilders;
using GIBS.Module.RestaurantMenu.Repository;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;

namespace GIBS.Module.RestaurantMenu.Migrations
{
    [DbContext(typeof(RestaurantMenuContext))]
    [Migration("GIBS.Module.RestaurantMenu.01.00.08.00")]
    public class AddMenuAttribute : MultiDatabaseMigration // Renamed class to 'InitializeModuleMigration'  
    {
        public AddMenuAttribute(IDatabase database) : base(database)
        {
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var attribureEntityBuilder = new MenuAttributeEntityBuilder(migrationBuilder, ActiveDatabase);
            attribureEntityBuilder.Create();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var attribureEntityBuilder = new MenuAttributeEntityBuilder(migrationBuilder, ActiveDatabase);
            attribureEntityBuilder.Drop();
        }
    }
}
