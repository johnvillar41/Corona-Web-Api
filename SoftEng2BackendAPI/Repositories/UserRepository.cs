using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserModel FetchSpecificUser(int user_id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> FetchUsers()
        {
            throw new NotImplementedException();
        }

        public bool LoginUser(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
