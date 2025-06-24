using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.RestaurantMenu.Migrations.EntityBuilders
{
    public class MenuAttributeEntityBuilder : AuditableBaseEntityBuilder<MenuAttributeEntityBuilder>
    {
        private const string _entityTableName = "GIBSMenuAttribute";
        private readonly PrimaryKey<MenuAttributeEntityBuilder> _primaryKey = new("PK_GIBSMenuAttribute", x => x.AttributeId);
        private readonly ForeignKey<MenuAttributeEntityBuilder> _moduleForeignKey = new("FK_GIBSMenuAttribute_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);
        //  private readonly ForeignKey<MenuCategoryEntityBuilder> _menuidForeignKey = new("FK_GIBSMenuCategory_Menu", x => x.RestaurantMenuId, "GIBSRestaurantMenu", "RestaurantMenuId", ReferentialAction.Cascade);


        public MenuAttributeEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
            //   ForeignKeys.Add(_menuidForeignKey);
        }

        protected override MenuAttributeEntityBuilder BuildTable(ColumnsBuilder table)
        {
            AttributeId = AddAutoIncrementColumn(table, "AttributeId");
            ModuleId = AddIntegerColumn(table, "ModuleId");
            AttributeName = AddMaxStringColumn(table, "AttributeName");
            AttributeDescription = table.Column<string>(name: "AttributeDescription", maxLength: int.MaxValue, nullable: true);
            AttributeIcon = table.Column<string>(name: "AttributeIcon", maxLength: int.MaxValue, nullable: true);
            AttributeCode = AddStringColumn(table, "AttributeCode", 2);
            AttributeColor = AddStringColumn(table, "AttributeColor", 7);
            SortOrder = table.Column<int>(name: "SortOrder", nullable: false, defaultValue: 0);
            IsActive = table.Column<bool>(name: "IsActive", nullable: false, defaultValue: true);
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> AttributeId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> AttributeName { get; set; }
        public OperationBuilder<AddColumnOperation> AttributeDescription { get; set; }
        public OperationBuilder<AddColumnOperation> AttributeIcon { get; set; }
        public OperationBuilder<AddColumnOperation> AttributeCode { get; set; }
        public OperationBuilder<AddColumnOperation> AttributeColor { get; set; }
        public OperationBuilder<AddColumnOperation> SortOrder { get; set; }
        public OperationBuilder<AddColumnOperation> IsActive { get; set; }
    }
}
