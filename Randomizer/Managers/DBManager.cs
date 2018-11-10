using System;
using System.Collections.Generic;
using System.Linq;
using Randomizer.Models;
using Randomizer.Tools;

namespace Randomizer.Managers
{
    internal class DBManager
    {
        private static readonly List<User> Users;

        static DBManager()
        {
            Users = SerializationManager.Deserialize<List<User>>(FileFolderHelper.StorageFilePath) ?? new List<User>();
            Users.Add(new User("May", "Polikarpova", "may@mail.com", "may", "zzz"));
            Users.Add(new User("Sofia", "Rylyuk", "sofia@mail.com", "sofia", "zzz"));
        }

        internal static bool UserExists(string login)
        {
            return Users.Any(u => u.Login == login);
        }

        internal static User GetUserByLogin(string login)
        {
            return Users.FirstOrDefault(u => u.Login == login);
        }

        internal static void AddUser(User user)
        {
            Users.Add(user);
            SaveChanges();
        }

        private static void SaveChanges()
        {
            SerializationManager.Serialize(Users, FileFolderHelper.StorageFilePath);
        }

        internal static User CheckCachedUser(User userCandidate)
        {
            var userInStorage = Users.FirstOrDefault(u => u.Guid == userCandidate.Guid);
            if (userInStorage != null && userInStorage.CheckPassword(userCandidate))
                return userInStorage;
            return null;

        }
        public static void UpdateUser(User currentUser)
        {
            SaveChanges();
        }
    }
 }

