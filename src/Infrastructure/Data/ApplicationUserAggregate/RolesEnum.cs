
using System.ComponentModel;

namespace Infrastructure.Data.ApplicationUserAggregate
{
    public enum RolesEnum
    {
        [Description("توسعه دهنده")]
        Developer = 1,
        [Description("حسابدار")]
        Accounting = 2,
        [Description("امور شعب")]
        BranchHead = 3,
        [Description("رئیس شعبه")]
        BranchBoss = 4,
        [Description("متصدی")]
        Operator = 5,
        [Description("ادمین تراکنش های نامعلوم")]
        AdminATMUnknown = 6,
        [Description("ادمین وام همراه")]
        AdminHamrahLoan = 7,
    }
}
