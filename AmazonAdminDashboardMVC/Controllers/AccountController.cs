using AmazonAdmin.Application.Services;
using AmazonAdmin.Domain;
using AmazonAdmin.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AmazonAdminDashboardMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IUserService _UserService;
        private readonly IMapper _Mapper;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        public AccountController(UserManager<ApplicationUser> userManager,IUserService userService,IMapper mapper,SignInManager<ApplicationUser> signInManager)
        {
            _UserManager = userManager;
            _UserService = userService;
            _Mapper = mapper;
            _SignInManager = signInManager;
        }

        

        public async Task<IActionResult> Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(UserRegisterDTO user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationuser = _Mapper.Map<ApplicationUser>(user);
                IdentityResult result=await _UserManager.CreateAsync(applicationuser,user.Password);
                if (result.Succeeded)
                {
                    try
                    {
						await _UserManager.AddToRoleAsync(applicationuser, "Admin");
					}catch(Exception ex)
                    {
						ModelState.AddModelError("", "Error On Adding Role");
					}
                   await _SignInManager.SignInAsync(applicationuser, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginDTO user)
        {
            if (ModelState.IsValid)
            {
               ApplicationUser usermodel= await _UserManager.FindByNameAsync(user.userName);
                if(usermodel != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult res =await _SignInManager.PasswordSignInAsync(usermodel, user.Password,user.RememberMe,false);
                    if (res.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong User Name Or Password !");
                }
            }
            return View(user);
        }
        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync(); 
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Profile()
        {
            string UsrId = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value;
            var user =await _UserManager.FindByIdAsync(UsrId);
            if(user != null)
            {
                return View(_Mapper.Map<UserRegisterDTO>(user));
            }
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(UserRegisterDTO userRegisterDTO)
        {
            string UsrId = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _UserManager.FindByIdAsync(UsrId);
            if (user != null)
            {
                user.UserName = userRegisterDTO.userName;
                user.Phone = userRegisterDTO.Phone;
                user.EmailAddress = userRegisterDTO.EmailAddress;
                var res=await _UserManager.UpdateAsync(user);
                if (res.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(userRegisterDTO);
                }
            }
            return NotFound();
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changepass)
        {
            if (ModelState.IsValid)
            {
                string UsrId = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value;
                var user = await _UserManager.FindByIdAsync(UsrId);
                if (user != null)
                {
                    var checkpassres = await _UserManager.CheckPasswordAsync(user, changepass.OldPassword);
                    if (checkpassres)
                    {
                        var Change = await _UserManager.ChangePasswordAsync(user, changepass.OldPassword, changepass.NewPassword);
                        if (Change.Succeeded)
                        {
                            return RedirectToAction("Profile");
                        }
                        else
                        {
                            return View(changepass);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("OldPassword", "Error password !");
                    }
                }
            }
            return View();
        }
    }
}
