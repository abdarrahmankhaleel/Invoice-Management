using Domain.Entity;
using Infrastructure.Data;
using Infrastructure.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvoiceProjectWeb.ApiControllers
{
    [Route("api/Invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceDbContext invoiceDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public InvoiceController(InvoiceDbContext invoiceDbContext,UserManager<ApplicationUser> userManager
            ,SignInManager<ApplicationUser> signInManager)
        {
            this.invoiceDbContext = invoiceDbContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var userId=userManager.GetUserId(User);
                var user=await userManager.FindByIdAsync(userId);
                var invoiceTempsLst =invoiceDbContext.InvoiceTemps.Where(x=>x.BranchId==user.BranchId&&x.UserId==userId)
                    .Include(x=>x.Branch)
                    .Include(x=>x.Product)
                    .Include(x=>x.Category).ToList();
                return Ok(invoiceTempsLst);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ProductsByCategory = invoiceDbContext.Products.Where(x=>x.CategoryId==id).ToList();
            return Ok(ProductsByCategory);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InvoiceTemp model)
        {
            try
            {
                var userId = userManager.GetUserId(User);
                var user =await userManager.FindByIdAsync(userId);
                var invTempInDb = invoiceDbContext.InvoiceTemps.FirstOrDefault(x => x.ProductId == model.ProductId&&x.UserId==userId);
                var Product = invoiceDbContext.Products.FirstOrDefault(x => x.Id == model.ProductId);
                model.TotalPrice = (model.QtyChoosed / Product.Quantity) * Product.Price;
                if (invTempInDb == null)
                {
                    model.BranchId = user.BranchId;
                    model.UserId = userId;
                    model.QtyOrigin = Product.Quantity;
                    model.PriceOrigin = Product.Price;
                    invoiceDbContext.InvoiceTemps.Add(model);
                }
                else
                {
                    invTempInDb.QtyChoosed += model.QtyChoosed;
                    invTempInDb.TotalPrice += model.TotalPrice;
                    invoiceDbContext.InvoiceTemps.Update(invTempInDb);
                }
                invoiceDbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
           
            
            
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var invTemInDb = invoiceDbContext.InvoiceTemps.FirstOrDefault(x => x.Id == id);
                if (invTemInDb != null)
                {
                    invoiceDbContext.InvoiceTemps.Remove(invTemInDb);
                    invoiceDbContext.SaveChanges();
                }
                return RedirectToAction("invoice","home");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
                
            }
           

        }
    }
}
