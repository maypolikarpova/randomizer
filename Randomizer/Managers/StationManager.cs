using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Randomizer.Models;
using Randomizer.Tools;

namespace Randomizer.Managers
{
    public static class StationManager
    {
        public static User CurrentUser { get; set; }

        static StationManager()
        {
            User user = DeserializeLastUser();
            if (user != null)
            {
               DeserializeLastUserHistory(user);
            }
    }

        private static User DeserializeLastUser()
        {
            User userCandidate;
            try
            {
                userCandidate = SerializationManager.Deserialize<User>(Path.Combine(FileFolderHelper.LastUserFilePath));   
            }
            catch (Exception ex)
            {
                userCandidate = null;
                Logger.Log("Failed to Deserialize last user", ex);
            }
            if (userCandidate == null)
            {
                Logger.Log("User was not deserialized");
                return null;
            }
            userCandidate = DBManager.CheckCachedUser(userCandidate);

            if (userCandidate == null)
                Logger.Log("Failed to relogin last user");
            else
            {
                CurrentUser = userCandidate;
                CurrentUser.Requests = userCandidate.Requests;
            }

            return userCandidate;
        }

        public static void DeserializeLastUserHistory(User user)
        {
            List<Request> requests;
            try
            {
                requests = SerializationManager.Deserialize<List<Request>>(Path.Combine(FileFolderHelper.LastUserHistoryPath));
            }
            catch (Exception ex)
            {
                requests = null;
                Logger.Log("Failed to Deserialize last user history", ex);
            }
            if (requests == null)
            {
                Logger.Log("User history was not deserialized");
                return;
            }
           
            CurrentUser.Requests = requests;
        }

        internal static void CloseApp()
        {
            SerializationManager.Serialize(CurrentUser, FileFolderHelper.LastUserFilePath);
            SerializationManager.Serialize(CurrentUser.Requests, FileFolderHelper.LastUserHistoryPath);
            Environment.Exit(1);
        }
    }
}
