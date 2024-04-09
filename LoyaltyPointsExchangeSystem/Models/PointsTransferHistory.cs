using System.ComponentModel.DataAnnotations;

namespace LoyaltyPointsExchangeSystem.Models
{
    public class PointsTransferHistory
    {
        public Guid Id { get; set; }
        [Display(Name ="Points In")]
        public long? pointIn { get; set; }
        [Display(Name = "Points Out")]
        public long? pointOut { get; set; }
        [Display(Name = "Transaction Date")]
        public DateTime transactionDate { get; set; }
        [Display(Name = "Total Points")]
        public long? totalPoints { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
