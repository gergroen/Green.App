using FluentMigrator;

namespace Green.App.Dao.FluentMigrator
{
    [Migration(1)]
    public class CreateSchemaVersionTable : Migration
    {
        public override void Up()
        {
            Create.Table("SchemaVersion")
                  .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                  .WithColumn("Version").AsInt64().Nullable()
                  .WithColumn("Script").AsString(255).Nullable();
        }

        public override void Down()
        {
            Delete.Table("SchemaVersion");
        }
    }
}
