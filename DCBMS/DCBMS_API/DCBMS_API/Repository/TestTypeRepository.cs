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
    public class TestTypeRepository : ITestTypeRepository
    {
        ApplicationDbContext _context;
        public TestTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TestType>> GetAllTestTypes()
        {
            if (_context != null)
            {
                return await _context.TestTypes.ToListAsync();
            }

            return null;
        }

        public async Task<TestType> GetTestType(int? id)
        {
            var country = await _context.TestTypes.FindAsync(id);
            return country;
        }

        public async Task<string> AddTestType(TestType testType)
        {
            if (testType != null)
            {
                if (!_context.TestTypes.Any(e => e.TestTypeName == testType.TestTypeName))
                {
                    await _context.TestTypes.AddAsync(testType);
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

        public async Task<string> UpdateTestType(TestType testType)
        {
            if (testType != null)
            {
                if (_context.TestTypes.Any(e => e.Id == testType.Id))
                {
                     _context.TestTypes.Update(testType);
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

        public async Task<string> DeleteTestType(int id)
        {
            if (id >0 ) 
            {

                var testType = await _context.TestTypes.FindAsync(id);

                if (testType != null)
                {

                    _context.TestTypes.Remove(testType);
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
