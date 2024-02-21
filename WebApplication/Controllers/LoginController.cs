using ASPNET_HHRR_Vacations.Models;
using ASPNET_HHRR_Vacations.Services.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Security.Claims;

namespace ASPNET_HHRR_Vacations.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly EnterpriseContext _enterpriseContext;
        private readonly IAuthService _authService;
        public LoginController(EnterpriseContext enterpriseContext, IAuthService authService)
        {
            _enterpriseContext = enterpriseContext;
            _authService = authService;
        }

        [NonAction]
        private async Task<ActionResult> UserRedirect()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var someUser = await _enterpriseContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (someUser == null)
                return View("Index");

            return User.IsInRole("Admin") ?
                RedirectToAction("Index", "AdminHome") :
                RedirectToAction("Index", "EmployeeHome");
        }
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
                await UserRedirect();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<UserCredential>> Credentials(UserCredential loginCredentials)
        {
            try
            {
                var authResult = await _authService.VerifyCredentials(loginCredentials);

                if (!authResult.IsSuccess)
                {
                    ModelState.AddModelError("", authResult.ErrorMessage);
                    return View("Index");
                }

                UserCredential? user = authResult.ObjectResult;

                var employee = await _enterpriseContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == user.EmployeeId);
                var claims = new List<Claim>
                {
                    new (ClaimTypes.Role, user.IsAdmin ? "Admin" : "User"),
                    new (ClaimTypes.Name, employee.CompleteName),
                    new (ClaimTypes.NameIdentifier, user.EmployeeId.ToString()),
                    new (ClaimTypes.Email,user.Email)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {

                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", user.IsAdmin ? "AdminHome" : "EmployeeHome");
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError("", "An error has occurred with the database request.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError("", "An error has occurred.");
            }

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
