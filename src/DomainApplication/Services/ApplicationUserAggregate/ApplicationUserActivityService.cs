using System.Threading.Tasks;
using DomainContracts.ApplicationUserAggregate;
using DomainContracts.Commons;
using DomainEntities.ApplicationUserAggregate;

namespace DomainApplication.Services.ApplicationUserAggregate
{
    public class ApplicationUserActivityService : IApplicationUserActivityService
    {
        private readonly IAsyncRepository<ApplicationUserActivity> _asyncRepository;
        private readonly IRepository<ApplicationUserActivity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationUserActivityService(IAsyncRepository<ApplicationUserActivity> asyncRepository,
            IRepository<ApplicationUserActivity> repository,
            IUnitOfWork unitOfWork)
        {
            _asyncRepository = asyncRepository;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Save(ApplicationUserActivity model)
        {
            if (model.Id > 0)
            {
                //==== UPDATE ====
                _repository.Update(model);
            }
            else
            {
                //==== INSERT ====
                _repository.Add(model);
            }
            await _unitOfWork.SaveAsync();
        }
    }
}
