using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Port.Data;
using My_Port.Dto;
using My_Port.Models;

namespace My_Port.Controllers
{
    public class RoleController(ApplicationDBContext _context) : Controller
    {
        public IActionResult Index()
        {
            var list = _context.Roles.Select(x => new RoleDto
            {
                RoleName = x.RoleName,
                RoleDescription = x.RoleDescription,
                RoleId = x.RoleId
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
                TempData["Error"]  = "Please Fill All the Details in the form";
                return View("AddRole");
            }

            var isexist = _context.Roles.Any(e => e.RoleName == dto.RoleName);
            if (isexist)
            {
                TempData["Info"] = "Role already exists";
                return View("AddRole");
            }

            _context.Roles.Add(new ad_Role
            {
                RoleName = dto.RoleName!,          // safe because we checked above
                RoleDescription = dto.RoleDescription
            });

            await _context.SaveChangesAsync();

            TempData["Success"] = "Role created successfully!";

            return RedirectToAction("Index");
        }

        public IActionResult RedirectToRoleForm()
        {
            return RedirectToAction("AddRole");
        }


        public async Task<IActionResult> UpdateRole(int id)
        {
            {
                var data = await _context.Roles.FirstOrDefaultAsync(x => x.RoleId == id);

                return View(new RoleDto
                {
                    RoleId = data!.RoleId,
                    RoleName = data.RoleName,
                    RoleDescription = data.RoleDescription
                });

            }
        }

        public async Task<IActionResult> UpdateRoleDetails(RoleDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.RoleName))
            {
                TempData["Info"] = "Please Fill All the Details in the form";
                return View("UpdateRole", new { id = dto!.RoleId });
            }
            var data = await _context.Roles.FirstOrDefaultAsync(x => x.RoleId == dto.RoleId);
            if (data == null)
            {
                TempData["Error"] = "Role not found";
                return View("UpdateRole", new { id = dto.RoleId });
            }
            else
            {
                data.RoleName = dto.RoleName;
                data.RoleDescription = dto.RoleDescription;
                _context.Roles.Update(data);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Role Updated successfully!";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            var data = await _context.Roles.FirstOrDefaultAsync(x => x.RoleId == id);
            if (data != null)
            {
                _context.Roles.Remove(data);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Role Deleted successfully!";
            }
            return RedirectToAction("Index");
        }
    }
}
