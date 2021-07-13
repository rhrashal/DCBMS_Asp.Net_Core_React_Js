using DCBMS_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DCBMS_API.Interface
{
    public interface IPaitentRepository
    {
        //Task<List<Test>> GetAllTest();

        //Task<Test> GetTest(int? id);

        Task<PatientVM> AddPatientRequest(PatientVM data);

        //Task<string> DeleteTest(int id);

        //Task<string> UpdateTest(Test test);
    }
}
