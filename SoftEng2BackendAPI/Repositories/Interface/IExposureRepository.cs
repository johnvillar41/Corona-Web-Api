using SoftEng2BackendAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Repositories.Interface
{
    public interface IExposureRepository
    {
        Task<IEnumerable> FetchAllExposedStudentsAsync();
        Task<IEnumerable<UserModel>> FetchExposedStudentsGivenByIDAsync(int id);
    }
}
