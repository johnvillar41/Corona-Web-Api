using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> FetchUsersAsync();
        Task<UserModel> FetchSpecificUserAsync(int user_id);
        Task<bool> LoginUserAsync(string username, string password);

        Task RegisterNewUserAsync(UserModel newUser);
    }
}
