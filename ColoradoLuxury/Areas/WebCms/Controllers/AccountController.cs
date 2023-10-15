using ColoradoLuxury.Areas.WebCms.VM;
using ColoradoLuxury.Core.Cryptography;
using ColoradoLuxury.Extensions;
using ColoradoLuxury.Models.BLL;
using ColoradoLuxury.Models.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Stripe.Terminal;

namespace ColoradoLuxury.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    public class AccountController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ColoradoContext _context;

        public AccountController(IConfiguration configuration, ColoradoContext context)
        {
            this.configuration = configuration;
            this._context = context;
        }
        [Route("/webCms/admin")]
        public IActionResult Login()
        {
            AdminDetailsVM admin = new AdminDetailsVM();
            return View(admin);
        }

        [HttpPost]
        [Route("/webCms/admin")]
        public IActionResult Login(AdminDetailsVM admin)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongLoginOrPassword", "Username or password is wrong!");
                return View(admin);
            }

            if (admin.Username.Trim().Encrypt() == configuration.GetValue<string>("Admin:Username")
                    && admin.Password.Encrypt() == configuration.GetValue<string>("Admin:Password"))
            {
                HttpContext.SetSessionString("admin", "admin-authorize");

                SessionLogAdminUser logAdminUser = new SessionLogAdminUser()
                {
                    Username = configuration.GetValue<string>("Admin:Username"),
                    Password = configuration.GetValue<string>("Admin:Password"),
                    LoggedIn= true
                };

                _context.SessionLogAdminUsers.Add(logAdminUser);
                _context.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("WrongLoginOrPassword", "Username or password is wrong!");
                return View(admin);
            }

            return RedirectToAction
                ("Index", "Home", new { areas = "WebCms" });
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction
                ("Login", "Account", new { areas = "WebCms" });
        }
    }
}
