using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.RestaurantMenu.Migrations.EntityBuilders
{
    public class RestaurantMenuEntityBuilder : AuditableBaseEntityBuilder<RestaurantMenuEntityBuilder>
    {
        private const string _entityTableName = "GIBSRestaurantMenu";
        private readonly PrimaryKey<RestaurantMenuEntityBuilder> _primaryKey = new("PK_GIBSRestaurantMenu", x => x.RestaurantMenuId);
        private readonly ForeignKey<RestaurantMenuEntityBuilder> _moduleForeignKey = new("FK_GIBSRestaurantMenu_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public RestaurantMenuEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override RestaurantMenuEntityBuilder BuildTable(ColumnsBuilder table)
        {
            RestaurantMenuId = AddAutoIncrementColumn(table,"RestaurantMenuId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            Description = table.Column<string>(name: "Description", maxLength: int.MaxValue, nullable: true);
            ImageURL = table.Column<string>(name: "ImageURL", maxLength: int.MaxValue, nullable: true);
            HoursServed = table.Column<string>(name: "HoursServed", maxLength: 256, nullable: true);
            SortOrder = table.Column<int>(name: "SortOrder", nullable: false, defaultValue: 0);
            IsActive = table.Column<bool>(name: "IsActive", nullable: false, defaultValue: true);
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> RestaurantMenuId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
        public OperationBuilder<AddColumnOperation> Description { get; set; }
        public OperationBuilder<AddColumnOperation> ImageURL { get; set; }
        public OperationBuilder<AddColumnOperation> HoursServed { get; set; }
        public OperationBuilder<AddColumnOperation> SortOrder { get; set; }
        public OperationBuilder<AddColumnOperation> IsActive { get; set; }
    }
}
