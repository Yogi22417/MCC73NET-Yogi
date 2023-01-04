using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Contexts;
namespace API.Repositories.Data;

public class ProfilingRepositories : GeneralRepository<Profiling, string>
{
    public ProfilingRepositories(MyContext context) : base(context)
    {

    }
}
