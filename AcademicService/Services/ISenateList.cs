using AcademicService.Models;

namespace AcademicService.Services
{
    public interface ISenateList
    {
        Task<SenateList> MakeSenateListAsync(SenateList senateList);
        Task<IEnumerable<SenateList>> GetSenateListAsync();
    }
}
