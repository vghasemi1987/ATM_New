using DomainContracts.BranchAggregate;
using DomainEntities.BranchAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.BranchAggregate
{
    public class BranchRepository : EfRepository<Branch>, IBranchRepository
    {
        private DbSet<Branch> DbSet { get; }
        public BranchRepository(AtmContext dbContext) : base(dbContext)
        {
            DbSet = dbContext.Set<Branch>();
        }

        //public Task<Branch> FindByIdAsync(int id)
        //{
        //    return DbSet.Where(o => o.Id == id)
        //        .FirstOrDefaultAsync();
        //}

        //public void Delete(IEnumerable<int> ids)
        //{
        //    foreach (var record in ids)
        //    {
        //        DbSet.Remove(new Branch { Id = record });
        //    }
        //}

        //public Task<List<BranchList>> GetBranchHeadList()
        //{
        //    return DbSet
        //        .Where(o => o.Parent == null)
        //        .Select(o => new BranchList
        //        {
        //            Id = o.Id,
        //            Name = o.Name
        //        })
        //        .AsNoTracking()
        //        .ToListAsync();
        //}

        public Task<List<BranchDto>> GetBranchList(int branchHead)
        {
            return DbSet
                .Where(o => o.BranchHeadId == branchHead)
                .AsNoTracking()
                .Select(o => new BranchDto
                {
                    Id = o.Id,
                    Title = $"{o.Code} - {o.Title}",
                    Code = o.Code,
                })
                .ToListAsync();
        }

        //public Task<List<BranchList>> GetListById(int id)
        //{
        //    return DbSet.Where(o => o.Id == id)
        //        .AsNoTracking()
        //        .Select(o => new BranchList
        //        {
        //            Id = o.Id,
        //            Name = o.Name
        //        })
        //        .ToListAsync();
        //}

        public DataSourceResult GetList(DataSourceRequest request)
        {
            return DbSet
                .Include(o => o.BranchHead)
                .Select(o => new BranchDto
                {
                    Id = o.Id,
                    Title = o.Title,
                    BranchHead = o.BranchHead.Title,
                    Code = o.Code,
                })
                .AsNoTracking()
                .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
        }

        public List<Branch> GetParentDepartments()
        {
            return DbSet
                .Where(o => o.BranchHead == null)
                .AsNoTracking()
                .ToList();
        }

        public bool CheckBranchExist(int code, int? id)
        {
            return DbSet.Any(o => (o.Code == code) && (!id.HasValue || o.Id != id));
        }

        public List<BranchDto> GetBranches()
        {
            return DbSet
                .Where(o => o.BranchHead != null)
                .OrderBy(x => x.BranchHead.Title)
                .ThenBy(x => x.Title)
                .Select(o => new BranchDto
                {
                    Title = o.Title,
                    BranchHead = o.BranchHead.Title,
                    Id = o.Id,
                    Code = o.Code,
                })
                .AsNoTracking()
                .ToList();
        }

        public List<BranchDto> GetChildDepartmentsByDepartmentId(short? departmentId)
        {
            return DbSet
                .Where(o => o.BranchHeadId == departmentId || o.Id == departmentId)
                .OrderBy(x => x.BranchHead.Title)
                .ThenBy(x => x.Title)
                .Select(o => new BranchDto
                {
                    Title = o.Title,
                    BranchHead = o.BranchHead.Title,
                    Id = o.Id,
                    Code = o.Code,
                })
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Branch> GetAllBranches(string type)
        {
            if (type == "branch")
            {
                var data = DbSet.Where(o => o.BranchHeadId != null)
                    .Select(o => new Branch
                    {
                        Id = o.Id,
                        Title = $"{o.Code} - {o.Title}",
                        BranchHeadId = o.BranchHeadId,
                        Code = o.Code
                    })
                    .ToList();
                return GetIndentList(data);
            }
            else if (type == "branchhead")
            {
                var data = DbSet.Where(o => o.BranchHeadId == null)
                    .Select(o => new Branch
                    {
                        Id = o.Id,
                        Title = $"{o.Code} - {o.Title}",
                        BranchHeadId = o.BranchHeadId,
                        Code = o.Code
                    })
                    .ToList();
                return GetIndentList(data);
            }
            else
            {
                var data = DbSet.ToList();
                return GetIndentList(data);
            }
        }

        private IEnumerable<Branch> GetIndentList(List<Branch> departments, int? parentId = null, string space = "", List<Branch> catList = null)
        {
            catList = catList ?? new List<Branch>();
            var parentList = departments.Where(p => p.BranchHeadId == parentId).ToList();
            foreach (var item in parentList)
            {
                item.Title = space + item.Title;
                catList.Add(item);
                GetIndentList(departments, item.Id, space + "\xA0\xA0\xA0\xA0", catList);
            }
            return catList;
        }
        public string GetBranchCode(int id)
        {
            return DbSet.FirstOrDefault(o => o.Id.Equals(id)).Code.ToString();
        }

        public List<string> GetBranchCodesByHeadId(int headId)
        {
            return DbSet.Where(o => o.BranchHeadId.Equals(headId)).Select(o => o.Code.ToString()).ToList();
        }


        public IEnumerable<BranchDto> GetAllBranchHeadAndBranchSort()
        {
            var data = DbSet
                .Include(o => o.BranchHead)
                .Select(o => new BranchDto
                {
                    Id = o.Id,
                    Title = o.Title,
                    BranchHeadId = o.BranchHeadId,
                    Code = o.Code,
                    BranchHead = o.BranchHead.Title,
                    BranchHeadCode = o.BranchHead.Code
                })
                .OrderBy(o => o.BranchHeadId)
                .ToList();

            data.ForEach(o => o.Title = !(o.BranchHeadId > 0) ? $"{o.Code} - {o.Title}" : $"{o.BranchHeadCode} - {o.BranchHead} - {o.Code} - {o.Title}");

            return data;
        }
        public int? GetBranchHeadIdById(int Id)
        {
            return DbSet
                .FirstOrDefault(o => o.Id.Equals(Id)).BranchHeadId;
        }
    }
}