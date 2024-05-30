using AcademicService.Models;
using AppDbContext.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicService.Services
{
    public class SenateListService : ISenateList
    {
        private readonly AcademicDbContext _context;

        public SenateListService(AcademicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SenateList>> GetSenateListAsync()
        {
            return await _context.SenateLists.ToListAsync();
        }

        public async Task<SenateList> MakeSenateListAsync(SenateList senateList)
        {
            _context.SenateLists.Add(senateList);
            await _context.SaveChangesAsync();

            return senateList;
        }
    }
}
