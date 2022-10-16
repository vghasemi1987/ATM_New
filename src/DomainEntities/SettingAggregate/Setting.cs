
using DomainEntities.Commons;

namespace DomainEntities.SettingAggregate
{
    public class Setting : Entity<short>
    {
        public string EmailUsername { get; set; }
        public string EmailPassword { get; set; }
        public string EmailSmtpServer { get; set; }
        public short? EmailPortNumber { get; set; }
        public bool? EnableSsl { get; set; }
        public string WelcomeText { get; set; }
        public string WebSiteTitle { get; set; }
        public string SmsServiceNumber { get; set; }
        public string SmsUserName { get; set; }
        public string SmsPassword { get; set; }
        public string ThanksMsg { get; set; }
    }
}