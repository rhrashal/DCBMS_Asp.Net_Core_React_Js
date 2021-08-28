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
                decimal grandTotal = 0;
                foreach (var item in testLIst)
                {
                    TestWiseReportVM rpt = new TestWiseReportVM();
                    rpt.TestId = item.Id;
                    rpt.TestName = item.TestName;
                    rpt.NoOfTest = testRequestList.Where(e => e.TestId == item.Id).ToList().Count();
                    rpt.TotalAmount = testRequestList.Where(e => e.TestId == item.Id).ToList().Sum(e => e.PayableAmount);
                    grandTotal += rpt.TotalAmount;
                    response.Add(rpt);
                }
                response[0].GrandTotal = grandTotal;
                var date = NumberToWords(Convert.ToInt32( grandTotal));

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            

            
        }
        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "শূন্য";

            if (number < 0)
                return "মাইনাস " + NumberToWords(Math.Abs(number));

            string words = "";
            if ((number / 10000000) > 0)
            {
                words += NumberToWords(number / 10000000) + " কোটি ";
                number %= 10000000;
            }
            if ((number / 100000) > 0)
            {
                words += NumberToWords(number / 100000) + " লাখ ";
                number %= 100000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " হাজার ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " শত ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += " ";
                //words += "এবং ";

                var unitsMap = new[] { "শূন্য", "এক", "দুই", "তিন", "চার", "পাঁচ", "ছয়", "সাত", "আট", "নয়", "দশ", "এগারো", "বারো", "তেরো", "চৌদ্দ", "পনেরো", "ষোল", "সতেরো", "আঠারো", "ঊনিশ", "বিশ", "একুশ", "বাইশ", "তেইশ ", "চব্বিশ", "পঁচিশ", "ছাব্বিশ", "সাতাশ", "আঠাশ", "ঊনত্রিশ", "ত্রিশ", "একত্রিশ", "বত্রিশ", "তেত্রিশ", "চৌত্রিশ", "পঁয়ত্রিশ", "ছত্রিশ", "সাঁইত্রিশ", "আটত্রিশ", "ঊনচল্লিশ", "চল্লিশ", "একচল্লিশ", "বিয়াল্লিশ", "তেতাল্লিশ", "চুয়াল্লিশ", "পঁয়তাল্লিশ", "ছেচল্লিশ", "সাতচল্লিশ", "আটচল্লিশ", "ঊনপঞ্চাশ", "পঞ্চাশ", "একান্ন", "বায়ান্ন", "তিপ্পান্ন", "চুয়ান্ন", "পঞ্চান্ন", "ছাপ্পান্ন", "সাতান্ন", "আটান্ন", "ঊনষাট", "ষাট", "একষট্টি", "বাষট্টি", "তেষট্টি", "চৌষট্টি", "পঁয়ষট্টি", "ছেষট্টি", "সাতষট্টি", "আটষট্টি", "ঊনসত্তর", "সত্তর", "একাত্তর", "বাহাত্তর", "তিয়াত্তর", "চুয়াত্তর", "পঁচাত্তর", "ছিয়াত্তর", "সাতাত্তর", "আটাত্তর", "ঊনআশি", "আশি", "একাশি", "বিরাশি", "তিরাশি", "চুরাশি", "পঁচাশি", "ছিয়াশি", "সাতাশি", "আটাশি", "ঊননব্বই", "নব্বই", "একানব্বই", "বিরানব্বই", "তিরানব্বই", "চুরানব্বই", "পঁচানব্বই", "ছিয়ানব্বই", "সাতানব্বই", "আটানব্বই", "নিরানব্বই", "একশো" };
                var tensMap = new[] { "শূন্য", "দশ", "বিশ", "ত্রিশ", "চল্লিশ", "পঞ্চাশ", "ষাট", "সত্তর", "আশি", "নব্বই" };

                if (number < 100)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
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
                decimal grandTotal = 0;
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
                    grandTotal += rpt.TotalAmount;
                    response.Add(rpt);
                }

                response[0].GrandTotal = grandTotal;
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
            patientList[0].GrandTotal = patientList.Sum(e=> e.TotalAmount);
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
