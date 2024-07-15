using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReactNetCoreSSO.Server.Controllers.Admin
{
    public class PingController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Admin, It Works!");
        }
    }
}
