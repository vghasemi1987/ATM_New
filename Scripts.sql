--username = 15825
--password =7515825
USE ATM_DB;
GO
SELECT TOP 10 * FROM dbo.ApplicationUserItems
UPDATE dbo.ApplicationUserItems
SET PasswordHash ='AQAAAAEAACcQAAAAEKAYdENoiu4ygK4V7xueW262vL5ta6fciSNfof79fxkil/A6+11ZVDe3yH0jdn1fWg=='
WHERE UserName = '15825';
--AQAAAAEAACcQAAAAEKAYdENoiu4ygK4V7xueW262vL5ta6fciSNfof79fxkil/A6+11ZVDe3yH0jdn1fWg==
GO