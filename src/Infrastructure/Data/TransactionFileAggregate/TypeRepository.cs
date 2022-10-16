using System.Collections.Generic;
using System.Threading.Tasks;
using DomainContracts.TransactionAggregate;
using DomainEntities.TransactionFileDetailAggregate;
using Infrastructure.Data.Commons;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.TransactionFileAggregate
{
    public class TypeRepository : EfRepository<Type>, ITypeRepository
    {
        private DbSet<Type> DbSet { get; }
        public TypeRepository(AtmContext dbContext) : base(dbContext)
        {
            DbSet = dbContext.Set<Type>();
        }

        public Task<List<Type>> GetList()
        {
            return DbSet
                .AsNoTracking()
                .ToListAsync();
        }
    }
}