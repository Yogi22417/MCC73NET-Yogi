using API.Repositories.Interface;
using API.ViewModels;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using API.Base;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class AccountController : BaseController<AccountRepositories, Account, string>
{
    private AccountRepositories _repositories;
    private IConfiguration _con;
    public AccountController(AccountRepositories repositories, IConfiguration con) : base(repositories) {
        _repositories = repositories;
        _con= con;
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
    //public ActionResult Insert(Account account)
    //{
    //    try
    //    {
    //        var result = _repositories.Insert(account);
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
    //public ActionResult Update(Account account)
    //{
    //    try
    //    {
    //        var result = _repositories.Update(account);
    //        return result == 0
    //            ? Ok(new { message = $"Id {account.NIK} Tidak Ditemukan" })
    //            : Ok(new { message = "Data Berhasil Diubah!" });
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(new { message = "Something Wrong Sorry" + ex });
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

    [AllowAnonymous]
    [HttpPost]
    [Route("Login")]
    public ActionResult Login(LoginVM entity)
    {
        try
        {
            var result = _repositories.Login(entity);
            switch (result)
            {
                case 0:
                    return Ok(new { message = $"Akun Dengan Email : {entity.Email} Tidak Ditemukan" });
                case 1:
                    return BadRequest(new { message = "Password Yang Anda Masukkan Salah" });
                default:
                    var roles = _repositories.UserRoles(entity.Email);

                    var claims = new List<Claim>()
                    {
                        new Claim("email", entity.Email)
                    };

                    foreach (var item in roles)
                    {
                        claims.Add(new Claim("role", item));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_con["JWT:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _con["JWT:Issuer"],
                        _con["JWT:Audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials:signIn
                        );
                    var generateToken = new JwtSecurityTokenHandler().WriteToken(token);
                    claims.Add(new Claim("Token Security", generateToken.ToString()));

                    return Ok(new { statusCode = 200, message = "Login Success!", data = generateToken });
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Something Wrong Sorry" + ex.Message });
        }
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("Register")]
    public ActionResult Regist(RegistrasiVM regis)
    {
        try
        {
            var result = _repositories.Registrasi(regis);
            if (result == 0)
            {
                return Ok(new { message = $"Email {regis.Email} Sudah Terdaftar ! Silahkan gunakan email lain" });
            }
            else if (result == 1)
            {
                return Ok(new { message = $"Nomor Telepon {regis.Phone} Sudah Terdaftar ! Silahkan gunakan nomor telepon lain" });
            }
            else if (result == 2)
            {
                return Ok(new { message = "Anda Berhasil Registrasi" });
            }
            return Ok(new { message = "Akun Tidak Bisa Diregistrasi" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Something Wrong Sorry" + ex.Message });
        }
    }
}
