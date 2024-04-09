using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LoyaltyPointsExchangeSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set;}
        [Display(Name = "Gender")]
        public string gender { get; set; }
        public long? Points { get; set; }
        public List<PointsTransferHistory> pointsTransferHistory { get; set; }
    }
}
