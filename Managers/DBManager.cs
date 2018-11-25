using System;
using System.Collections.Generic;
using System.Linq;
using Randomizer.DBAdapter;
using Randomizer.DBModels;
using Randomizer.Tools;
using RandomizerServiceInterface;

namespace Randomizer.Managers
{
    public class DBManager
    {
        private static readonly List<User> Users;

        static DBManager()
        {
            Users = SerializationManager.Deserialize<List<User>>(FileFolderHelper.StorageFilePath) ?? new List<User>();
            Users.Add(new User("May", "Polikarpova", "may@mail.com", "may", "zzz"));
            Users.Add(new User("Sofia", "Rylyuk", "sofia@mail.com", "sofia", "zzz"));
        }

        public static bool UserExists(string login)
        {
            return RandomizerServiceWrapper.UserExists(login);
        }

        public static User GetUserByLogin(string login)
        {
            return RandomizerServiceWrapper.GetUserByLogin(login);
        }

        public static User GetUsetByGuid(Guid guid)
        {
            return RandomizerServiceWrapper.GetUserByGuid(guid);
        }

        public static void AddUser(User user)
        {
            RandomizerServiceWrapper.AddUser(user);
        }

        public static void AddRequest(Request request)
        {
            RandomizerServiceWrapper.AddRequest(request);
        }

        private static void SaveChanges()
        {
            SerializationManager.Serialize(Users, FileFolderHelper.StorageFilePath);
        }

        public static User CheckCachedUser(User userCandidate)
        {
            var userInStorage = GetUsetByGuid(userCandidate.Guid);
            if (userInStorage != null && userInStorage.CheckPassword(userCandidate))
                return userInStorage;
            return null;

        }
        public static void UpdateUser(User currentUser)
        {
            RandomizerServiceWrapper.UpdateUser(currentUser);
        }
    }
 }

