using DomainEntities.HamrahLoan;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.HamrahLoanHeaders.ViewModels
{
    public class ChangeStatusViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} را انتخاب نمایید"),
            Display(Name = "وضعیت")]
        public HamrahLoanStatus Status { get; set; }
        public string Warning { get; set; }
    }
}
