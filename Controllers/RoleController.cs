using Microsoft.AspNetCore.Mvc;

namespace My_Port.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddRole()
        {
            return View();
        }

        public IActionResult RedirectToRoleForm()
        {
            return RedirectToAction("AddRole");
        }
    }
}
