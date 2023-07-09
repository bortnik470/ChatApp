using ChatApp.Models;

namespace ChatApp.Modules.Data.Finders
{
    public class UserManager
    {
        public readonly AuthenticationDbContext _context;

        public UserManager(AuthenticationDbContext context)
        {
            _context = context;
        }

        public UserModel findUserById(string id)
        {
            return _context.Users.Find(id);
        }

        public UserModel findUserByNick(string nick)
        {
            return _context.Users.First(x => x.UserName == nick);
        }
    }
}