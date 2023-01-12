using Domain.Entity;
using Infrastructure.Data;
using Infrastructure.ViewModel;
using InvoiceProjectWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace InvoiceProjectWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InvoiceDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(ILogger<HomeController> logger,InvoiceDbContext context,UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Invoice()
        {
            var userId =  userManager.GetUserId(User);
            var user= await userManager.FindByIdAsync(userId);
            
           ViewBag.Suppliers=context.Suppliers.Where(x=>x.BranchId== user.BranchId&&x.CurrentState>0).ToList();
            ViewBag.BranchCategories = context.BranchCategories.Where(x=>x.BranchId == user.BranchId).Include(x=>x.Category).ToList();
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Invoice(PurchaseInvoice model)
        {
            var userId = userManager.GetUserId(User);
            var user = await userManager.FindByIdAsync(userId);
            if (!ModelState.IsValid)
            {
                ViewBag.Suppliers = context.Suppliers.Where(x => x.BranchId == user.BranchId && x.CurrentState > 0).ToList();
                ViewBag.BranchCategories = context.BranchCategories.Where(x => x.BranchId == user.BranchId).Include(x => x.Category).ToList();
                return View(model);
            }
            else
            {
                List<InvoiceTemp> lstInvoiceTemps = context.InvoiceTemps.Where(x => x.BranchId == user.BranchId && x.UserId == userId).ToList();
                model.UserId=userId;
                model.BranchId=user.BranchId;
                model.CreatedDate = DateTime.Now;
                model.CreateUserId = userId;
                foreach (var item in lstInvoiceTemps)
                {
                    model.PurchaseInvoiceItems.Add(new PurchaseInvoiceItem
                    {
                        CategoryId=item.CategoryId,
                        ProductId=item.ProductId,
                        PriceOrigin=item.PriceOrigin,
                        QtyChoosed=item.QtyChoosed,
                        QtyOrigin=item.QtyOrigin,
                        TotalPrice=item.TotalPrice, 
                    });
                    context.InvoiceTemps.Remove(item);
                    context.SaveChanges();
                }

                context.PurchaseInvoices.Add(model);
                context.SaveChanges();
                return RedirectToAction(nameof(Invoice));

            }

        }


        public IActionResult GetPriceProduct(int? id)
        {
            var PriceProduct = context.Products.FirstOrDefault(x => x.Id==id);
            return Ok(PriceProduct);
        }
        public async Task<IActionResult> GetAllTotalPrice()
        {
        var userId = userManager.GetUserId(User);
        var user = await userManager.FindByIdAsync(userId);
        var AllTotal = context.InvoiceTemps.Where(x => x.UserId == userId&&x.BranchId==user.BranchId)
            .Sum(x=>x.TotalPrice);

        return Ok(AllTotal);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}