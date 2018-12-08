﻿using Randomizer.DBModels;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace RandomizerServiceInterface
{
    public class RandomizerServiceWrapper
    {
        public static bool UserExists(string login)
        {
            using (var myChannelFactory = new ChannelFactory<IRandomizerContract>("Server"))
            {
                IRandomizerContract client = myChannelFactory.CreateChannel();
                return client.UserExists(login);
            }
        }

        public static User GetUserByLogin(string login)
        {
            using (var myChannelFactory = new ChannelFactory<IRandomizerContract>("Server"))
            {
                IRandomizerContract client = myChannelFactory.CreateChannel();
                return client.GetUserByLogin(login);
            }
        }

        public static User GetUserByGuid(Guid guid)
        {
            using (var myChannelFactory = new ChannelFactory<IRandomizerContract>("Server"))
            {
                IRandomizerContract client = myChannelFactory.CreateChannel();
                return client.GetUserByGuid(guid);
            }
        }

        public static void AddUser(User user)
        {
            using (var myChannelFactory = new ChannelFactory<IRandomizerContract>("Server"))
            {
                IRandomizerContract client = myChannelFactory.CreateChannel();
                client.AddUser(user);
            }
        }

        public static void AddRequest(Request request)
        {
            using (var myChannelFactory = new ChannelFactory<IRandomizerContract>("Server"))
            {
                IRandomizerContract client = myChannelFactory.CreateChannel();
                client.AddRequest(request);
            }
        }

        public static void UpdateUser(User currentUser)
        {
            using (var myChannelFactory = new ChannelFactory<IRandomizerContract>("Server"))
            {
                IRandomizerContract client = myChannelFactory.CreateChannel();
                client.UpdateUser(currentUser);
            }
        }

        public static void SaveRequest(Request request)
        {
            using (var myChannelFactory = new ChannelFactory<IRandomizerContract>("Server"))
            {
                IRandomizerContract client = myChannelFactory.CreateChannel();
                client.SaveRequest(request);
            }
        }

        public static List<User> GetAllUsers(Guid requestGuid)
        {
            using (var myChannelFactory = new ChannelFactory<IRandomizerContract>("Server"))
            {
                IRandomizerContract client = myChannelFactory.CreateChannel();
                return client.GetAllUsers(requestGuid);
            }
        }

        public static void DeleteRequest(Request selectedRequest)
        {
            using (var myChannelFactory = new ChannelFactory<IRandomizerContract>("Server"))
            {
                IRandomizerContract client = myChannelFactory.CreateChannel();
                client.DeleteRequest(selectedRequest);
            }
        }
    }
}

