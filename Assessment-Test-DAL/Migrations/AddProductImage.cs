using FluentMigrator;

namespace Assessment_Test_DAL.Migrations;

[Migration(202511220900)]
public class AddProductImage : Migration
{
    public override void Up()
    {
        Alter.Table("Products").AddColumn("ImageUrl").AsString(512).Nullable();
    }

    public override void Down()
    {
        Delete.Column("ImageUrl").FromTable("Products");

    }
}
