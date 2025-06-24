using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.RestaurantMenu.Migrations.EntityBuilders
{
    public class MenuOptionEntityBuilder : AuditableBaseEntityBuilder<MenuOptionEntityBuilder>
    {
        private const string _entityTableName = "GIBSMenuOption";
        private readonly PrimaryKey<MenuOptionEntityBuilder> _primaryKey = new("PK_MenuOption", x => x.OptionId);
        private readonly ForeignKey<MenuOptionEntityBuilder> _itemForeignKey = new("FK_GIBSMenuOption_GIBSMenuItem", x => x.ItemId, "GIBSMenuItem", "ItemId", ReferentialAction.NoAction);

        public MenuOptionEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_itemForeignKey);
        }

        protected override MenuOptionEntityBuilder BuildTable(ColumnsBuilder table)
        {
            OptionId = AddAutoIncrementColumn(table, "OptionId");
            OptionName = AddMaxStringColumn(table, "OptionName", false);
            OptionPrice = AddDecimalColumn(table, "OptionPrice", 18, 2, true);
            OptionImage = AddMaxStringColumn(table, "OptionImage", true);
            MOCalories = AddIntegerColumn(table, "MOCalories", true);
            MOProtein = AddDecimalColumn(table, "MOProtein", 18, 2, true);
            MOFat = AddDecimalColumn(table, "MOFat", 18, 2, true);
            MOCarbohydrates = AddDecimalColumn(table, "MOCarbohydrates", 18, 2, true);
            MOSodium = AddDecimalColumn(table, "MOSodium", 18, 2, true);
            ItemId = AddIntegerColumn(table, "ItemId");
            IsActive = AddBooleanColumn(table, "IsActive", false, true); // Default ((1))
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> OptionId { get; set; }
        public OperationBuilder<AddColumnOperation> OptionName { get; set; }
        public OperationBuilder<AddColumnOperation> OptionPrice { get; set; }
        public OperationBuilder<AddColumnOperation> OptionImage { get; set; }
        public OperationBuilder<AddColumnOperation> MOCalories { get; set; }
        public OperationBuilder<AddColumnOperation> MOProtein { get; set; }
        public OperationBuilder<AddColumnOperation> MOFat { get; set; }
        public OperationBuilder<AddColumnOperation> MOCarbohydrates { get; set; }
        public OperationBuilder<AddColumnOperation> MOSodium { get; set; }
        public OperationBuilder<AddColumnOperation> ItemId { get; set; }
        public OperationBuilder<AddColumnOperation> IsActive { get; set; }
    }
}