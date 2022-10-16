using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.TransactionAggregate;
using DomainEntities.TransactionFileAggregate;
using DomainEntities.TransactionFileDetailAggregate;
using DomainEntities.WorkfollowAggregate;
using Infrastructure.Data.ApplicationUserAggregate;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Web.Core.Transactions.ViewModels;
using Web.Extensions;
using Web.Extensions.Attributes;
using File = DomainEntities.TransactionFileAggregate.File;
using Type = DomainEntities.TransactionFileDetailAggregate.Type;

namespace Web.Core.Transactions
{
	[Authorize]
	[DisplayName("مدیریت فایل تراکنش ها")]
	public class TransactionsController : Controller
	{
		private readonly IFileRepository _fileRepository;
		private readonly ITransactionService _transactionService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ITypeRepository _typeRepository;

		public TransactionsController(IFileRepository transactionRepository,
			IUnitOfWork unitOfWork,
			ITypeRepository typeRepository,
			ITransactionService transactionService)
		{
			_fileRepository = transactionRepository;
			_unitOfWork = unitOfWork;
			_typeRepository = typeRepository;
			_transactionService = transactionService;
		}

		[Permission]
		[DisplayName("لیست فایل تراکنش ها")]
		public IActionResult Index()
		{
			return View();
		}

		[Permission]
		[DisplayName("لیست تمامی تراکنش ها")]
		public IActionResult All()
		{
			return View();
		}

		public IActionResult GetFozoniFiles(string models, SearchViewModel searchModel)
		{
			if (User.IsInRole(RolesEnum.Operator.DescriptionAttr()))
			{
				var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
				return Json(_fileRepository.GetList(request, User.GetUserId(), null, null, null));
			}
			else
			{
				var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
				return Json(_fileRepository.GetList(request, null, null, searchModel.FromDate?.ToMiladiDate(), searchModel.ToDate?.ToMiladiDate()));
			}
		}

		//public async Task<DataSourceResult> GetBranchFiles()
		//{
		//    var dataString = HttpContext.GetJsonDataFromQueryString();
		//    var request = JsonConvert.DeserializeObject<DataSourceRequest>(dataString);
		//    var list = await _fileRepository.GetList(request, null, User.GetBranchId());
		//    return new DataSourceResult
		//    {
		//        Data = list,
		//        Total = list.Count
		//    };
		//}

		//public IActionResult GetAllFiles(string models)
		//{
		//var dataString = HttpContext.GetJsonDataFromQueryString();
		//var request = JsonConvert.DeserializeObject<DataSourceRequest>(dataString);

		//var list = await _fileRepository.GetList(request, null, null);
		//return new DataSourceResult
		//{
		//    Data = list,
		//    Total = list.Count
		//};


		//return Json(list);
		//}

		[HttpGet]
		//[Permission]
		[DisplayName("ثبت فایل فزونی")]
		public IActionResult UploadFile()
		{
			var model = new UploadFileViewModel();
			return PartialView("_Detail", model);
		}

		[HttpPost, DisableRequestSizeLimit]
		public async Task<IActionResult> UploadFile(UploadFileViewModel model)
		{
			using (var stream = model.PostedFile.OpenReadStream())
			{
				using (var binaryReader = new BinaryReader(stream))
				{
					var fileContent = binaryReader.ReadBytes((int)model.PostedFile.Length);
					Tuple<List<FileDetail>, List<FileDetail>> result;
					int? branchId = User.GetBranchId();
					int? branchHeadId = User.GetBranchHeadId();
					if (!(branchId > 0))
					{
						return Json(new
						{
							Message = Message.Show("شعبه کاربر شما مشخص نشده است، لطفا با اداره حسابداری تماس حاصل فرمایید", MessageType.Warning),
							RefreshGrid = true
						});
					}
					else if (!(branchHeadId > 0))
					{
						return Json(new
						{
							Message = Message.Show("امور شعب کاربر شما مشخص نشده است، لطفا با اداره حسابداری تماس حاصل فرمایید", MessageType.Warning),
							RefreshGrid = true
						});
					}
					using (var reader = new StreamReader(model.PostedFile.OpenReadStream()))
					{
						result = await ProcessFile(reader.ReadToEnd());
					}
					foreach (var item in result.Item2)
					{

						bool isNumeric = Int64.TryParse(item.CardNumber, out long n);
						if (!isNumeric || item.CardNumber.Length != 16)
						{
							return Json(new
							{
								Message = Message.Show($"شماره کارت {item.CardNumber} مورد قبول نیست.", MessageType.Error),
								RefreshGrid = true
							});
						}
					}
					if (result.Item2.Any())
					{
						var fileEntity = new File
						{
							Name = model.PostedFile.FileName,
							UserId = User.GetUserId(),
							StatusId = EnumStatus.OperatorProcessing,
							FileData = fileContent,
							BranchId = branchId.Value,
							FileDetails = result.Item2,
							BranchHeadId = branchHeadId.Value,
						};
						await _transactionService.Save(fileEntity);
					}
					return Json(new
					{
						Message = Message.Show($"فایل دارای {result.Item1.Count} رکورد تکراری و {result.Item2.Count} رکورد جدید است.", MessageType.Success),
						RefreshGrid = true
					});
				}
			}
		}

		[HttpDelete]
		[Permission]
		[DisplayName("حذف")]
		public async Task<IActionResult> DeleteRows(List<int> ids)
		{
			if (!ids.Any()) return Json(false);
			foreach (var item in ids)
			{
				_fileRepository.Delete(new File { Id = item });
			}
			await _unitOfWork.SaveAsync();
			return Json(new
			{
				Message = Message.Show("رکورد های انتخابی با موفقیت حذف شدند ...", MessageType.Success),
				RefreshGrid = true
			});
		}

		private async Task<Tuple<List<FileDetail>, List<FileDetail>>> ProcessFile(string fileText)
		{
			var fozoniTypes = new List<Type>();
			var repeatedTransaction = new List<FileDetail>();
			var fozoniTransaction = new List<FileDetail>();

			// File info as file type
			var fileTypes = await _typeRepository.GetList();

			//Filter Just approve File
			fozoniTypes.AddRange(from item in fileTypes
								 let regex = new Regex(item.Content)
								 where regex.Match(fileText).Success
								 select item);

			// ATM   پیدا کردن انواع     
			foreach (var item in fozoniTypes)
			{
				// پیدا کردن تراکنش های دارای فزونی 
				var transactionItems = (item.Extension == "jrn" || item.Extension == "txt") ?
						fileText.Split(new[] { item.Separation }, StringSplitOptions.None) :
						Regex.Split(fileText, item.Separation);
				var findFozoni = transactionItems.Where(a => Regex.Match(a, item.Content).Success).ToList();

				//پردازش هر تراکنش فزونی
				foreach (var fozoni in findFozoni)
				{
					//پیدا کردن کاراکتر اینتر در هر تراکنش  فزونی و ذخیره در آرایه
					var fozoniLines = fozoni.Split('\n').ToList();
					var lengh = fozoniLines.FirstOrDefault(o => o.Contains("<=="))?.IndexOf("<==");

					for (int i = 0; i < fozoniLines.Count; i++)
					{
						if (!lengh.HasValue)
							break;
						fozoniLines[i] = fozoniLines[i].Length >= lengh ?
							fozoniLines[i].Remove(0, lengh.Value) : fozoniLines[i];
					}

					var fozoniLiness = fozoniLines.Select((value, index) => new { value, index })
										.Where(a => a.value.Contains("  ATM"))
										.Select(a => a.index);



					foreach (var keyIndex in fozoniLiness)
					{
						var date = fozoniLines[keyIndex + 1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
						var hour = fozoniLines[keyIndex + 1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];

						//var tt = Convert.ToInt32(date.Split('/')[0]) > 31
						//                    ? ((date.Split('/')[0].Length > 2
						//                            ? date.Split('/')[0].Substring(0, 4)
						//                            : date.Split('/')[0]) + date.Split('/')[1] +
						//                        date.Split('/')[2]).Trim()
						//                    : (date.Split('/')[2] + date.Split('/')[1] +
						//                        (date.Split('/')[0].Length > 2
						//                            ? date.Split('/')[0].Substring(2, 2)
						//                            : date.Split('/')[0])).Trim();

						//var t1 = Convert.ToInt32(date.Split('/')[0]);
						//var t2 = date.Split('/')[0].Length;
						//var t3 = date.Split('/')[0].Substring(0, 4);
						//var t4 = date.Split('/')[0];
						//var t5 = date.Split('/')[1];
						//var t6 = date.Split('/')[2];
						//var t7 = date.Split('/')[1];
						//var t8 = date.Split('/')[0].Length;
						//var t9 = date.Split('/')[0].Substring(2, 2);
						//var t10 = date.Split('/')[0];

						var cardNumber = fozoniLines[keyIndex + 2].Split(new[] { ':', '\r' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

						if (cardNumber == "INVALID-CARD")
							continue;
						var transaction = new FileDetail
						{
							TypeId = item.Id,
							Date = Convert.ToInt32(date.Split('/')[0]) > 31
											? ((date.Split('/')[0].Length > 2
													? date.Split('/')[0].Substring(0, 4)
													: date.Split('/')[0]) + date.Split('/')[1] +
												date.Split('/')[2]).Trim()
											: (date.Split('/')[2] + date.Split('/')[1] +
												(date.Split('/')[0].Length > 2
													? date.Split('/')[0].Substring(2, 2)
													: date.Split('/')[0])).Trim(),
							Time = hour.Replace(":", string.Empty).Trim(),
							Operation = fozoniLines[keyIndex + 1].Split(new[] { ' ' },
								StringSplitOptions.RemoveEmptyEntries)[2].Trim(),
							AtmCode = fozoniLines[keyIndex + 1].Split(new[] { ' ', '\r' },
								StringSplitOptions.RemoveEmptyEntries)[3].Trim(),
							CardNumber = cardNumber,
							StatusId = EnumStatus.OperatorProcessing
						};

						//دومین سطر بعد

						var mAmount = fozoniLines[keyIndex + 4].Split(new[] { ':', '\r' }, StringSplitOptions.RemoveEmptyEntries)[1];
						mAmount = mAmount.Replace(" ", string.Empty).Trim();
						mAmount = mAmount.Replace(",", string.Empty).Trim();

						transaction.Amount = mAmount.ToDecimal();
						if (transaction.Amount <= 0)
							continue;

						try
						{
							//var data =  fozoniLines[keyIndex + 5];

							//var temp = new[] { ':','\r' };

							//var info01 = data.Split(temp,options:StringSplitOptions.RemoveEmptyEntries);

							//var result00 = info01[0] == null ? "": info01[0].Trim();
							//var result01 = info01[1] == null ? "" : info01[1].Trim();



							//var tt = fozoniLines[keyIndex + 5]
							//.Split(new[] { ':', '\r' },
							//StringSplitOptions.RemoveEmptyEntries)[1].Trim();

							string step01 = fozoniLines[keyIndex + 5];

							char[] middle = new[] { ':', '\r' };

							string step02 = step01.Split(middle,
											StringSplitOptions.RemoveEmptyEntries)[1].Trim();

							transaction.TransactionNumber =
											fozoniLines[keyIndex + 5]
											.Split(new[] { ':', '\r' },
											StringSplitOptions.RemoveEmptyEntries)[1].Trim();
						}
						catch (Exception ex)
						{
							string exception = ex.Message;
							string innerException = ex.InnerException?.Message;
							continue;
							//throw;
						}






						if (transaction.TransactionNumber.Length <= 0)
							continue;

						if (fozoniLines[keyIndex + 6].StartsWith("="))
							continue;

						transaction.ResponseCode = fozoniLines[keyIndex + 6].Split(new[] { ':', '\r' }, StringSplitOptions.RemoveEmptyEntries)[1].Substring(0, 3).Trim();
						var CheckSuccessCode = fozoniLines[keyIndex + 6].Split(new[] { ':', '\r' }, StringSplitOptions.RemoveEmptyEntries)[1].Substring(0, 4).Trim();
						if (CheckSuccessCode != "000")
							continue;
						transaction.IsRefahi = transaction.CardNumber.Substring(0, 6) == "589463";

						var isTransactionExist = await _transactionService.CheckTransactionExist(transaction.CardNumber,
							transaction.TransactionNumber);

						if (!isTransactionExist)
						{
							repeatedTransaction.Add(transaction);
						}
						else
						{
							transaction.Workfollows.Add(new Workfollow
							{
								StatusId = EnumStatus.OperatorProcessing,
								UserId = User.GetUserId(),
							});
							fozoniTransaction.Add(transaction);
						}
					}
				}
			}
			return new Tuple<List<FileDetail>, List<FileDetail>>(repeatedTransaction, fozoniTransaction);
		}
	}
}