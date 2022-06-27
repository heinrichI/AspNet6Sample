using NPoco;
using NPoco.FluentMappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public static class MyFactory
    {
        public static DatabaseFactory DbFactory { get; set; }

        public static void Setup(string connectionString)
        {
            //var fluentConfig = FluentMappingConfiguration.Configure(new OurMappings());
            //or individual mappings
            var fluentConfig = FluentMappingConfiguration.Configure(new UserMapping());

            DbFactory = DatabaseFactory.Config(x =>
            {
                x.UsingDatabase(() => new Database(connectionString, 
                    DatabaseType.SqlServer2012,
                    System.Data.SqlClient.SqlClientFactory.Instance));
                x.WithFluentConfig(fluentConfig);
                //x.WithMapper(new Mapper());
            });
        }
    }
}
