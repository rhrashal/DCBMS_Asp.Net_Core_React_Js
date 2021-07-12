using DCBMS_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DCBMS_API.Interface
{
    public interface ITestRepository
    {
        Task<List<Test>> GetAllTest();

        Task<Test> GetTest(int? id);

        Task<string> AddTest(Test test);

        Task<string> DeleteTest(int id);

        Task<string> UpdateTest(Test test);
    }
}
