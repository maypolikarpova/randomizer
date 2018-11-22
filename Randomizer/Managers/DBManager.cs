using System;
using System.Collections.Generic;
using System.Linq;
using Randomizer.DBAdapter;
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
            return EntityWrapper.UserExists(login);
        }

        internal static User GetUserByLogin(string login)
        {
            var v = EntityWrapper.GetUserByLogin(login);
            return v;
        }

        internal static User GetUsetByGuid(Guid guid)
        {
            return EntityWrapper.GetUserByGuid(guid);
        }

        internal static void AddUser(User user)
        {
            EntityWrapper.AddUser(user);
        }

        internal static void AddRequest(Request request)
        {
            EntityWrapper.AddRequest(request);
        }

        private static void SaveChanges()
        {
            SerializationManager.Serialize(Users, FileFolderHelper.StorageFilePath);
        }

        internal static User CheckCachedUser(User userCandidate)
        {
            var userInStorage = GetUsetByGuid(userCandidate.Guid);
            if (userInStorage != null && userInStorage.CheckPassword(userCandidate))
                return userInStorage;
            return null;

        }
        public static void UpdateUser(User currentUser)
        {
            EntityWrapper.UpdateUser(currentUser);
        }
    }
 }

