using System.Collections.Generic;
using System.Linq;
using Randomizer.Models;

namespace Randomizer.Managers
{
    public class DBManager
    {
        private static readonly List<User> Users = new List<User>();

        static DBManager()
        {
            Users.Add(new User("May", "Polikarpova", "may@mail.com", "may", "zzz"));
            Users.Add(new User("Sofia", "Rylyuk", "sofia@mail.com", "sofia", "zzz"));
        }

        public static bool UserExists(string login)
        {
            return Users.Any(u => u.Login == login);
        }

        public static User GetUserByLogin(string login)
        {
            return Users.FirstOrDefault(u => u.Login == login);
        }

        public static void AddUser(User user)
        {
            Users.Add(user);
        }
        
        
    }
}

