using FluentMigrator;

namespace Assessment_Test_DAL.Migrations
{
    [Migration(20251122_02)]
    public class AddProductAndOrderItem : Migration
    {
        public override void Up()
        {
            Create.Table("Products")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(256).NotNullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("Price").AsDecimal(18, 2).NotNullable()
                .WithColumn("Stock").AsInt32().NotNullable()
                .WithColumn("Sku").AsString(100).NotNullable().Unique()
                .WithColumn("Category").AsString(100).Nullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("CreatedAt").AsDateTime().Nullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("UpdatedAt").AsDateTime().Nullable().WithDefault(SystemMethods.CurrentDateTime);

            Create.Table("OrderItems")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Quantity").AsInt32().NotNullable()
                .WithColumn("UnitPrice").AsDecimal(18, 2).NotNullable()
                .WithColumn("ProductId").AsInt32().NotNullable()
                .ForeignKey("FK_OrderItems_Products_ProductId", "Products", "Id").OnDelete(System.Data.Rule.Cascade);

            Create.Index("IX_Products_Category").OnTable("Products").OnColumn("Category");
            Create.Index("IX_Products_IsActive").OnTable("Products").OnColumn("IsActive");
        }

        public override void Down()
        {
            Delete.Table("OrderItems");
            Delete.Table("Products");
        }
    }
}
