using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class InvoiceTemp : RelatedBranch
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        public int QtyOrigin { get; set; }
        public int QtyChoosed { get; set; }

        public decimal PriceOrigin { get; set; }
        public decimal TotalPrice { get; set; }

        public string UserId { get; set; } = "";

    }
}
