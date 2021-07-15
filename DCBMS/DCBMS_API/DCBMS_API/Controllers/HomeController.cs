using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCBMS_API.Interface;
using DCBMS_API.Models;
using DCBMS_API.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DCBMS_API.Controllers
{

    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly ITestTypeRepository _testType;
        private readonly ITestRepository _test;
        private readonly IPaitentRepository _patient;
        public HomeController(ITestTypeRepository testType, ITestRepository test, IPaitentRepository patient)
        {
            _testType = testType;
            _test = test;
            _patient = patient;
        }

        #region Patient

        [Route("BillPay")]
        [HttpGet]
        public async Task<ActionResult<Response>> ScerchBill(int Id)
        {
            Response res = new Response();
            res.results = await _patient.ProcessPay(Id);
            return res;
        }


        [Route("ScerchBill")]
        [HttpGet]
        public async Task<ActionResult<Response>> ScerchBill(ScerchVM filterVM)
        {
            Response res = new Response();
            res.results = await _patient.GetPatient(filterVM);
            return res;
        }


        [Route("AddPatientRequest")]
        [HttpPost]
        public async Task<ActionResult<Response>> AddTest(PatientVM patient)
        {
            Response res = new Response();
            if (patient != null && patient.TestRequestList.Count > 0)
            {
                res.results = await _patient.AddPatientRequest(patient);
            }
            return res;
        }

        #endregion

        #region Test
        [Route("GetTestList")]
        [HttpGet]
        public async Task<ActionResult<Response>> GetTestList()
        {
            Response res = new Response();
            res.results = await _test.GetAllTest();
            return res;
        }

        [Route("AddTest")]
        [HttpPost]
        public async Task<ActionResult<Response>> AddTest(Test test)
        {
            Response res = new Response();
            if (ModelState.IsValid)
            {
                res.results = await _test.AddTest(test);
            }
            return res;
        }

        [Route("EditTest")]
        [HttpPut]
        public async Task<ActionResult<Response>> EditTestType(Test test)
        {
            Response res = new Response();
            if (ModelState.IsValid)
            {
                res.results = await _test.UpdateTest(test);
            }
            return res;
        }

        [Route("DeleteTest")]
        [HttpDelete]
        public async Task<ActionResult<Response>> DeleteTest(int testId)
        {
            Response res = new Response();
            if (testId > 0)
            {
                res.results = await _test.DeleteTest(testId);
            }
            return res;
        }
        #endregion

        #region TestType
        [Route("GetTestTypeList")]
        [HttpGet]
        public async Task<ActionResult<Response>> GetTestTypeList()
        {
            Response res = new Response();
            res.results = await _testType.GetAllTestTypes();
            return res;
        }

        [Route("AddTestType")]
        [HttpPost]
        public async Task<ActionResult<Response>> AddTestType(TestType testType)
        {
            Response res = new Response();
            if (ModelState.IsValid)
            {
                res.results = await _testType.AddTestType(testType);
            }
            return res;
        }

        [Route("EditTestType")]
        [HttpPut]
        public async Task<ActionResult<Response>> EditTestType(TestType testType)
        {
            Response res = new Response();
            if (ModelState.IsValid)
            {
                res.results = await _testType.UpdateTestType(testType);
            }
            return res;
        }

        [Route("DeleteTestType")]
        [HttpDelete]
        public async Task<ActionResult<Response>> DeleteTestType(int testTypeId)
        {
            Response res = new Response();
            if (testTypeId > 0)
            {
                res.results = await _testType.DeleteTestType(testTypeId);
            }
            return res;
        }
        #endregion

        #region Report
        [Route("TestWiseReport")]
        [HttpPost]
        public async Task<ActionResult<Response>> TestWiseReport(FilterVM filter)
        {
            Response res = new Response();
            res.results = await _patient.TestWiseReport(filter);
            return res;
        }

        [Route("TypeWiseReport")]
        [HttpGet]
        public async Task<ActionResult<Response>> TypeWiseReport(FilterVM filter)
        {
            Response res = new Response();
            res.results = await _patient.TypeWiseReport(filter);
            return res;
        }

        [Route("UnPaidBillReport")]
        [HttpGet]
        public async Task<ActionResult<Response>> UnPaidBillReport(FilterVM filter)
        {
            Response res = new Response();
            res.results = await _patient.UnPaidBillReport(filter);
            return res;
        }

        #endregion
    }
}
