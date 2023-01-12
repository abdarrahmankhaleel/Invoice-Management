using Domain;
using Domain.Entity;
using Infrastructure.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class InvoiceDbContext :IdentityDbContext<ApplicationUser>
    {
        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchCategory> BranchCategories  { get; set; }    
        public DbSet<InvoiceTemp> InvoiceTemps  { get; set; }    
        public DbSet<PurchaseInvoice> PurchaseInvoices  { get; set; }    
        public DbSet<PurchaseInvoiceItem> PurchaseInvoiceItems  { get; set; }    
    }
}
