USE [CodePizza]
GO

DELETE FROM [dbo].[Users];
GO

SET IDENTITY_INSERT [dbo].[Users] ON
GO

INSERT INTO [dbo].[Users]
           ([ID]
		   ,[Version]
           ,[PasswordSalt]
           ,[PasswordHash]
           ,[Email]
           ,[Name]
           ,[Surname]
		   )
     VALUES
           (1
		   ,0
		   ,'D3EW4ZGMXJoMv2HZXbKj0efFxmojSchSY4a3m20r5ik='
		   ,'JAgAG/Atnu/sS6PCG3vhH6XQ5AuHwwkmuoFt2EtnWHQ='
           ,'code@pizza.com'
           ,'Test'
           ,'User'
           )
GO

SET IDENTITY_INSERT [dbo].[Users] OFF
GO

/*
UNDO:

USE [CodePizza]
GO

DELETE FROM [dbo].[Users];
GO

*/