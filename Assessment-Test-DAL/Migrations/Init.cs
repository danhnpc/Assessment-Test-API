using FluentMigrator;

namespace Assessment_Test_DAL.Migrations;

[Migration(20251122_01)]
public class Init : Migration
{
    public override void Up()
    {
        Create.Table("Users")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Username").AsString().NotNullable()
            .WithColumn("PasswordHash").AsString().NotNullable()
            .WithColumn("Email").AsString().NotNullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable();

        Create.Table("Customers")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("FullName").AsString().NotNullable()
            .WithColumn("Phone").AsString().NotNullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("UserId").AsInt32().NotNullable().ForeignKey("Users", "Id");
    }

    public override void Down()
    {
        Delete.Table("Customers");
        Delete.Table("Users");
    }
}
