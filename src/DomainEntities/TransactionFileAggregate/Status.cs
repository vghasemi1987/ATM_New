using DomainEntities.Commons;
using System.ComponentModel;

namespace DomainEntities.TransactionFileAggregate
{
    public class Status : Entity<EnumStatus>
    {
        public string Title { get; set; }
    }

    public enum EnumStatus : short
    {
        [Description("پردازش شده")]
        OperatorProcessing = 1,//متصدی
        [Description("ارسال به رئیس شعبه")]
        SendToBranchBoss = 2,//رئیس شعبه
        [Description("ارسال به حسابدار")]
        SendToAccounting = 3,//حسابدار
        [Description("ثبت نهایی")]
        FinalRegistration = 4,//حسابدار
        [Description("بازگشت به متصدی")]
        BackToOperator = 5,//متصدی
        [Description("بازگشت به رئیس شعبه")]
        BackToBranchBoss = 6//رئیس شعبه
    }
}
