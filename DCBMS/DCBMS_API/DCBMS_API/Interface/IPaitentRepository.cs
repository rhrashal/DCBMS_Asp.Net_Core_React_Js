using DCBMS_API.Models;
using DCBMS_API.Models.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DCBMS_API.Interface
{
    public interface IPaitentRepository
    {
        Task<Patient> ProcessPay(int PatientId);

        Task<Patient> GetPatient(ScerchVM filterVM);

        Task<PatientVM> AddPatientRequest(PatientVM data);

        Task<List<TestWiseReportVM>> testWiseReport(FilterVM filter);
    }
}
