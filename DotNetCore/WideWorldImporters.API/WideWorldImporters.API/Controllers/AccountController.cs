using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;
using WideWorldImporters.API.Models;

namespace WideWorldImporters.API.Controllers
{
    [EnableCors("AllowOrigin", "*", "*")]
    [Route("rest/account")]
    [ApiController]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class AccountController : ControllerBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        [HttpPost("login")]
        public ActionResult Login(User user)
        {
            string userName = user.UserName;
            string password = user.Passowrd;
            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject("User Logined Successfully"));
        }
    }
}