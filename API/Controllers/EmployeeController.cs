using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class EmployeeController : BaseController<EmployeeRepositories,Employee, string>
{
    private EmployeeRepositories _repositories;
    public EmployeeController(EmployeeRepositories repositories) : base(repositories) { _repositories = repositories; }

    //[Authorize(Roles = "Manager, Admin")]
    [HttpGet]
    [Route("Master")]
    [AllowAnonymous]
    public ActionResult GetMasters()
    {        try
        {
            var result = _repositories.MasterEmployeeData();
            return result == null
                ? Ok(new { statusCode = 200, message = "Data Tidak Ditemukan !!" })
                : Ok(new { statusCode = 200, message = "Data Berhasil Diterima", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { statusCode = 500, message = "Something Wrong I Can Fell It !!" + ex.Message});
        }
    }

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
    //[Route("{NIK}")]
    //public ActionResult GetById(string NIK)
    //{
    //    try
    //    {
    //        var result = _repositories.Get(NIK);
    //        return result == null
    //            ? Ok(new { statusCode = 200, message = $"Data Id = {NIK} Tidak Ditemukan !!" })
    //            : Ok(new { statusCode = 200, message = $"Data {NIK} Berhasil Diterima", data = result });
    //    }
    //    catch
    //    {
    //        return BadRequest(new { statusCode = 500, message = "Something Wrong I Can Fell It !!" });
    //    }
    //}

    //[HttpPost]
    //public ActionResult Insert(Employee employee)
    //{
    //    try
    //    {
    //        var result = _repositories.Insert(employee);
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
    //public ActionResult Update(Employee employee)
    //{
    //    try
    //    {
    //        var result = _repositories.Update(employee);
    //        return result == 0
    //            ? Ok(new { message = $"Id {employee.NIK} Tidak Ditemukan" })
    //            : Ok(new { statusCode = 200, message = "Data Berhasil Diubah!", data = result  });
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(new { message = "Something Wrong Sorry" + ex.Message });
    //    }
    //}

    //[HttpDelete]
    //public ActionResult Delete(string NIK)
    //{
    //    try
    //    {
    //        var result = _repositories.Delete(NIK);
    //        Convert.ToString(result);
    //        return result == 0
    //            ? Ok(new { message = $"Id {NIK} Tidak Ditemukan" })
    //            : Ok(new { message = "Data Berhasil Dihapus" });
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(new { message = "Something Wrong Sorry" + ex });
    //    }
    //}
}
