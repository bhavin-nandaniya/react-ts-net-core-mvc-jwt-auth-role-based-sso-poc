using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ReactNetCoreSSO.Server.Controllers.Admin
{
    [Authorize(Roles ="Admin")]
    public class DashController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
