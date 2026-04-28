using Microsoft.AspNetCore.Mvc;
using My_Port.Data;
using My_Port.Dto;
using My_Port.Models;

namespace My_Port.Controllers
{
    public class RoleController(ApplicationDBContext _context) : Controller
    {
        public IActionResult Index()
        {
            var list= _context.Roles.Select(x=> new RoleDto
            {
                RoleName=x.RoleName,
                RoleDescription=x.RoleDescription,
                RoleId=x.RoleId
            }).ToList();

            return View(list);
        }
        public IActionResult AddRole()
        {
            return View();
        }

        public async Task<IActionResult> AddRoleDetails(RoleDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.RoleName))
            {
                ViewBag.Message = "Please Fill All the Details in the form";
                return View("AddRole");
            }

            var isexist = _context.Roles.Any(e => e.RoleName == dto.RoleName);
            if (isexist)
            {
                ViewBag.Message = "Role already exists";
                return View("AddRole");
            }

            _context.Roles.Add(new ad_Role
            {
                RoleName = dto.RoleName!,          // safe because we checked above
                RoleDescription = dto.RoleDescription
            });

            await _context.SaveChangesAsync();
            

            return RedirectToAction("Index");
        }

        public IActionResult RedirectToRoleForm()
        {
            return RedirectToAction("AddRole");
        }
    }
}
