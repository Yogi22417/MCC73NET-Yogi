using API.Contexts;
using API.Models;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data;

public class RolesRepositories : GeneralRepository<Role, int>
{
    public RolesRepositories(MyContext context) : base(context)
    {

    }
}
