using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> FetchUsers();
        Task<UserModel> FetchSpecificUser(int user_id);
        Task<bool> LoginUser(string username, string password);
    }
}
