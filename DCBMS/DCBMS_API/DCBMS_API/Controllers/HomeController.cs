using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCBMS_API.Interface;
using DCBMS_API.Models;
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
        public HomeController(ITestTypeRepository testType)
        {
            _testType = testType;
        }

        #region TestType
        [Route("GetTestTypeList")]
        [HttpGet]
        public async Task<ActionResult<Response>> GetTestTypeList()
        {
            Response res = new Response();
            res.results =  await _testType.GetAllTestTypes();
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
            if (testTypeId>0)
            {
                res.results = await _testType.DeleteTestType(testTypeId);
            }
            return res;
        }
        #endregion

    }
}
