using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = "Admin")]
public class AccountRoleController : BaseController<AccountRolesRepositories,AccountRole, int>
{

    public AccountRoleController(AccountRolesRepositories repositories) : base(repositories) { }

    //[HttpGet]
    //public ActionResult GetAll()
    //{
    //    try
    //    {
    //        var result = _repositories.Get();
    //        return result == null
    //            ? Ok(new { statusCode = 200, message = "Data Tidak Ditemukan !!" })
    //            : Ok(new { statusCode = 200, message = "Data Berhasil Diterima", data = result });
    //    }
    //    catch
    //    {
    //        return BadRequest(new { statusCode = 500, message = "Something Wrong I Can Fell It !!" });
    //    }
    //}

    //[HttpGet]
    //[Route("{id}")]
    //public ActionResult GetById(int id)
    //{
    //    try
    //    {
    //        var result = _repositories.Get(id);
    //        return result == null
    //            ? Ok(new { statusCode = 200, message = $"Data Id = {id} Tidak Ditemukan !!" })
    //            : Ok(new { statusCode = 200, message = $"Data {id} Berhasil Diterima", data = result });
    //    }
    //    catch
    //    {
    //        return BadRequest(new { statusCode = 500, message = "Something Wrong I Can Fell It !!" });
    //    }
    //}

    //[HttpPost]
    //public ActionResult Insert(AccountRole accountrole)
    //{
    //    try
    //    {
    //        var result = _repositories.Insert(accountrole);
    //        return result == null
    //            ? Ok(new { message = "Data Gagal Ditambahkan!" })
    //            : Ok(new { message = "Data Berhasil Ditambahkan!" });
    //    }
    //    catch
    //    {
    //        return BadRequest(new { message = "Failed To Insert Check Out Yout Property" });
    //    }
    //}

    //[HttpPut]
    //public ActionResult Update(AccountRole accountrole)
    //{
    //    try
    //    {
    //        var result = _repositories.Update(accountrole);
    //        return result == 0
    //            ? Ok(new { message = $"Id {accountrole.Id} Tidak Ditemukan" })
    //            : Ok(new { message = "Data Berhasil Diubah!" });
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(new { message = "Something Wrong Sorry" + ex });
    //    }
    //}

    //[HttpDelete]
    //public ActionResult Delete(int id)
    //{
    //    try
    //    {
    //        var result = _repositories.Delete(id);
    //        Convert.ToString(result);
    //        return result == 0
    //            ? Ok(new { message = $"Id {id} Tidak Ditemukan" })
    //            : Ok(new { message = "Data Berhasil Dihapus" });
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(new { message = "Something Wrong Sorry" + ex });
    //    }
    //}
}
