using AmazonAdmin.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AmazonAdminDashboardMVC.Controllers
{
	[Authorize(Roles ="Admin")]
	public class RoleController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		public RoleController(RoleManager<IdentityRole> roleManager)
		{
			_roleManager= roleManager;
		}
		public IActionResult AddRole()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddRole(RoleDTO role)
		{
			if (ModelState.IsValid) 
			{
				IdentityRole rolemodel= new IdentityRole();
				rolemodel.Name = role.RoleName;
				IdentityResult res = await _roleManager.CreateAsync(rolemodel);
				if (res.Succeeded)
				{
					return RedirectToAction("Index","Home");
				}
				else
				{
					ModelState.AddModelError("", res.Errors.FirstOrDefault().Description);
				}

			}
			return View(role);

		}
	}
}
