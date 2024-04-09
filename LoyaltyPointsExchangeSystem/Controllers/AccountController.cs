using LoyaltyPointsExchangeSystem.Models;
using LoyaltyPointsExchangeSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoyaltyPointsExchangeSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index","home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            List<string> Gender = new List<string>()
            { "Male","Female","Other"};
            ViewBag.Gender = new SelectList(Gender);
            return View();
        }

        //[HttpPost]
        //[HttpGet]
        [AcceptVerbs("Get","Post")]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user =  await userManager.FindByEmailAsync(email);
            if(user == null)
                return Json(true);
            return Json($"Email {email} is already in use");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { firstName = model.FirstName,lastName = model.LastName,gender = model.Gender, UserName = model.Email, Email = model.Email };
                var results = await userManager.CreateAsync(user, model.Password);

                if (results.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("index","home");
                }
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var results = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (results.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                ModelState.AddModelError("", "Invalid login attempt");

            }
            return View(model);
        }
    }
}
