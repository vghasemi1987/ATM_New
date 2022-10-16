using System.ComponentModel.DataAnnotations;

namespace Web.Core.Transactions.ViewModels
{
    public class SearchViewModel
    {
        [Display(Name = "از تاریخ")]
        public string FromDate { get; set; }
        [Display(Name = "تا تاریخ")]
        public string ToDate { get; set; }
    }
}
