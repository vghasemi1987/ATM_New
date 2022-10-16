using System.Threading.Tasks;
using DomainContracts.Commons;
using DomainContracts.SettingAggregate;
using DomainEntities.SettingAggregate;

namespace DomainApplication.Services.SettingAggregate
{
    public class SettingService : ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAsyncRepository<Setting> _asyncRepository;
        private readonly IRepository<Setting> _repository;

        public SettingService(IUnitOfWork unitOfWork,
            IAsyncRepository<Setting> asyncRepository,
            IRepository<Setting> repository)
        {
            _unitOfWork = unitOfWork;
            _asyncRepository = asyncRepository;
            _repository = repository;
        }

        public async Task SaveEmailAsync(Setting model)
        {
            if (model != null)
            {
                _repository.Update(model, o => o.EmailPassword,
                    o => o.EmailPortNumber, o => o.EmailUsername, o => o.EmailSmtpServer, o => o.EnableSsl,
                    o => o.WebSiteTitle);
            }
            else
            {
                //==== INSERT ====
                _repository.Add(model);
            }
            await _unitOfWork.SaveAsync();
        }
        public async Task SaveSmsAsync(Setting model)
        {
            if (model != null)
            {
                _repository.Update(model, o => o.WelcomeText, o => o.SmsServiceNumber,
                    o => o.SmsUserName, o => o.SmsPassword, o => o.ThanksMsg);
                await _unitOfWork.SaveAsync();
            }
        }
        public async Task SaveAppAsync(Setting model)
        {
            if (model != null)
            {
                _repository.Update(model, o => o.WebSiteTitle);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
