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
    public class TestRepository : ITestRepository
    {
        ApplicationDbContext _context;
        public TestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Test>> GetAllTest()
        {
            if (_context != null)
            {
                var testList  = await _context.Tests.ToListAsync();
                var testTypeList  = await _context.TestTypes.ToListAsync();
                foreach (var item in testList)
                {
                    item.TestTypeName = testTypeList.Where(e => e.Id == item.TestTypeId).FirstOrDefault().TestTypeName;
                }
                return testList;
            }

            return null;
        }

        public async Task<Test> GetTest(int? id)
        {
            return await _context.Tests.FindAsync(id);
        }

        public async Task<string> AddTest(Test test)
        {
            if (test != null)
            {
                if (!_context.Tests.Any(e => e.TestName == test.TestName))
                {
                    await _context.Tests.AddAsync(test);
                    await _context.SaveChangesAsync();
                    return Constant.SAVED;
                }
                else
                {
                    return Constant.DATA_EXISTS;
                }
            }
            return Constant.INVAILD_DATA;
        }

        public async Task<string> UpdateTest(Test test)
        {
            if (test != null)
            {
                if (_context.Tests.Any(e => e.Id == test.Id))
                {
                     _context.Tests.Update(test);
                    await _context.SaveChangesAsync();
                    return Constant.UPDATED;
                }
                else
                {
                    return Constant.NOT_FOUND;
                }       
            }
            return Constant.INVAILD_DATA;
        }

        public async Task<string> DeleteTest(int id)
        {
            if (id >0 ) 
            {

                var test = await _context.Tests.FindAsync(id);

                if (test != null)
                {

                    _context.Tests.Remove(test);
                    await _context.SaveChangesAsync();
                    return Constant.DELETED;
                }
                else
                {
                    return Constant.NOT_FOUND;
                }
            }
            return Constant.INVAILD_DATA;
        }
    }
}
