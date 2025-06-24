using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.RestaurantMenu.Migrations.EntityBuilders
{
    public class MenuItemEntityBuilder : AuditableBaseEntityBuilder<MenuItemEntityBuilder>
    {
        private const string _entityTableName = "GIBSMenuItem";
        private readonly PrimaryKey<MenuItemEntityBuilder> _primaryKey = new("PK_GIBSMenuItem", x => x.ItemId);
        private readonly ForeignKey<MenuItemEntityBuilder> _moduleForeignKey = new("FK_GIBSMenuItem_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);
        private readonly ForeignKey<MenuItemEntityBuilder> _restaurantMenuForeignKey = new("FK_GIBSMenuItem_RestaurantMenu", x => x.RestaurantMenuId, "GIBSRestaurantMenu", "RestaurantMenuId", ReferentialAction.NoAction);
        private readonly ForeignKey<MenuItemEntityBuilder> _categoryForeignKey = new("FK_GIBSMenuItem_MenuCategory", x => x.CategoryId, "GIBSMenuCategory", "CategoryId", ReferentialAction.NoAction);

        public MenuItemEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
            ForeignKeys.Add(_restaurantMenuForeignKey);
            ForeignKeys.Add(_categoryForeignKey);
        }

        protected override MenuItemEntityBuilder BuildTable(ColumnsBuilder table)
        {
            ItemId = AddAutoIncrementColumn(table, "ItemId");
            ModuleId = AddIntegerColumn(table, "ModuleId");
            RestaurantMenuId = AddIntegerColumn(table, "RestaurantMenuId");
            CategoryId = AddIntegerColumn(table, "CategoryId");
            ItemName = AddMaxStringColumn(table, "ItemName");
            ItemDescription = table.Column<string>(name: "ItemDescription", maxLength: int.MaxValue, nullable: true);
            ItemImageURL = table.Column<string>(name: "ItemImageURL", maxLength: int.MaxValue, nullable: true);
            ItemPrice = table.Column<decimal>(name: "ItemPrice", nullable: false, defaultValue: 0m);
            PriceNote = table.Column<string>(name: "PriceNote", maxLength: 256, nullable: true);
            Calories = table.Column<decimal>(name: "Calories", nullable: false, defaultValue: 0m);
            Protein = table.Column<decimal>(name: "Protein", nullable: false, defaultValue: 0m);
            Fat = table.Column<decimal>(name: "Fat", nullable: false, defaultValue: 0m);
            Carbohydrates = table.Column<decimal>(name: "Carbohydrates", nullable: false, defaultValue: 0m);
            Sodium = table.Column<decimal>(name: "Sodium", nullable: false, defaultValue: 0m);
            SortOrder = table.Column<int>(name: "SortOrder", nullable: false, defaultValue: 0);
            IsNewItem = table.Column<bool>(name: "IsNewItem", nullable: false, defaultValue: false);
            IsActive = table.Column<bool>(name: "IsActive", nullable: false, defaultValue: true);
            AddAuditableColumns(table);
            return this;
        }
        public OperationBuilder<AddColumnOperation> ItemId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> RestaurantMenuId { get; set; }
        public OperationBuilder<AddColumnOperation> CategoryId { get; set; }
        public OperationBuilder<AddColumnOperation> ItemName { get; set; }
        public OperationBuilder<AddColumnOperation> ItemDescription { get; set; }
        public OperationBuilder<AddColumnOperation> ItemImageURL { get; set; }
        public OperationBuilder<AddColumnOperation> ItemPrice { get; set; }
        public OperationBuilder<AddColumnOperation> PriceNote { get; set; }
        public OperationBuilder<AddColumnOperation> Calories { get; set; }
        public OperationBuilder<AddColumnOperation> Protein { get; set; }
        public OperationBuilder<AddColumnOperation> Fat { get; set; }
        public OperationBuilder<AddColumnOperation> Carbohydrates { get; set; }
        public OperationBuilder<AddColumnOperation> Sodium { get; set; }
        public OperationBuilder<AddColumnOperation> SortOrder { get; set; }
        public OperationBuilder<AddColumnOperation> IsNewItem { get; set; }
        public OperationBuilder<AddColumnOperation> IsActive { get; set; }
    }
}
