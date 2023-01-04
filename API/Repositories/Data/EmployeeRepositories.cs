using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Contexts;
using API.ViewModels;
using Castle.Core.Resource;

namespace API.Repositories.Data;

public class EmployeeRepositories : GeneralRepository<Employee, string>
{
    private MyContext _context;
    private DbSet<Employee> _employee;
    private DbSet<Account> _account;
    private DbSet<Profiling> _profiling;
    private DbSet<Education> _education;
    private DbSet<University> _university;

    public EmployeeRepositories(MyContext context) : base(context)
    {
        _context = context;
        _employee = context.Set<Employee>();
        _account = context.Set<Account>();
        _profiling = context.Set<Profiling>();
        _education = context.Set<Education>();
        _university = context.Set<University>();
    }

    public IEnumerable<MasterEmployeeDataVM> MasterEmployeeData()
    {
        var result = _employee
            .Join(_account, e => e.NIK, a => a.NIK, (e, a) 
            => new { _employee = e, _account = a})
            .Join(_profiling, a => a._employee.NIK, p => p.NIK, (a, p) 
            => new { a._employee, a._account, _profiling = p  })
            .Join(_education, a => a._profiling.EducationId, ed => ed.Id, (a, ed)
            => new { a._employee, a._profiling, a._account, _education = ed})
            .Join(_university, a => a._education.UniversityId, u => u.Id, (a, u) 
            => new { a._employee, a._profiling, a._account, a._education, _university = u }).
            Select(s => new MasterEmployeeDataVM
            {
                NIK=  s._employee.NIK,
                FullName = s._employee.FirstName + " " + s._employee.LastName,
                Phone = s._employee.Phone,
                Gender = (s._employee.Gender).ToString(),
                Email = s._employee.Email,
                BirthDate = s._employee.BirthDate,
                Salary = s._employee.Salary,
                Education_id = s._education.Id,
                GPA = s._education.GPA,
                Degree = s._education.Degree,
                UniversityName = s._university.Name,
            }).ToList();
        return result;
    }

}
