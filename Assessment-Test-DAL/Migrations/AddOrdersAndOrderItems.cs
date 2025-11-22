

using FluentMigrator;
using System.Diagnostics.Metrics;

namespace Assessment_Test_DAL.Migrations;

[Migration(202511230900)]
public class AddOrdersAndOrderItems : Migration
{
    public override void Up()
    {
        Create.Table("Orders")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("CustomerId").AsInt32().NotNullable()
            .WithColumn("TotalAmount").AsDecimal(18, 2).NotNullable().WithDefaultValue(0)
            .WithColumn("CreatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime);


        // Example: add OrderId to existing OrderItems (nullable to be safe)
        if (!Schema.Table("OrderItems").Column("OrderId").Exists())
        {
            Alter.Table("OrderItems").AddColumn("OrderId").AsInt32().Nullable();
            Create.ForeignKey("FK_OrderItems_Orders_OrderId")
                .FromTable("OrderItems").ForeignColumn("OrderId")
                .ToTable("Orders").PrimaryColumn("Id").OnDelete(System.Data.Rule.Cascade);
        }
    }

    public override void Down()
    {
        if (Schema.Table("OrderItems").Column("OrderId").Exists())
        {
            Delete.ForeignKey("FK_OrderItems_Orders_OrderId").OnTable("OrderItems");
            Delete.Column("OrderId").FromTable("OrderItems");
        }

        Delete.Table("Orders");
    }
}
