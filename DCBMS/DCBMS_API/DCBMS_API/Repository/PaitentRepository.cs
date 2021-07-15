using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCBMS_API.Data;
using DCBMS_API.Interface;
using DCBMS_API.Models;
using DCBMS_API.Models.ViewModel;
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

        public async Task<Patient> GetPatient(ScerchVM filterVM)
        {
            if (filterVM != null)
            {
                if ( !String.IsNullOrEmpty( filterVM.BillNo))
                {
                    return await _context.Patients.Where(e => e.BillNo == filterVM.BillNo).FirstOrDefaultAsync();
                }
                else if(!String.IsNullOrEmpty(filterVM.Mobile))
                {
                    return await _context.Patients.Where(e => e.Mobile == filterVM.Mobile).FirstOrDefaultAsync();
                }                
            }
            return null;
        }
        public async Task<PatientVM> AddPatientRequest(PatientVM data)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    List<TestRequest> requestList = new List<TestRequest>();

                    Patient patient = new Patient();
                    decimal totalAmount = 0;
                    foreach (var item in data.TestRequestList)
                    {
                        totalAmount = totalAmount + item.PayableAmount;
                    }

                    patient.PatientName = data.PatientName;
                    patient.DateOfBirth = data.DateOfBirth;
                    patient.Mobile = data.Mobile;
                    patient.TestDate = DateTime.Now;
                    patient.DueDate = DateTime.Now;
                    patient.BillNo = DateTime.Now.Second.ToString() + DateTime.Now.Minute + DateTime.Now.Hour+"-" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.ToString("yy"); 
                    patient.IsPaid = false;
                    patient.Status = "Unpaid";
                    patient.TotalAmount = totalAmount;


                    await _context.Patients.AddAsync(patient);
                    await _context.SaveChangesAsync();

                    if (patient.Id > 0 && data.TestRequestList.Count > 0)
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
                        transaction.Commit();
                    }
                    var requestRes = _context.TestRequests.Where(e => e.PatientId == patient.Id).Include(e=> e.Test).ToList();
                    foreach (var item in requestRes)
                    {
                        item.TestName = item.Test.TestName;
                    }
                    PatientVM res = new PatientVM();
                    res.PatientName = patient.PatientName;
                    res.DateOfBirth = patient.DateOfBirth;
                    res.Mobile = patient.Mobile;
                    res.TestDate = patient.TestDate;
                    res.DueDate = patient.DueDate;
                    res.BillNo = patient.BillNo;
                    res.IsPaid = patient.IsPaid;
                    res.Status = patient.Status;
                    res.TotalAmount = patient.TotalAmount;
                    res.Id = patient.Id;
                    res.TestRequestList = requestRes;

                    return res;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<string> ProcessPay(int PatientId)
        {
            Patient patient = new Patient();
            if (PatientId>0)
            {
                patient = await _context.Patients.Where(e => e.Id == PatientId).FirstOrDefaultAsync();
                if (!patient.IsPaid)
                {
                    patient.IsComplete = true;
                    patient.IsPaid = true;
                    patient.Status = "Paid";
                    _context.Patients.Update(patient);
                    _context.SaveChanges();
                    return "Bill Paid Successfully.";
                }
                else
                {
                    return "Bill already Paid.";
                }
            }
            return "Bill Not found.";
        }

        public async Task<List<TestWiseReportVM>> TestWiseReport(FilterVM filter)
        {
            try
            {
                DateTime fromDate = Convert.ToDateTime(filter.FromDate);
                DateTime toDate = Convert.ToDateTime(filter.ToDate);
                List<TestWiseReportVM> response = new List<TestWiseReportVM>();
                List<TestRequest> testRequestList = new List<TestRequest>();

                var patientList = await _context.Patients.Where(e => e.TestDate.Date >= fromDate.Date && e.TestDate.Date <= toDate.Date && e.IsPaid == true).ToListAsync();
                foreach (var item in patientList)
                {
                    var requestList = await _context.TestRequests.Where(e => e.PatientId == item.Id).ToListAsync();
                    testRequestList.AddRange(requestList);
                }
                var testLIst = await _context.Tests.ToListAsync();
                foreach (var item in testLIst)
                {
                    TestWiseReportVM rpt = new TestWiseReportVM();
                    rpt.TestId = item.Id;
                    rpt.TestName = item.TestName;
                    rpt.NoOfTest = testRequestList.Where(e => e.TestId == item.Id).ToList().Count();
                    rpt.TotalAmount = testRequestList.Where(e => e.TestId == item.Id).ToList().Sum(e => e.PayableAmount);
                    response.Add(rpt);
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            

            
        }

        public async Task<List<TypeWiseReportVM>> TypeWiseReport(FilterVM filter)
        {
            try
            {
                DateTime fromDate = Convert.ToDateTime(filter.FromDate);
                DateTime toDate = Convert.ToDateTime(filter.ToDate);
                List<TypeWiseReportVM> response = new List<TypeWiseReportVM>();
                List<TestRequest> testRequestList = new List<TestRequest>();

                var patientList = await _context.Patients.Where(e => e.TestDate.Date >= fromDate.Date && e.TestDate.Date <= toDate.Date && e.IsPaid == true).ToListAsync();
                foreach (var item in patientList)
                {
                    var requestList = await _context.TestRequests.Where(e => e.PatientId == item.Id).ToListAsync();
                    testRequestList.AddRange(requestList);
                }
                var testTypeList = await _context.TestTypes.ToListAsync();
                var testList = await _context.Tests.ToListAsync();
                foreach (var item in testTypeList)
                {
                    TypeWiseReportVM rpt = new TypeWiseReportVM();
                    rpt.TestTypeId = item.Id;
                    rpt.TestTypeName = item.TestTypeName;

                    //sub loop
                    int no = 0;
                    decimal amount = 0;
                    var testFlt = testList.Where(e => e.TestTypeId == item.Id).ToList();
                    foreach (var p in testFlt)
                    {
                        no = no + testRequestList.Where(e => e.TestId == p.Id).ToList().Count();
                        amount = amount + testRequestList.Where(e => e.TestId == p.Id).ToList().Sum(e => e.PayableAmount);
                    }

                    rpt.NoOfTest = no;
                    rpt.TotalAmount = amount;

                    response.Add(rpt);
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Patient>> UnPaidBillReport(FilterVM filter)
        {
            List<Patient> patientList = new List<Patient>();
            DateTime fromDate = Convert.ToDateTime(filter.FromDate);
            DateTime toDate = Convert.ToDateTime(filter.ToDate);
            patientList = await _context.Patients.Where(e => e.TestDate.Date >= fromDate.Date && e.TestDate.Date <= toDate.Date && e.IsPaid == false).ToListAsync();
            return patientList;
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
