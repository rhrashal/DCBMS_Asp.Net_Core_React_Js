using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCBMS_API.Data;
using DCBMS_API.Interface;
using DCBMS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DCBMS_API.Repository
{
    public class PaitentRepository : IPaitentRepository
    {
        ApplicationDbContext _context;
        public PaitentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PatientVM> AddPatientRequest(PatientVM data)
        {
            try
            {
                List<TestRequest> requestList = new List<TestRequest>();

                Patient patient = new Patient();

                patient.PatientName = data.PatientName;
                patient.DateOfBirth = data.DateOfBirth;
                patient.Mobile = data.Mobile;
                patient.TestDate = DateTime.Now;
                patient.BillNo = data.PatientName.Substring(0,3) + DateTime.Now.Minute + DateTime.Now.Hour+DateTime.Now.Day+ DateTime.Now.Month+ DateTime.Now.Year;
                patient.IsPaid = false;
                patient.Status = "Unpaid";

                await _context.Patients.AddAsync(patient);
                //await _context.SaveChangesAsync();

                if (patient.Id>0 && data.TestRequestList.Count>0)
                {
                    foreach (var item in data.TestRequestList)
                    {
                        TestRequest request = new TestRequest();
                        request.PatientId = patient.Id;
                        request.TestId = item.TestId;
                        request.PayableAmount = item.PayableAmount;
                        requestList.Add(request);
                    }
                    await _context.TestRequests.AddRangeAsync(requestList);
                    await _context.SaveChangesAsync();                    
                }

                var requestRes = _context.TestRequests.Where(e => e.PatientId == patient.Id).ToList();
                foreach (var item in requestRes)
                {
                    item.TestName = item.Test.TestTypeName;
                }
                PatientVM res = new PatientVM();
                res.PatientName = patient.PatientName;
                res.DateOfBirth = patient.DateOfBirth;
                res.Mobile = patient.Mobile;
                res.TestDate = patient.TestDate;
                res.BillNo = patient.BillNo;
                res.IsPaid = patient.IsPaid;
                res.Status = patient.Status;
                res.Id = patient.Id;
                res.TestRequestList = requestRes;

                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public async Task<List<Test>> GetAllTest()
        //{
        //    if (_context != null)
        //    {
        //        var testList  = await _context.Tests.ToListAsync();
        //        var testTypeList  = await _context.TestTypes.ToListAsync();
        //        foreach (var item in testList)
        //        {
        //            item.TestTypeName = testTypeList.Where(e => e.Id == item.TestTypeId).FirstOrDefault().TestTypeName;
        //        }
        //        return testList;
        //    }

        //    return null;
        //}

        //public async Task<Test> GetTest(int? id)
        //{
        //    return await _context.Tests.FindAsync(id);
        //}

        //public async Task<string> AddTest(Test test)
        //{
        //    if (test != null)
        //    {
        //        if (!_context.Tests.Any(e => e.TestName == test.TestName))
        //        {
        //            await _context.Tests.AddAsync(test);
        //            await _context.SaveChangesAsync();
        //            return Constant.SAVED;
        //        }
        //        else
        //        {
        //            return Constant.DATA_EXISTS;
        //        }
        //    }
        //    return Constant.INVAILD_DATA;
        //}

        //public async Task<string> UpdateTest(Test test)
        //{
        //    if (test != null)
        //    {
        //        if (_context.Tests.Any(e => e.Id == test.Id))
        //        {
        //             _context.Tests.Update(test);
        //            await _context.SaveChangesAsync();
        //            return Constant.UPDATED;
        //        }
        //        else
        //        {
        //            return Constant.NOT_FOUND;
        //        }       
        //    }
        //    return Constant.INVAILD_DATA;
        //}

        //public async Task<string> DeleteTest(int id)
        //{
        //    if (id >0 ) 
        //    {

        //        var test = await _context.Tests.FindAsync(id);

        //        if (test != null)
        //        {

        //            _context.Tests.Remove(test);
        //            await _context.SaveChangesAsync();
        //            return Constant.DELETED;
        //        }
        //        else
        //        {
        //            return Constant.NOT_FOUND;
        //        }
        //    }
        //    return Constant.INVAILD_DATA;
        //}
    }
}
