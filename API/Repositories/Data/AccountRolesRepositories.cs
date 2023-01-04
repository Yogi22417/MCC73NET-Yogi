using API.Contexts;
using API.Models;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data;

public class AccountRolesRepositories : GeneralRepository<AccountRole, int>
{
    public AccountRolesRepositories(MyContext context) : base(context)
    {
        
    }
}