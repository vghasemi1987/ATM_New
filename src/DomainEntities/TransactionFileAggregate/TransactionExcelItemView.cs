using DomainEntities.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DomainEntities.TransactionFileAggregate
{
	public class TransactionToExelAll : Entity<long>
	{
		//->Transaction_FileDetails
		/// <summary>
		/// شماره تراکنش
		/// </summary>
		public string TransactionNumber { get; set; }
		/// <summary>
		/// تاریخ تراکنش
		/// </summary>
		public string Date { get; set; }
		/// <summary>
		/// ساعت تراکنش
		/// </summary>
		public string Time { get; set; }
		/// <summary>
		/// شماره کارت
		/// </summary>
		public string CardNumber { get; set; }

		/// <summary>
		/// وضعیت
		/// </summary>
		public string Status { get; set; }
		/// <summary>
		/// مبلغ
		/// </summary>
		public decimal? Amount { get; set; }
		//->ATMUnknownTransactions
		/// <summary>
		/// نوع کارت
		/// </summary>
		//public string ATM { get; set; }
		/// <summary>
		/// کد دستگاه
		/// </summary>
		public string CardType { get; set; }
		/// <summary>
		/// عنوان امور شعب
		/// </summary>
		public string MainTitleBranch { get; set; }
		/// <summary>
		/// کد امور شعب
		/// </summary>
		public int? MainCodeBranch { get; set; }
		/// <summary>
		/// نام شعبه
		/// </summary>
		public string TitleBranch { get; set; }
		public string AtmCode { get; set; }
		/// <summary>
		/// کد شعبه
		/// </summary>
		// public int? CodeBranch { get; set; }
		/// <summary>
		/// کد کاربر
		/// </summary>
		public string UserName { get; set; }
		/// <summary>
		/// نام کاربر
		/// </summary>
		public string Nikname { get; set; }
		/// <summary>
		/// تاریخ ارجاع متصدی
		/// </summary>
		public DateTime? DateRefEmp { get; set; }
		/// <summary>
		/// تاریخ ارجاع رئیس شعبه
		/// </summary>
		public DateTime? DateRefBranch { get; set; }
		/// <summary>
		/// شماره سند
		/// </summary>
		public int? DocumentCode { get; set; }
		/// <summary>
		/// نام فایل
		/// </summary>
		//public string Name { get; set; }

		/// <summary>
		/// نام فایل
		/// </summary>
		public string FileName { get; set; }
	

		public string UserDescription { get; set; }
		/// <summary>
		/// تاریخ برداشت فایل
		/// </summary>
		//public DateTime? RegDateTime { get; set; }
		public DateTime? RegDateTime { get; set; }
		/// <summary>
		/// وضیعت
		/// </summary>
		//public string Title { get; set; }
		public int BranchCode { get; set; }
	}
}