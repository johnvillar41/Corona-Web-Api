using SoftEng2BackendAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Repositories.Interface
{
    public interface ISymptomRepository
    {
        Task<IEnumerable<SymptomsModel>> FetchAllSymptomsAsync();

        Task<SymptomsModel> FetchSpecificSymptomAsync(int symptom_id);

        Task<IEnumerable<SymptomsModel>> FetchAllSymptomsForSpecificStudentAsync(int student_id);


    }
}
