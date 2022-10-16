using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.TransactionDetails.ViewModels
{
    public class SearchViewModel
    {
        [Display(Name = "از تاریخ")]
        public string FromDate { get; set; }
        [Display(Name = "تا تاریخ")]
        public string ToDate { get; set; }

        public SearchType SearchType { get; set; }
    }

    public enum SearchType
    {
        ShetabiSearch,
        SumOfBranch
    }
}