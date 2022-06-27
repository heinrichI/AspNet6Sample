using BusinessLogic;
using NPoco.FluentMappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class UserMapping : Map<User>
    {
        public UserMapping()
        {
            PrimaryKey(x => x.Name, autoIncrement: false);
            TableName("User");
            Columns(x =>
            {
                x.Column(y => y.Name).WithName("Username");
                x.Column(y => y.Password).WithName("PasswordHash");
                x.Column(y => y.Email);
            });
        }
    }
}
