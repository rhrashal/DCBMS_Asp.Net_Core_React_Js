using DCBMS_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DCBMS_API.Interface
{
    public interface ITestTypeRepository
    {
        Task<List<TestType>> GetAllTestTypes();

        Task<TestType> GetTestType(int? id);

        Task<string> AddTestType(TestType testType);

        Task<string> DeleteTestType(int id);

        Task<string> UpdateTestType(TestType testType);
    }
}
