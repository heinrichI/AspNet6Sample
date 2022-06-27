using BusinessLogic;
using NPoco;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace DataBase
{
    public class UserRepository : IUserRepository
    {
        public User GetSingle(Expression<Func<User, bool>> exp)
        {
            using (IDatabase db = MyFactory.DbFactory.GetDatabase())
            {
                return db.Query<User>().Where(exp).SingleOrDefault();
            }
        }

        public async Task<bool> Save(User user)
        {
            using (IDatabase db = MyFactory.DbFactory.GetDatabase())
            {
                await db.InsertAsync<User>(user);
                return true;
            }
        }
    }
}