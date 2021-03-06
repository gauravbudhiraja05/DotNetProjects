USE [Pickford]
GO
/****** Object:  UserDefinedFunction [dbo].[SplitString]    Script Date: 13-05-2020 14:35:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[SplitString]
(    
      @Input NVARCHAR(MAX),
      @Character CHAR(1)
)
RETURNS @Output TABLE (
      Item NVARCHAR(1000)
)
AS
BEGIN
      DECLARE @StartIndex INT, @EndIndex INT
 
      SET @StartIndex = 1
      IF SUBSTRING(@Input, LEN(@Input) - 1, LEN(@Input)) <> @Character
      BEGIN
            SET @Input = @Input + @Character
      END
 
      WHILE CHARINDEX(@Character, @Input) > 0
      BEGIN
            SET @EndIndex = CHARINDEX(@Character, @Input)
           
            INSERT INTO @Output(Item)
            SELECT SUBSTRING(@Input, @StartIndex, @EndIndex - 1)
           
            SET @Input = SUBSTRING(@Input, @EndIndex + 1, LEN(@Input))
      END
 
      RETURN
END
GO
/****** Object:  Table [dbo].[Department]    Script Date: 13-05-2020 14:35:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [varchar](100) NULL,
	[IsActive] [bit] NULL,
	[TelephoneNumber] [varchar](100) NULL,
	[ImageName] [varchar](100) NULL,
	[DepartmentHead] [varchar](500) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[DeletedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FolderDetails]    Script Date: 13-05-2020 14:35:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FolderDetails](
	[FolderId] [int] IDENTITY(1,1) NOT NULL,
	[FolderParentId] [int] NULL,
	[FolderName] [varchar](100) NULL,
	[CreationDate] [datetime] NULL,
	[FolderFirstName] [varchar](100) NULL,
	[FolderSurName] [varchar](100) NULL,
	[DepartMentId] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[createdBy] [int] NULL,
	[modifiedBy] [int] NULL,
	[deletedBy] [int] NULL,
	[IsActive] [int] NULL,
 CONSTRAINT [PK_FolderDetails] PRIMARY KEY CLUSTERED 
(
	[FolderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoggedInUser]    Script Date: 13-05-2020 14:35:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoggedInUser](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[FullName] [varchar](100) NOT NULL,
	[RoleName] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_LoggedInUser] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[News]    Script Date: 13-05-2020 14:35:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NewsCode] [int] NULL,
	[Title] [varchar](100) NULL,
	[TeaserText] [varchar](100) NULL,
	[Content1] [varchar](1000) NULL,
	[Content2] [varchar](1000) NULL,
	[DepartmentId] [int] NULL,
	[IsFeatureOnHomePage] [bit] NULL,
	[ThumbnailImage] [varchar](100) NULL,
	[MainImage] [varchar](100) NULL,
	[AdditionalImage1] [varchar](100) NULL,
	[AdditionalImage2] [varchar](100) NULL,
	[PublishDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[AuthorName] [varchar](100) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[DeletedBy] [int] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OurValues]    Script Date: 13-05-2020 14:35:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OurValues](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ValueTitle] [varchar](100) NULL,
	[ValueBackgroundImage] [varchar](100) NULL,
	[ValueTopLeftText] [varchar](1000) NULL,
	[ValueTopRightText] [varchar](1000) NULL,
	[CommunicationTitle] [varchar](100) NULL,
	[CommunicationIcon] [varchar](100) NULL,
	[CommunicationImage] [varchar](100) NULL,
	[CommunicationContent] [varchar](1000) NULL,
	[DedicationTitle] [varchar](100) NULL,
	[DedicationIcon] [varchar](100) NULL,
	[DedicationImage] [varchar](100) NULL,
	[DedicationContent] [varchar](1000) NULL,
	[CareTitle] [varchar](100) NULL,
	[CareIcon] [varchar](100) NULL,
	[CareImage] [varchar](100) NULL,
	[CareContent] [varchar](1000) NULL,
	[ExcellentTitle] [varchar](100) NULL,
	[ExcellentIcon] [varchar](100) NULL,
	[ExcellentImage] [varchar](100) NULL,
	[ExcellentContent] [varchar](1000) NULL,
	[CreationDate] [datetime] NULL,
	[ModificationDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_OurValues] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vacancy]    Script Date: 13-05-2020 14:35:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vacancy](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VacancyCode] [int] NULL,
	[Title] [varchar](100) NULL,
	[TeaserText] [varchar](100) NULL,
	[Content1] [varchar](1000) NULL,
	[Content2] [varchar](1000) NULL,
	[DepartmentId] [int] NULL,
	[ThumbnailImage] [varchar](100) NULL,
	[MainImage] [varchar](100) NULL,
	[AdditionalImage1] [varchar](100) NULL,
	[AdditionalImage2] [varchar](100) NULL,
	[PublishDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[AuthorName] [varchar](100) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[DeletedBy] [int] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Vacancy] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([DepartmentId], [DepartmentName], [IsActive], [TelephoneNumber], [ImageName], [DepartmentHead], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate]) VALUES (1, N'Department 3', 1, N'9876543210', N'abd84449-258e-4000-a531-1c4c5d1bed9430c7532d20200407070749.png', N'Sample Dummy Text', 1, 1, 1, CAST(N'2020-05-10T15:03:15.253' AS DateTime), CAST(N'2020-05-11T11:14:27.643' AS DateTime))
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName], [IsActive], [TelephoneNumber], [ImageName], [DepartmentHead], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate]) VALUES (2, N'Department 1', 1, N'9876543210', N'8e640629-1cd2-43f3-a9da-71e2955a247fdownload.jpg', N'dfdffdfdfddf', 1, NULL, 1, CAST(N'2020-05-10T16:04:51.103' AS DateTime), NULL)
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName], [IsActive], [TelephoneNumber], [ImageName], [DepartmentHead], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate]) VALUES (3, N'Department 2', 1, N'9876543210', N'f7f79901-8412-4eff-82cf-7436d0762f4edownload.jpg', N'sasassasasasa', 1, NULL, 1, CAST(N'2020-05-10T16:05:14.220' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[LoggedInUser] ON 

INSERT [dbo].[LoggedInUser] ([UserId], [Email], [Password], [FullName], [RoleName], [CreatedDate]) VALUES (1, N'superadmin@gmail.com', N'superadmin', N'Super Admin', N'SA', CAST(N'2020-05-10T11:26:33.553' AS DateTime))
INSERT [dbo].[LoggedInUser] ([UserId], [Email], [Password], [FullName], [RoleName], [CreatedDate]) VALUES (4, N'admin@gmail.com', N'admin', N'Departmental Admin', N'DA', CAST(N'2020-05-12T10:55:01.690' AS DateTime))
SET IDENTITY_INSERT [dbo].[LoggedInUser] OFF
GO
SET IDENTITY_INSERT [dbo].[News] ON 

INSERT [dbo].[News] ([ID], [NewsCode], [Title], [TeaserText], [Content1], [Content2], [DepartmentId], [IsFeatureOnHomePage], [ThumbnailImage], [MainImage], [AdditionalImage1], [AdditionalImage2], [PublishDate], [CreatedDate], [ModifiedDate], [AuthorName], [CreatedBy], [ModifiedBy], [DeletedBy], [IsActive]) VALUES (1, NULL, N'News 1 Updated', N'News Teaser Text', N'<span style="color: rgb(0, 0, 0); font-family: &quot;Open Sans&quot;, Arial, sans-serif; text-align: justify;">t is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using ''Content here, content here'', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for ''lorem ipsum'' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).</span><br>', N'<span style="color: rgb(0, 0, 0); font-family: &quot;Open Sans&quot;, Arial, sans-serif; text-align: justify;">t is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using ''Content here, content here'', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for ''lorem ipsum'' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).</span><br>', 2, 1, N'c151789b-1523-498a-a79a-f5d0b23d672e0ab71bf1-e6be-4c35-afd7-12e890aa348anews1.jpg', N'2509cc51-b485-4973-b0a1-ea447bee3ab70bc41d21-2796-4ffb-be25-5c13e6052f2f25_banner-2018.jpg', N'2c3b1aed-4cb9-442a-b5e7-b918e1ce5d700bc41d21-2796-4ffb-be25-5c13e6052f2f25_banner-2018.jpg', N'a15ed49e-c1bf-4057-8ab7-71dde073567d0bc41d21-2796-4ffb-be25-5c13e6052f2f25_banner-2018.jpg', CAST(N'2020-11-05T00:00:00.000' AS DateTime), CAST(N'2020-05-11T14:07:32.807' AS DateTime), CAST(N'2020-05-11T17:51:58.820' AS DateTime), N'Uma Shankar', 0, 1, 1, 0)
SET IDENTITY_INSERT [dbo].[News] OFF
GO
SET IDENTITY_INSERT [dbo].[OurValues] ON 

INSERT [dbo].[OurValues] ([ID], [ValueTitle], [ValueBackgroundImage], [ValueTopLeftText], [ValueTopRightText], [CommunicationTitle], [CommunicationIcon], [CommunicationImage], [CommunicationContent], [DedicationTitle], [DedicationIcon], [DedicationImage], [DedicationContent], [CareTitle], [CareIcon], [CareImage], [CareContent], [ExcellentTitle], [ExcellentIcon], [ExcellentImage], [ExcellentContent], [CreationDate], [ModificationDate], [CreatedBy], [ModifiedBy]) VALUES (1, N'Value Title', N'03f38bc6-fe15-406d-af18-44854d14cca9pickfords-charity.jpg', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', N'Communication Title', N'05c9e508-ab09-45a0-9bba-dbb3dcec6b51com_icon.png', N'6f5636bd-6602-4865-a789-7a9fb2b6ba2fPenguins.jpg', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', N'Dedication Title', N'33bf4a3c-5ecd-4c8c-9ed0-dca72ad310b6excellence_icon.png', N'43dd79a1-a9dc-4cb0-b8d1-8dc3ae34d267Tulips.jpg', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', N'Care Title', N'49fadafc-900e-48f7-a2b2-102d45c71500care_icon.png', N'54f40a16-5a18-42dd-aa90-75131aef1f40pickfords-charity.jpg', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', N'Excellent Title', N'acb9994e-b57e-4675-a859-931dc8531172excellence_icon.png', N'd403a85b-23bc-48f1-ac69-f5db84c611addownload(1).jpg', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', CAST(N'2020-05-10T11:26:33.553' AS DateTime), CAST(N'2020-05-10T11:26:33.553' AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[OurValues] OFF
GO
SET IDENTITY_INSERT [dbo].[Vacancy] ON 

INSERT [dbo].[Vacancy] ([ID], [VacancyCode], [Title], [TeaserText], [Content1], [Content2], [DepartmentId], [ThumbnailImage], [MainImage], [AdditionalImage1], [AdditionalImage2], [PublishDate], [CreatedDate], [ModifiedDate], [AuthorName], [CreatedBy], [ModifiedBy], [DeletedBy], [IsActive]) VALUES (1, NULL, N'Vacancy 1 ', N'Vacancy 1 Teaser Text 1', N'<strong style="margin: 0px; padding: 0px; color: rgb(0, 0, 0); font-family: &quot;Open Sans&quot;, Arial, sans-serif; text-align: justify;">Lorem Ipsum</strong><span style="color: rgb(0, 0, 0); font-family: &quot;Open Sans&quot;, Arial, sans-serif; text-align: justify;">&nbsp;is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</span><br>', N'<strong style="margin: 0px; padding: 0px; color: rgb(0, 0, 0); font-family: &quot;Open Sans&quot;, Arial, sans-serif; text-align: justify;">Lorem Ipsum</strong><span style="color: rgb(0, 0, 0); font-family: &quot;Open Sans&quot;, Arial, sans-serif; text-align: justify;">&nbsp;is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</span><br>', 2, N'12153293-cdcc-4f46-9108-a3d88b38144a1bc7e913-389a-466a-9d3e-589a9c6b9ba5download(3).jpg', N'9abc1b77-107c-4547-93d5-268c257aea120afc3a20-034f-416f-9342-dcc59a730c1e0bc41d21-2796-4ffb-be25-5c13', N'570184f3-f5f3-4d94-b7e2-ffb2d7eb7abe0afc3a20-034f-416f-9342-dcc59a730c1e0bc41d21-2796-4ffb-be25-5c13', N'4d89b021-d657-4962-bdc2-a67662a326580bc41d21-2796-4ffb-be25-5c13e6052f2f25_banner-2018.jpg', CAST(N'2020-12-05T00:00:00.000' AS DateTime), CAST(N'2020-05-12T10:44:05.523' AS DateTime), CAST(N'2020-05-12T10:44:49.660' AS DateTime), N'Uma Shankar', 1, 1, 1, 0)
SET IDENTITY_INSERT [dbo].[Vacancy] OFF
GO
ALTER TABLE [dbo].[Department] ADD  CONSTRAINT [DF_Department_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[LoggedInUser] ADD  CONSTRAINT [DF_LoggedInUser_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
/****** Object:  StoredProcedure [dbo].[usp_AddNews]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_AddNews]
( 
 @Title               VARCHAR(100), 
 @TeaserText          VARCHAR(100), 
 @Content1            VARCHAR(1000), 
 @Content2            VARCHAR(1000), 
 @DepartmentId        INT, 
 @IsFeatureOnHomePage BIT, 
 @ThumbnailImage      VARCHAR(100), 
 @MainImage           VARCHAR(100), 
 @AdditionalImage1    VARCHAR(100), 
 @AdditionalImage2    VARCHAR(100), 
 @PublishDate         DATETIME, 
 @AuthorName          VARCHAR(100), 
 @CreatedBy           INT, 
 @IsActive            BIT
)
AS
    BEGIN
		Declare @NewsCode INT;
		DECLARE @Code INT;
		DECLARE @IDENTITY INT;
		SELECT @Code= Max(NewsCode) from  News;
		IF(@Code=0 )
		BEGIN
			SET @NewsCode=101;
		END
		ELSE 
		BEGIN
			SET @NewsCode= @Code+1;
		END
        INSERT INTO News
			   (NewsCode,Title,TeaserText,Content1,Content2,DepartmentId,IsFeatureOnHomePage,
				ThumbnailImage,MainImage,AdditionalImage1,AdditionalImage2,PublishDate,CreatedDate,
				AuthorName,IsActive,CreatedBy)
        VALUES (@NewsCode,@Title,@TeaserText,@Content1,@Content2,@DepartmentId,@IsFeatureOnHomePage, 
				@ThumbnailImage,@MainImage,@AdditionalImage1,@AdditionalImage2,@PublishDate,GetDate(), 
				@AuthorName,@IsActive,@CreatedBy)

		SELECT @IDENTITY= @@IDENTITY;

		DECLARE @IsSuccess  BIT;
		DECLARE @Message VARCHAR(100);
		IF(@IDENTITY >0)
			BEGIN
				SET @IsSuccess=1;
				SET @Message='Record Saved Successfully';
			END
		ELSE
			BEGIN
				SET @IsSuccess=0;
				SET @Message='Record Not Saved Successfully';
			END
		SELECT @IsSuccess as IsSuccess, @Message as Message
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_AddVacancy]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[usp_AddVacancy]
( 
 @Title               VARCHAR(100), 
 @TeaserText          VARCHAR(100), 
 @Content1            VARCHAR(1000), 
 @Content2            VARCHAR(1000), 
 @DepartmentId        INT, 
 @ThumbnailImage      VARCHAR(100), 
 @MainImage           VARCHAR(100), 
 @AdditionalImage1    VARCHAR(100), 
 @AdditionalImage2    VARCHAR(100), 
 @PublishDate         DATETIME, 
 @AuthorName          VARCHAR(100), 
 @CreatedBy           INT, 
 @IsActive            BIT
)
AS
    BEGIN
		Declare @VacancyCode INT;
		DECLARE @Code INT;
		DECLARE @IDENTITY INT;
		SELECT @Code= Max(VacancyCode) from  Vacancy;
		IF(@Code=0 )
		BEGIN
			SET @VacancyCode=101;
		END
		ELSE 
		BEGIN
			SET @VacancyCode= @Code+1;
		END
        INSERT INTO Vacancy
			   (VacancyCode,Title,TeaserText,Content1,Content2,DepartmentId,ThumbnailImage,
			    MainImage,AdditionalImage1,AdditionalImage2,PublishDate,CreatedDate,
				AuthorName,IsActive,CreatedBy)
        VALUES (@VacancyCode,@Title,@TeaserText,@Content1,@Content2,@DepartmentId,@ThumbnailImage, 
				@MainImage,@AdditionalImage1,@AdditionalImage2,@PublishDate,GetDate(), 
				@AuthorName,@IsActive,@CreatedBy)

		SELECT @IDENTITY= @@IDENTITY;

		DECLARE @IsSuccess  BIT;
		DECLARE @Message VARCHAR(100);
		IF(@IDENTITY >0)
			BEGIN
				SET @IsSuccess=1;
				SET @Message='Record Saved Successfully';
			END
		ELSE
			BEGIN
				SET @IsSuccess=0;
				SET @Message='Record Not Saved Successfully';
			END
		SELECT @IsSuccess as IsSuccess, @Message as Message
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_AuthenticateAdminUser]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_AuthenticateAdminUser] --'admin@gmail.com','admin'

(@Email    VARCHAR(100), 
 @Password VARCHAR(100)
)
AS
     DECLARE @UserId INT;
     DECLARE @EmailId VARCHAR(100);
     DECLARE @FullName VARCHAR(100);
     DECLARE @RoleName VARCHAR(100);
     DECLARE @IsSuccess BIT;
     DECLARE @Message VARCHAR(100);
    BEGIN
        IF EXISTS
        (
            SELECT COUNT(*)
            FROM LoggedInUser
            WHERE Email = @Email
                  AND Password = @Password
        )
            BEGIN
                SELECT @UserId=UserId, 
                       @EmailId= Email, 
                       @FullName=FullName, 
                       @RoleName=RoleName,
					   @IsSuccess=1,
					   @Message='Record Found'
                FROM LoggedInUser
                WHERE Email = @Email
                      AND Password = @Password;
        END;
		ELSE
		BEGIN
			SET @IsSuccess=0;
			SET @Message='No Record Found'
		END

		SELECT @UserId as UserId,@EmailId as EmailId,@FullName as FullName,@RoleName as RoleName,@IsSuccess as IsSuccess,@Message as Message
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteDepartmentsByIds]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_DeleteDepartmentsByIds] --1
(@DepartmentIds VARCHAR(500), 
 @DeletedBy     INT
)
AS
    BEGIN
        DECLARE @IsSuccess BIT;
        DECLARE @Message VARCHAR(100);
        UPDATE Department
          SET 
              DeletedBy = @DeletedBy, 
              IsActive = 0
        WHERE DepartmentId IN
        (
            SELECT Item
            FROM dbo.SplitString(@DepartmentIds, '|')
        );
        SET @IsSuccess = 1;
        SET @Message = 'Records Deleted Successfully';

		SELECT @IsSuccess as IsSuccess, @Message as Message

    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteNewsByIds]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_DeleteNewsByIds] --1
(@NewsIds VARCHAR(500), 
 @DeletedBy     INT
)
AS
    BEGIN
        DECLARE @IsSuccess BIT;
        DECLARE @Message VARCHAR(100);
        UPDATE News
          SET 
              DeletedBy = @DeletedBy, 
              IsActive = 0
        WHERE ID IN
        (
            SELECT Item
            FROM dbo.SplitString(@NewsIds, '|')
        );
        SET @IsSuccess = 1;
        SET @Message = 'Records Deleted Successfully';

		SELECT @IsSuccess as IsSuccess, @Message as Message

    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteVacancyByIds]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_DeleteVacancyByIds] --1
(@VacancyIds VARCHAR(500), 
 @DeletedBy     INT
)
AS
    BEGIN
        DECLARE @IsSuccess BIT;
        DECLARE @Message VARCHAR(100);
        UPDATE Vacancy
          SET 
              DeletedBy = @DeletedBy, 
              IsActive = 0
        WHERE ID IN
        (
            SELECT Item
            FROM dbo.SplitString(@VacancyIds, '|')
        );
        SET @IsSuccess = 1;
        SET @Message = 'Records Deleted Successfully';

		SELECT @IsSuccess as IsSuccess, @Message as Message

    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllDepartments]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_GetAllDepartments] --1
AS
    BEGIN
           SELECT DepartmentId,DepartmentName,TelephoneNumber,ImageName,DepartmentHead
           FROM Department WHERE IsActive = 1;
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllDepartments_New]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_GetAllDepartments_New] 
AS
    BEGIN
           SELECT DepartmentId,DepartmentName,IsActive,TelephoneNumber,ImageName,
				  DepartmentHead,CreatedBy,ModifiedBy,CreatedDate,
				  CONVERT(varchar, CreatedDate, 1) as CreationDateDisplay
           FROM Department WHERE IsActive = 1;
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllFolderListByDeptId]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_GetAllFolderListByDeptId] 
(@DeptId int)
AS
    BEGIN
           SELECT FolderId,FolderParentId,FolderName,CreationDate,
				  FolderFirstName,FolderSurName,DepartMentId
           FROM FolderDetails WHERE DepartMentId = @DeptId;
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_GetDepartmentById]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_GetDepartmentById] --1
(@deptId int)
AS
    BEGIN
           SELECT DepartmentId,DepartmentName,IsActive,TelephoneNumber,ImageName,
				  DepartmentHead,CreatedBy,ModifiedBy,CreatedDate,
				  CONVERT(varchar, CreatedDate, 1) as CreationDateDisplay,
				  CONVERT(varchar, CreatedDate, 1) as CreationDate
           FROM Department WHERE DepartmentId=@deptId AND IsActive = 1;
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_GetImageNames]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_GetImageNames](@NewsIds VARCHAR(500))
AS
    BEGIN
		SELECT ThumbnailImage,MainImage,AdditionalImage1,AdditionalImage2
		  FROM News where ID  IN
        (
            SELECT Item
            FROM dbo.SplitString(@NewsIds, '|')
        );
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_GetImageNamesForVacancy]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_GetImageNamesForVacancy](@VacancyIds VARCHAR(500))
AS
    BEGIN
		SELECT ThumbnailImage,MainImage,AdditionalImage1,AdditionalImage2
		  FROM Vacancy where ID  IN
        (
            SELECT Item
            FROM dbo.SplitString(@VacancyIds, '|')
        );
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_GetNewsByDepartmentId]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_GetNewsByDepartmentId](@deptid INT)
AS
    BEGIN
        IF(@deptid = 0)
            BEGIN
                SELECT n.ID, 
                       n.NewsCode, 
                       n.Title, 
                       d.DepartmentName,
					   n.AuthorName,
					   CONVERT(varchar, n.PublishDate, 1) PublishDate,
					   CONVERT(varchar, n.CreatedDate, 1) CreationDate,
					   CONVERT(varchar, n.PublishDate, 1) PublishDateDisplay,
					   CONVERT(varchar, n.CreatedDate, 1) CreationDateDisplay
                FROM News n
                     LEFT JOIN Department d ON n.DepartmentId = d.DepartmentId
                WHERE n.IsActive = 1;
        END;
            ELSE
            BEGIN
                SELECT n.ID, 
                       n.NewsCode, 
                       n.Title, 
                       d.DepartmentName,
					   n.AuthorName,
					   CONVERT(varchar, n.PublishDate, 1) PublishDate,
					   CONVERT(varchar, n.CreatedDate, 1) CreationDate,
					   CONVERT(varchar, n.PublishDate, 1) PublishDateDisplay,
					   CONVERT(varchar, n.CreatedDate, 1) CreationDateDisplay
                FROM News n
                     LEFT JOIN Department d ON n.DepartmentId = d.DepartmentId
                WHERE n.IsActive = 1
                      AND n.DepartmentId = @deptid;
        END;
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_GetNewsById]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_GetNewsById](@Id INT)
AS
    BEGIN
		SELECT ID as Id,NewsCode,Title,TeaserText,Content1,Content2,DepartmentId,IsFeatureOnHomePage
		      ,ThumbnailImage,MainImage,AdditionalImage1,AdditionalImage2,PublishDate,
			  CONVERT(varchar, PublishDate, 1) as PublishDateDisplay,AuthorName,
			  CONVERT(varchar, CreatedDate, 1) as CreationDate,CreatedBy,ModifiedBy,IsActive
		  FROM News where ID=@Id
		SELECT DepartmentId,DepartmentName,TelephoneNumber,ImageName,DepartmentHead
		  FROM Department WHERE IsActive = 1;
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_GetOurValues]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_GetOurValues] 
AS
    BEGIN
          SELECT ID,ValueTitle,ValueBackgroundImage,ValueTopLeftText,ValueTopRightText,
		         CommunicationTitle,CommunicationIcon,CommunicationImage,CommunicationContent,
		         DedicationTitle,DedicationIcon,DedicationImage,DedicationContent,
		         CareTitle,CareIcon,CareImage,CareContent,
		         ExcellentTitle,ExcellentIcon,ExcellentImage,ExcellentContent,
		         CreationDate,ModificationDate,CreatedBy,ModifiedBy
	      FROM OurValues
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_GetPreRequisitesDataToCreateNews]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_GetPreRequisitesDataToCreateNews](@UserId INT)
AS
    BEGIN
        SELECT DepartmentId,DepartmentName,TelephoneNumber,ImageName,DepartmentHead
        FROM Department WHERE IsActive = 1;
		SELECT FullName as AuthorName FROM LoggedInUser where UserId=@UserId
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_GetPreRequisitesDataToCreateVacancy]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_GetPreRequisitesDataToCreateVacancy](@UserId INT)
AS
    BEGIN
        SELECT DepartmentId,DepartmentName,TelephoneNumber,ImageName,DepartmentHead
        FROM Department WHERE IsActive = 1;
		SELECT FullName as AuthorName FROM LoggedInUser where UserId=@UserId
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_GetVacancyByDepartmentId]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_GetVacancyByDepartmentId](@deptid INT)
AS
    BEGIN
        IF(@deptid = 0)
            BEGIN
                SELECT v.Id, 
                       v.VacancyCode, 
                       v.Title, 
                       d.DepartmentName,
					   v.AuthorName,
					   CONVERT(varchar, v.PublishDate, 1) PublishDate,
					   CONVERT(varchar, v.CreatedDate, 1) CreationDate,
					   CONVERT(varchar, v.PublishDate, 1) PublishDateDisplay,
					   CONVERT(varchar, v.CreatedDate, 1) CreationDateDisplay
                FROM Vacancy v
                     LEFT JOIN Department d ON v.DepartmentId = d.DepartmentId
                WHERE v.IsActive = 1;
        END;
            ELSE
            BEGIN
                SELECT v.ID, 
                       v.VacancyCode, 
                       v.Title, 
                       d.DepartmentName,
					   v.AuthorName,
					   CONVERT(varchar, v.PublishDate, 1) PublishDate,
					   CONVERT(varchar, v.CreatedDate, 1) CreationDate,
					   CONVERT(varchar, v.PublishDate, 1) PublishDateDisplay,
					   CONVERT(varchar, v.CreatedDate, 1) CreationDateDisplay
                FROM Vacancy v
                     LEFT JOIN Department d ON v.DepartmentId = d.DepartmentId
                WHERE v.IsActive = 1
                      AND v.DepartmentId = @deptid;
        END;
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_GetVacancyById]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_GetVacancyById](@Id INT)
AS
    BEGIN
		SELECT Id,VacancyCode,Title,TeaserText,Content1,Content2,DepartmentId,ThumbnailImage,
			   MainImage,AdditionalImage1,AdditionalImage2,PublishDate,
			   CONVERT(varchar, PublishDate, 1) as PublishDateDisplay,AuthorName,
			   CONVERT(varchar, CreatedDate, 1) as CreationDate,CreatedBy,ModifiedBy,IsActive
		  FROM Vacancy where ID=@Id
		SELECT DepartmentId,DepartmentName,TelephoneNumber,ImageName,DepartmentHead
		  FROM Department WHERE IsActive = 1;
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_IsDepartmentExist]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_IsDepartmentExist] --'admin@gmail.com','admin'

(@departmentId    INT, 
 @departmentName VARCHAR(100)
)
AS
    BEGIN
	Declare @Exists bit;
        IF EXISTS
        (
            SELECT DepartmentName
            FROM Department
            WHERE DepartmentName = @departmentName
                  AND DepartmentId=@departmentId
        )
            BEGIN
                SET @Exists= 1;
        END;
		ELSE
		BEGIN
			SET @Exists= 0;
		END

		SELECT @Exists  as isExist
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_SaveDepartment]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_SaveDepartment] --'admin@gmail.com','admin'

(@departmentName  VARCHAR(100), 
 @isActive        BIT, 
 @image           VARCHAR(100), 
 @createdBy       INT, 
 @telephoneNumber VARCHAR(100), 
 @departmentHead  VARCHAR(500)
)
AS
    BEGIN
        DECLARE @Identity INT;
        DECLARE @IsSuccess BIT;
        DECLARE @Message VARCHAR(100);
        INSERT INTO Department
        (DepartmentName, 
         IsActive, 
         ImageName, 
         CreatedBy, 
         TelephoneNumber, 
         DepartmentHead
        )
        VALUES
        (@departmentName, 
         @isActive, 
         @image, 
         @createdBy, 
         @telephoneNumber, 
         @departmentHead
        );
        SELECT @Identity = @@IDENTITY;
        IF(@Identity > 0)
            BEGIN
                SET @IsSuccess = 1;
                SET @Message = 'Record Saved Successfully';
        END;
            ELSE
            BEGIN
                SET @IsSuccess = 0;
                SET @Message = 'Record Not Saved Successfully';
        END;
        SELECT @IsSuccess AS IsSuccess, 
               @Message AS Message;
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateDepartment]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_UpdateDepartment] --'admin@gmail.com','admin'

(@departmenId     INT, 
 @departmentName  VARCHAR(100), 
 @isActive        BIT, 
 @image           VARCHAR(100), 
 @modifiedBy      INT, 
 @telephoneNumber VARCHAR(100), 
 @departmentHead  VARCHAR(500)
)
AS
    BEGIN
        DECLARE @Identity INT;
        DECLARE @IsSuccess BIT;
        DECLARE @Message VARCHAR(100);
        UPDATE Department
          SET 
              DepartmentName = @departmentName, 
              IsActive = @isActive, 
              ImageName = @image, 
              ModifiedBy = @modifiedBy, 
              TelephoneNumber = @telephoneNumber, 
              DepartmentHead = @departmentHead, 
              ModifiedDate = GETDATE()
        WHERE DepartmentId = @departmenId;
        SET @IsSuccess = 1;
        SET @Message = 'Record Updated Successfully';
        SELECT @IsSuccess AS IsSuccess, 
               @Message AS Message;
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateNews]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_UpdateNews]
(@Id                  INT, 
 @Title               VARCHAR(100), 
 @TeaserText          VARCHAR(100), 
 @Content1            VARCHAR(1000), 
 @Content2            VARCHAR(1000), 
 @DepartmentId        INT, 
 @IsFeatureOnHomePage BIT, 
 @ThumbnailImage      VARCHAR(100), 
 @MainImage           VARCHAR(100), 
 @AdditionalImage1    VARCHAR(100), 
 @AdditionalImage2    VARCHAR(100), 
 @PublishDate         DATETIME, 
 @AuthorName          VARCHAR(100), 
 @CreatedBy           INT, 
 @ModifiedBy          INT, 
 @IsActive            BIT
)
AS
    BEGIN
        UPDATE News
          SET 
              Title = @Title, 
              TeaserText = @TeaserText, 
              Content1 = @Content1, 
              Content2 = @Content2, 
              DepartmentId = @DepartmentId, 
              IsFeatureOnHomePage = @IsFeatureOnHomePage, 
              ThumbnailImage = @ThumbnailImage, 
              MainImage = @MainImage, 
              AdditionalImage1 = @AdditionalImage1, 
              AdditionalImage2 = @AdditionalImage2, 
              PublishDate = @PublishDate, 
              ModifiedDate = GETDATE(), 
              AuthorName = @AuthorName, 
              IsActive = @IsActive, 
              ModifiedBy = @ModifiedBy
        WHERE ID = @Id;
        DECLARE @IsSuccess BIT;
        DECLARE @Message VARCHAR(100);
        SET @IsSuccess = 1;
        SET @Message = 'Record Updated Successfully';
        SELECT @IsSuccess AS IsSuccess, 
               @Message AS Message;
    END;
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateVacancy]    Script Date: 13-05-2020 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_UpdateVacancy]
(@Id                  INT, 
 @Title               VARCHAR(100), 
 @TeaserText          VARCHAR(100), 
 @Content1            VARCHAR(1000), 
 @Content2            VARCHAR(1000), 
 @DepartmentId        INT, 
 @ThumbnailImage      VARCHAR(100), 
 @MainImage           VARCHAR(100), 
 @AdditionalImage1    VARCHAR(100), 
 @AdditionalImage2    VARCHAR(100), 
 @PublishDate         DATETIME, 
 @AuthorName          VARCHAR(100), 
 @CreatedBy           INT, 
 @ModifiedBy          INT, 
 @IsActive            BIT
)
AS
    BEGIN
        UPDATE Vacancy
          SET 
              Title = @Title, 
              TeaserText = @TeaserText, 
              Content1 = @Content1, 
              Content2 = @Content2, 
              DepartmentId = @DepartmentId, 
              ThumbnailImage = @ThumbnailImage, 
              MainImage = @MainImage, 
              AdditionalImage1 = @AdditionalImage1, 
              AdditionalImage2 = @AdditionalImage2, 
              PublishDate = @PublishDate, 
              ModifiedDate = GETDATE(), 
              AuthorName = @AuthorName, 
              IsActive = @IsActive, 
              ModifiedBy = @ModifiedBy
        WHERE ID = @Id;
        DECLARE @IsSuccess BIT;
        DECLARE @Message VARCHAR(100);
        SET @IsSuccess = 1;
        SET @Message = 'Record Updated Successfully';
        SELECT @IsSuccess AS IsSuccess, 
               @Message AS Message;
    END;
GO
