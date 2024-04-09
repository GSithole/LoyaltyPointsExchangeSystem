using System.ComponentModel.DataAnnotations;
using System.Runtime.ExceptionServices;

namespace LoyaltyPointsExchangeSystem.Models
{
    public class ProfileViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string? Email { get; set; }
        [Display(Name = "Points Balance")]
        public long Points { get; set; }

        public List<PointsTransferHistory> pointsTransferHistory { get; set; }
    }
}
