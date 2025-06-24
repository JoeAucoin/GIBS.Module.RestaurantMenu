using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using GIBS.Module.RestaurantMenu.Migrations.EntityBuilders;
using GIBS.Module.RestaurantMenu.Repository;

namespace GIBS.Module.RestaurantMenu.Migrations
{
    [DbContext(typeof(RestaurantMenuContext))]
    [Migration("GIBS.Module.RestaurantMenu.01.00.00.00")]
    public class InitializeModule : MultiDatabaseMigration
    {
        public InitializeModule(IDatabase database) : base(database)
        {
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var entityBuilder = new RestaurantMenuEntityBuilder(migrationBuilder, ActiveDatabase);
            entityBuilder.Create();

            //var categoryentityBuilder = new MenuCategoryEntityBuilder(migrationBuilder, ActiveDatabase);
            //categoryentityBuilder.Create();

            //var attribureEntityBuilder = new MenuAttributeEntityBuilder(migrationBuilder, ActiveDatabase);
            //attribureEntityBuilder.Create();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            var entityBuilder = new RestaurantMenuEntityBuilder(migrationBuilder, ActiveDatabase);
            entityBuilder.Drop();

            //var categoryentityBuilder = new MenuCategoryEntityBuilder(migrationBuilder, ActiveDatabase);
            //categoryentityBuilder.Drop();

            //var attribureEntityBuilder = new MenuAttributeEntityBuilder(migrationBuilder, ActiveDatabase);
            //attribureEntityBuilder.Drop();
        }
    }
}
