using Infarstuructre.ViewModel;
using Infrastructure.Data;
using Infrastructure.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceProjectWeb.Controllers
{
    public class AccountsController : Controller
    {
        private readonly InvoiceDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountsController(InvoiceDbContext context,UserManager<ApplicationUser> userManager
            ,SignInManager<ApplicationUser> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Register()
        {
            ViewBag.Branches=context.Branches.Where(x=>x.CurrentState>0).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(VmRegister model)
        {
            if(!ModelState.IsValid)
                return View(model);
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                Email=model.Email,
                UserName=model.Email,
                BranchId=model.BranchId,
            };
            var result=await userManager.CreateAsync(user,model.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return RedirectToAction("Invoice", "Home");
            }

            return View(model);

        }


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
            }
            ViewBag.errorMessge = false;
            return View(model);
        }

       
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));

        }

    }
}
