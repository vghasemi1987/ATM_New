using DomainEntities.TransactionFileAggregate;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.ATMUnknown.ViewModels
{
    public class ToNextStepViewModel
    {
        //[HiddenInput]
        public List<int> Ids { get; set; }
        public EnumStatus Status { get; set; }

        [Display(Name = "متن پیام"), MaxLength(50, ErrorMessage = "حداکثر {0} کاربر باید {1} کاراکتر باشد")]
        public string MessageText { get; set; }
        public int CustomerOrBreanchId { get; set; }
    }
}