USE [ProjectMeeting]
GO
/****** Object:  Table [dbo].[M_Customer]    Script Date: 08/08/2021 22:13:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[M_Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[Mobile] [varchar](50) NULL,
	[ImageUpload] [varchar](50) NULL,
	[Designation] [varchar](100) NULL,
	[Country] [varchar](100) NULL,
	[Gender] [varchar](50) NULL,
	[UserName] [varchar](100) NULL,
	[Password] [varchar](50) NULL,
	[bit_deletedFlag] [bit] NULL,
 CONSTRAINT [PK_M_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[M_Customer] ON
INSERT [dbo].[M_Customer] ([Id], [Name], [Mobile], [ImageUpload], [Designation], [Country], [Gender], [UserName], [Password], [bit_deletedFlag]) VALUES (1, N'SIBA MOHAN', N'9853333397', N'siba.jpg', N'SR, PROGRAMMER ANALYST', N'India', N'Male', N'siba', N'siba', 0)
INSERT [dbo].[M_Customer] ([Id], [Name], [Mobile], [ImageUpload], [Designation], [Country], [Gender], [UserName], [Password], [bit_deletedFlag]) VALUES (4, N'SATYA NIRAKAR', N'9856421542', N'satyaNirakar.jpg', N'SR SOFTWARE ENGINEER', N'India', N'Male', N'satya', N'satya', 0)
INSERT [dbo].[M_Customer] ([Id], [Name], [Mobile], [ImageUpload], [Designation], [Country], [Gender], [UserName], [Password], [bit_deletedFlag]) VALUES (5, N'Santosh Behera', N'9745125485', N'santosh.jpg', N'Sr.Tech Led', N'India', N'Male', N'santosh', N'santosh', 0)
SET IDENTITY_INSERT [dbo].[M_Customer] OFF
/****** Object:  StoredProcedure [dbo].[usp_Customer_aed]    Script Date: 08/08/2021 22:13:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_Customer_aed]
@P_Action varchar(2)=null,
@P_UserName varchar(50)=null,
@P_Password varchar(50)=null,
@P_Country varchar(50)=null,
@P_Name varchar(100)=null,
@P_Address varchar(100)=null,
@P_Mobile varchar(50)=null,
@P_Email varchar(50)=null,
@P_Gender varchar(50)=null,
@P_Image varchar(500)=null,
@P_Id int=0,

@P_msg varchar(50)=null output
as
begin

	if(@P_Action='A')
	begin
		insert into M_Customer( Name,[password]) values(@P_UserName,@P_Password)
		set @P_msg ='1'
	end

	if(@P_Action='I')
	begin
		insert into M_Customer(Name, [Designation], Mobile, username, Gender, [Password],Country,ImageUpload) 
			   values    (@P_Name,@P_Address,@P_Mobile,@P_Email, @P_Gender,@P_Password,@P_Country,@P_Image)
		set @P_msg ='1'
	end

	if(@P_Action='L')--Login
	begin
		declare @count int=0
		select @count=(select count(*) from M_Customer where username=@P_Email and [Password]=@P_Password and bit_deletedFlag=0)
		begin
		if  (@count>0)
		begin
		set @P_msg ='1'
		end
		else
		begin
		set @P_msg ='2'
		end
		End
	end

	IF(@P_Action='S')
		BEGIN			
			SELECT Id,Name as Name , [Designation], Mobile, username, Gender, [Password],Country,ImageUpload FROM M_Customer
		END
IF(@P_Action='D')
		BEGIN	
            DELETE FROM M_Customer WHERE id=@P_Id  
        end
end
GO
/****** Object:  Default [DF_M_Customer_bit_deletedFlag]    Script Date: 08/08/2021 22:13:48 ******/
ALTER TABLE [dbo].[M_Customer] ADD  CONSTRAINT [DF_M_Customer_bit_deletedFlag]  DEFAULT ((0)) FOR [bit_deletedFlag]
GO
