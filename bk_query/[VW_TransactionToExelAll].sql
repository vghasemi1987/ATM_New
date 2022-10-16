USE [ATM_DB]
GO

/****** Object:  View [dbo].[VW_TransactionToExelAll]    Script Date: 4/10/2022 10:12:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER VIEW [dbo].[VW_TransactionToExelAll]
AS
select * from (
SELECT 
ROW_NUMBER() OVER (ORDER BY Trd.id) as Id,
Trd.TransactionNumber 'TransactionNumber',
Trd.Date 'Date',
Trd.Time 'Time',
Trd.AtmCode 'AtmCode',
Trd.CardNumber 'CardNumber',
Trd.Amount 'Amount',
(select CONCAT(tt.Title,' - ',tt.Content) from Transaction_Types tt where tt.Id = trd.TypeId) as CardType,
Trd.UserDescription 'UserDescription',
Trd.DocumentCode 'DocumentCode',
trf.Name 'FileName',
--atm.TransactionAmount 'مبلغ',
--atm.CardType 'نوع کارت',
--atm.Status 'وضعیت',
(select Title from  dbo.Transaction_Status where id = trf.statusId)as 'Status',
(select Title from  dbo.Common_Branches where id = trf.BranchId)as 'TitleBranch',
(select code from  dbo.Common_Branches where id = trf.BranchId)as 'MainCodeBranch', --bra.BranchHeadId ,
(select Title from  dbo.Common_Branches where id = trf.BranchId)as 'MainTitleBranch', --bra.BranchHeadId ,
uit.UserName 'UserName',
uit.FirstName +' '+ uit.LastName 'Nikname',
(select MAX(UpdateDateTime)  from dbo.Transaction_Workfollow where FileDetailId = trd.Id AND StatusId=5) as 'DateRefEmp',
(select MAX(UpdateDateTime)  from dbo.Transaction_Workfollow where FileDetailId = trd.Id AND StatusId=6) as 'DateRefBranch',
trf.RegDateTime as 'RegDateTime'
FROM  dbo.Transaction_FileDetails trd
left join  dbo.Transaction_Files trf ON Trd.FileId = trf.Id
left join  dbo.ApplicationUserItems uit ON uit.Id = trf.UserId
)t
GO


