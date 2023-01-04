using API.Contexts;
using API.Models;
using API.ViewModels;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data;

public class EducationRepositories : GeneralRepository<Education, int>
{
    private MyContext _context;
    public EducationRepositories(MyContext context) : base(context)
    {
        _context = context;
    }
    public IEnumerable<MasterEducationVM> MasterEducation()
    {
        var result = _context.Educations.Join(_context.Universities, e => e.UniversityId, u => u.Id,
            (e, u) => new MasterEducationVM
            {
                Id = e.Id,
                Degree = e.Degree,
                GPA = e.GPA,
                UniversityName = u.Name
            });
        return result;
    }
}
