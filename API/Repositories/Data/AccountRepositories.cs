using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Contexts;
using API.ViewModels;
using API.Handler;
using System.Linq;

namespace API.Repositories.Data;

public class AccountRepositories : GeneralRepository<Account, string>
{
    private MyContext _context;
    private DbSet<Account> _account;
    private DbSet<Employee> _employee;
    private DbSet<Profiling> _profiling;
    private DbSet<Education> _education;
    private DbSet<University> _university;
    private DbSet<AccountRole> _accrole;
    private DbSet<Role> _role;

    public AccountRepositories(MyContext context) : base(context)
    {
        _context = context;
        _account = context.Set<Account>();
        _employee = context.Set<Employee>();
        _profiling = context.Set<Profiling>();
        _education = context.Set<Education>();
        _university = context.Set<University>();
        _accrole = context.Set<AccountRole>();
        _role = context.Set<Role>();
    }

    public int Login(LoginVM login)
    {
        var result = _employee.Join(_account, e => e.NIK, a => a.NIK,
            (e, a) => new LoginVM
            {
                Email = e.Email,
                Password = a.Password
            })
            .SingleOrDefault(e => e.Email == login.Email);

        if (result == null)
        {
            return 0;
        }
        if (Hashing.ValidatePassword(login.Password, result.Password) != true)
        {
            return 1;
        }
        return 2;
    }
    public int Registrasi(RegistrasiVM entity)
    {
        var result = 0;
        var email = _employee.Where(e => e.Email == entity.Email);
        var phone = _employee.Where(e => e.Phone == entity.Phone);
        if (email.Count() != 0)
        {
            return 0;
        }
        else if (phone.Count() != 0)
        {
            return 1;
        }
        var emp = new Employee()
        {
            NIK = entity.NIK,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Gender = entity.Gender,
            BirthDate = entity.BirthDate,
            Email = entity.Email,
            Phone = entity.Phone,
            Salary = entity.Salary,
            Account = new Account()
            {
                NIK = entity.NIK,
                Password = Hashing.HashPassword(entity.Password),
                OTP = 12345,
                IsUsed = true,
                ExpiredToken = DateTime.Now
            }
        };
        _employee.Add(emp);
        result = _context.SaveChanges();

        var univ = new University()
        {
            Name = entity.UniversityName
        };
        _university.Add(univ);
        result = _context.SaveChanges();

        var edu = new Education()
        {
            Degree = entity.Degree,
            GPA = entity.GPA,
            UniversityId = univ.Id
        };
        _education.Add(edu);
        result = _context.SaveChanges();

        var prof = new Profiling()
        {
            NIK = entity.NIK,
            EducationId = edu.Id,
        };
        _profiling.Add(prof);
        result = _context.SaveChanges();

        _context.AccountRoles.Add(new AccountRole()
        {
            AccountNIK = entity.NIK,
            RoleId = 1
        });
        _context.SaveChanges();

        return 2;
    }

    public List<string> UserRoles(string email)
    {
        var result = _employee
            .Join(_account, e => e.NIK, a => a.NIK, (e, a)
            => new { _employee = e, _account = a })
            .Join(_accrole, a => a._account.NIK, ar => ar.AccountNIK, (a, ar)
            => new { a._employee, a._account, _accrole = ar })
            .Join(_context.Roles, a => a._accrole.RoleId, ed => ed.Id, (a, ed)
            => new { a._employee, a._accrole, a._account, _role = ed })
            .Where(a=>a._employee.Email == email)
            .Select(a => a._role.Name).ToList();

        return result;
    }
}