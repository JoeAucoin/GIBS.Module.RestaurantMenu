using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.RestaurantMenu.Migrations.EntityBuilders
{
    //internal class MenuItemAttributeEntityBuilder
    public class MenuItemAttributeEntityBuilder : AuditableBaseEntityBuilder<MenuItemAttributeEntityBuilder>
    {



        private const string _entityTableName = "GIBSMenuItemAttribute";
        private readonly PrimaryKey<MenuItemAttributeEntityBuilder> _primaryKey = new("PK_GIBSMenuItemAttribute", x => x.MenuItemAttributeId);
        private readonly ForeignKey<MenuItemAttributeEntityBuilder> _menuItemForeignKey = new("FK_GIBSMenuItemAttribute_MenuItem", x => x.ItemId, "GIBSMenuItem", "ItemId", ReferentialAction.Cascade);
        public MenuItemAttributeEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_menuItemForeignKey);
        }
        protected override MenuItemAttributeEntityBuilder BuildTable(ColumnsBuilder table)
        {
            MenuItemAttributeId = AddAutoIncrementColumn(table, "MenuItemAttributeId");
            ItemId = AddIntegerColumn(table, "ItemId");
            AttributeId = AddIntegerColumn(table, "AttributeId");
            AddAuditableColumns(table);
            return this;
        }
        public OperationBuilder<AddColumnOperation> MenuItemAttributeId { get; set; }
        public OperationBuilder<AddColumnOperation> ItemId { get; set; }
        public OperationBuilder<AddColumnOperation> AttributeId { get; set; }

    }
}
