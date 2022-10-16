using ApplicationCommon;
using DomainContracts.BranchAggregate;
using DomainContracts.Commons;
using DomainEntities.BranchAggregate;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Web.Core.Branches.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.Branches
{
    [Authorize]
    [DisplayName("مدیریت شعبه ها")]
    public class BranchesController : Controller
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BranchesController(IUnitOfWork unitOfWork,
            IBranchRepository branchRepository)
        {
            _unitOfWork = unitOfWork;
            _branchRepository = branchRepository;
        }
        [HttpPost]
        public JsonResult ValidateCode(int code, int? id)
        {
            var result = _branchRepository.CheckBranchExist(code, id);
            return Json(!result);
        }
        [Permission]
        [DisplayName("لیست نمایشی")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetRecords(string models)
        {
            var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
            var list = _branchRepository.GetList(request);
            return Json(list);
        }

        [HttpGet]
        [Permission]
        [DisplayName("مشاهده جزئیات")]
        public IActionResult GetDetail(short? id)
        {
            var model = new BranchViewModel
            {
                BranchHeadSelectList = new SelectList(_branchRepository.GetParentDepartments(), nameof(BranchDto.Id), nameof(BranchDto.Title))
            };
            if (id.HasValue)
            {
                var oData = _branchRepository.GetSingleBySpec(o => o.Id == id.Value);

                model.Id = oData.Id;
                model.Title = oData.Title;
                model.code = oData.Code;
                model.BranchHeadId = oData.BranchHeadId;
            }
            return PartialView("_Detail", model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission]
        [DisplayName("افزودن")]
        public async Task<IActionResult> AddDetail(BranchViewModel model)
        {
            var branch = new Branch
            {
                Title = model.Title,
                Code = model.code,
                BranchHeadId = model.BranchHeadId,
            };
            _branchRepository.Add(branch);
            await _unitOfWork.SaveAsync();
            return Json(new
            {
                Message = Message.Show("اطلاعات شعبه با موفقیت ثبت شد...", MessageType.Success),
                RefreshGrid = true
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission]
        [DisplayName("ویرایش")]
        public async Task<IActionResult> EditDetail(BranchViewModel model)
        {
            var department = new Branch
            {
                Id = model.Id.Value,
                Title = model.Title,
                Code = model.code,
                BranchHeadId = model.BranchHeadId,
            };
            _branchRepository.Update(department, o => o.Title, o => o.BranchHeadId,o=>o.Code);
            await _unitOfWork.SaveAsync();
            return Json(new
            {
                Message = Message.Show("اطلاعات شعبه با موفقیت ثبت شد...", MessageType.Success),
                RefreshGrid = true
            });
        }

        [HttpDelete]
        [Permission]
        [DisplayName("حذف")]
        public async Task<IActionResult> DeleteRows(List<short> ids)
        {
            if (!ids.Any()) return Json(false);
            foreach (var item in ids)
            {
                _branchRepository.Delete(new Branch { Id = item });
            }
            await _unitOfWork.SaveAsync();
            return Json(new
            {
                Message = Message.Show("رکورد های انتخابی با موفقیت حذف شدند ...", MessageType.Success),
                RefreshGrid = true
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetBranchesByBranchHeadId(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("Error");
            }

            var result = await _branchRepository.GetBranchList(id.Value);
            return Ok(result);
        }
    }
}