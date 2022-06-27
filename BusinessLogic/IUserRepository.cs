using System.Linq.Expressions;

namespace BusinessLogic
{
    public interface IUserRepository
    {
        Task<bool> Save(User user);

        User GetSingle(Expression<Func<User, bool>> exp);
    }
}