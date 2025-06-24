using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.RestaurantMenu.Migrations.EntityBuilders
{
    public class MenuCategoryEntityBuilder : AuditableBaseEntityBuilder<MenuCategoryEntityBuilder>
    {
        private const string _entityTableName = "GIBSMenuCategory";
        private readonly PrimaryKey<MenuCategoryEntityBuilder> _primaryKey = new("PK_GIBSMenuCategory", x => x.CategoryId);
        private readonly ForeignKey<MenuCategoryEntityBuilder> _moduleForeignKey = new("FK_GIBSMenuCategory_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);
      //  private readonly ForeignKey<MenuCategoryEntityBuilder> _menuidForeignKey = new("FK_GIBSMenuCategory_Menu", x => x.RestaurantMenuId, "GIBSRestaurantMenu", "RestaurantMenuId", ReferentialAction.Cascade);


        public MenuCategoryEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
         //   ForeignKeys.Add(_menuidForeignKey);
        }

        protected override MenuCategoryEntityBuilder BuildTable(ColumnsBuilder table)
        {
            CategoryId = AddAutoIncrementColumn(table, "CategoryId");
            ModuleId = AddIntegerColumn(table, "ModuleId");
            CategoryName = AddMaxStringColumn(table, "CategoryName");
            CategoryDescription = table.Column<string>(name: "CategoryDescription", maxLength: int.MaxValue, nullable: true);
            CategoryIcon = table.Column<string>(name: "CategoryIcon", maxLength: int.MaxValue, nullable: true);
            RestaurantMenuId = AddIntegerColumn(table, "RestaurantMenuId");
            SortOrder = table.Column<int>(name: "SortOrder", nullable: false, defaultValue: 0);
            IsActive = table.Column<bool>(name: "IsActive", nullable: false, defaultValue: true);
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> CategoryId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> CategoryName { get; set; }
        public OperationBuilder<AddColumnOperation> CategoryDescription { get; set; }
        public OperationBuilder<AddColumnOperation> CategoryIcon { get; set; }
        public OperationBuilder<AddColumnOperation> RestaurantMenuId { get; set; }
        public OperationBuilder<AddColumnOperation> SortOrder { get; set; }
        public OperationBuilder<AddColumnOperation> IsActive { get; set; }
    }
}
