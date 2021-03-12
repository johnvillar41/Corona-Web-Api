using SoftEng2BackendAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Repositories.Interface
{
    public interface ISymptomRepository
    {
        Task<IEnumerable<SymptomsModel>> FetchAllPeopleWithSymptomsAsync();

        Task<IEnumerable<UserModel>> FetchStudentsWithSpecificSymptomAsync(string symptom_name);

        Task<IEnumerable<SymptomsModel>> FetchSymptomsForSpecificStudentAsync(int student_id);
        Task DeleteAllStudentSymptomsAsync(int id);
        Task InsertNewSymptoms(SymptomsModel symptoms);
        Task DeleteSpecificSymptomAsync(int symptom_id);
    }
}
