using System.ComponentModel.DataAnnotations;

namespace LoyaltyPointsExchangeSystem.ViewModels
{
    public class TransferPointsViewModel
    {
        [Display(Name ="Enter points to be transfered")]
        public long pointsToTransfer { get; set; }
        [Display(Name ="Select user to transfer points to")]
        public string user { get; set; }
    }
}
