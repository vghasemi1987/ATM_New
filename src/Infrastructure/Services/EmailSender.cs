using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using DomainContracts.Commons;
using DomainContracts.SettingAggregate;

namespace Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IFileService _fileService;
        private readonly ISettingRepository _settingRepository;

        public EmailSender(/*IOptions<AuthMessageSenderOptions> optionsAccessor, */
            IFileService fileService,
            ISettingRepository settingRepository)
        {
            _fileService = fileService;
            _settingRepository = settingRepository;
            //Options = optionsAccessor.Value;
        }

        //public AuthMessageSenderOptions Options { get; }

        public Task SendEmailAsync(string email, string subject, string message, string fileAttachment)
        {
            Execute(email, subject, message, fileAttachment).Wait();
            return Task.FromResult(0);
        }

        private async Task Execute(string email, string subject, string message, string fileAttachment)
        {
            var mailoptions = await _settingRepository.FindByIdAsync(1);
            var mail = new MailMessage(new MailAddress(mailoptions.EmailUsername), new MailAddress(email))
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = message,
                Priority = MailPriority.High
            };

            if (fileAttachment.Length > 0)
            {
                if (_fileService.FileExist(fileAttachment))
                {
                    var attachment = new Attachment(fileAttachment, MediaTypeNames.Application.Octet);
                    mail.Attachments.Add(attachment);
                }
                //var fileName = Path.GetFileName(fileAttachment.FileName);
                //mail.Attachments.Add(new Attachment(fileAttachment.OpenReadStream(), fileName));
            }

            var smtp = new SmtpClient(mailoptions.EmailSmtpServer, mailoptions.EmailPortNumber.GetValueOrDefault())
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mailoptions.EmailUsername, mailoptions.EmailPassword),
                EnableSsl = true
            };
            await smtp.SendMailAsync(mail);
            //return Task.CompletedTask;
        }
    }
}
