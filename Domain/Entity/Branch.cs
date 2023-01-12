using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Branch
    {
        public Branch()
        {
            BranchCategories=new List<BranchCategory>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Address { get; set; }
        public int CurrentState { get; set; } = 1;
        public string? CreateUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdateUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? DeleteUserId { get; set; }
        public DateTime? DeletedDate { get; set; }
        
        public List<BranchCategory>? BranchCategories { get; set; }
    }
}
