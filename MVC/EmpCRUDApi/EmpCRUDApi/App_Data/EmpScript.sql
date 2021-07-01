USE [EmpCrud]
GO
/****** Object:  Table [dbo].[EmpDetails]    Script Date: 16-Nov-20 4:49:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmpDetails](
	[EmpId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Salary] [numeric](18, 0) NOT NULL,
	[DOB] [varchar](100) NULL,
	[HighestQualification] [varchar](50) NULL,
 CONSTRAINT [PK_EmpDetails] PRIMARY KEY CLUSTERED 
(
	[EmpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[EmpDetails] ON 
GO
INSERT [dbo].[EmpDetails] ([EmpId], [FirstName], [LastName], [Salary], [DOB], [HighestQualification]) VALUES (1, N'Employee 1 FName', N'Employee 1 LName', CAST(20000 AS Numeric(18, 0)), N'1990-01-05', N'MCA')
GO
INSERT [dbo].[EmpDetails] ([EmpId], [FirstName], [LastName], [Salary], [DOB], [HighestQualification]) VALUES (2, N'Employee 2 FName', N'Employee 2 FName', CAST(35000 AS Numeric(18, 0)), N'1996-04-30', N'M Pharma')
GO
SET IDENTITY_INSERT [dbo].[EmpDetails] OFF
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteEmployeebyId]    Script Date: 16-Nov-20 4:49:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_DeleteEmployeebyId] @EmpId int

AS
BEGIN
	Delete from EmpDetails where EmpId=@EmpId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllEmployees]    Script Date: 16-Nov-20 4:49:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetAllEmployees]
AS
BEGIN
	SELECT EmpId, FirstName, LastName, Salary, DOB, HighestQualification FROM EmpDetails
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetEmployeebyId]    Script Date: 16-Nov-20 4:49:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetEmployeebyId] @EmpId int
AS
BEGIN
	SELECT EmpId, FirstName, LastName, Salary, DOB, HighestQualification FROM EmpDetails where EmpId=@EmpId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SaveEmployee]    Script Date: 16-Nov-20 4:49:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[usp_SaveEmployee]
(
 @FirstName varchar(100),
 @LastName  varchar(100),
 @Salary numeric(18,0),
 @DOB date,
 @HighestQualification varchar(50)
)
AS
BEGIN
	INSERT INTO EmpDetails
           (FirstName, LastName, Salary, DOB, HighestQualification)
     VALUES
           (@FirstName, @LastName, @Salary, @DOB , @HighestQualification)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateEmployee]    Script Date: 16-Nov-20 4:49:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_UpdateEmployee]
(
 @EmpId int,
 @FirstName varchar(100),
 @LastName  varchar(100),
 @Salary numeric(18,0),
 @DOB date,
 @HighestQualification varchar(50)
)
AS
BEGIN
	Update EmpDetails set FirstName= @FirstName,
						  LastName=@LastName,
						  Salary=@Salary,
						  DOB=@DOB,
						  HighestQualification=@HighestQualification
						  where EmpId=@EmpId
END
GO
