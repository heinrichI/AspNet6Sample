using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMigration
{
    [Migration(20220626121655)]
    public class Migration1 : Migration
    {        
        public override void Up()
        {
            Create.Table("User")
                .WithColumn("[Username]").AsString(100).PrimaryKey()
                .WithColumn("[PasswordHash]").AsBinary(64).Nullable()
                .WithColumn("Email").AsString(100).Nullable().Unique();
        }

        public override void Down()
        {
            Delete.Table("User");
        }
    }
}
