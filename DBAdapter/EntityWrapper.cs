using Randomizer.DBModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randomizer.DBAdapter
{
    public static class EntityWrapper
    {
        public static bool UserExists(string login)
        {
            using (var context = new RandomizerDBContext())
            {
                return context.Users.Any(u => u.Login == login);
            }
        }

        public static User GetUserByLogin(string login)
        {
            using (var context = new RandomizerDBContext())
            {
                return context.Users.Include(u => u.Requests).FirstOrDefault(u => u.Login == login);
            }
        }

        public static User GetUserByGuid(Guid guid)
        {
            using (var context = new RandomizerDBContext())
            {
                return context.Users.Include(u => u.Requests).FirstOrDefault(u => u.Guid == guid);
            }
        }

        public static List<User> GetAllUsers(Guid requestGuid)
        {
            using (var context = new RandomizerDBContext())
            {
                return context.Users.Where(u => u.Requests.All(r => r.Guid != requestGuid)).ToList();
            }
        }

        public static void AddUser(User user)
        {
            using (var context = new RandomizerDBContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public static void UpdateUser(User user)
        {
            using (var context = new RandomizerDBContext())
            {
                context.SaveChanges();
            }

        }

        public static void AddRequest(Request request)
        {
            using (var context = new RandomizerDBContext())
            {
                request.DeleteDatabaseValues();
                context.Requests.Add(request);
                context.SaveChanges();
            }
        }

        public static void SaveRequest(Request request)
        {
            using (var context = new RandomizerDBContext())
            {
                context.Entry(request).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void DeleteRequest(Request selectedRequest)
        {
            using (var context = new RandomizerDBContext())
            {
                selectedRequest.DeleteDatabaseValues();
                context.Requests.Attach(selectedRequest);
                context.Requests.Remove(selectedRequest);
                context.SaveChanges();
            }
        }
    }
}
