using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Repositories.MockImplementation
{
    public class MockUserRepository : IUserRepository
    {
        private List<UserModel> mockList = new List<UserModel>();
        public UserModel FetchSpecificUser(int user_id)
        {
            return new UserModel(1,"username", "password", null, UserModel.User_Status.ACTIVE);
        }

        public IEnumerable<UserModel> FetchUsers()
        {
            mockList.Add(new UserModel(1, "username", "password", null, UserModel.User_Status.ACTIVE));
            mockList.Add(new UserModel(2, "username", "password", null, UserModel.User_Status.ACTIVE)); 
            mockList.Add(new UserModel(3, "username", "password", null, UserModel.User_Status.ACTIVE));
            return mockList;
        }

        public bool LoginUser(string username, string password)
        {
            if (username.Equals("username") && password.Equals("password"))
                return true;

            return false;
        }
    }
}
