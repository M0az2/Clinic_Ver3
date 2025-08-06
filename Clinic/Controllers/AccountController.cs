using Clinic.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinic.Controllers
{
    public class AccountController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Email, string password)
        {

            var doctor = db.Doctors.SingleOrDefault(d => d.Email == Email);

            if (doctor == null || doctor.Password != password) return Unauthorized("Invalid email or password.");

            // Claims
            List<Claim> claims;
            if (doctor.Specialization == "Admin")
            {
                 claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, doctor.FullName),
                    new Claim(ClaimTypes.Email, doctor.Email),
                    new Claim("DoctorId", doctor.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Admin")
                };
            }
            else
            {
                claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, doctor.FullName),
                    new Claim(ClaimTypes.Email, doctor.Email),
                    new Claim("DoctorId", doctor.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Doctor")
                };
            }
            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MyCookieAuth", principal);

            Response.Cookies.Append("Id", doctor.Id.ToString());

            return RedirectToAction(controllerName: "Home", actionName: "Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Login");
        }
    }
}
