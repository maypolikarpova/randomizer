using Randomizer.DBModels;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace RandomizerServiceInterface
{
    [ServiceContract]
    public interface IRandomizerContract
    {
        [OperationContract]
        bool UserExists(string login);
        [OperationContract]
        User GetUserByLogin(string login);
        [OperationContract]
        User GetUserByGuid(Guid guid);
        [OperationContract]
        List<User> GetAllUsers(Guid requestGuid);
        [OperationContract]
        void AddUser(User user);
        [OperationContract]
        void UpdateUser(User user);
        [OperationContract]
        void AddRequest(Request request);
        [OperationContract]
        void SaveRequest(Request request);
        [OperationContract]
        void DeleteRequest(Request selectedRequest);
    }
}
