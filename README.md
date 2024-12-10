USE [Practice]
GO
/****** Object:  Table [dbo].[tbl_Department]    Script Date: 09-12-2024 23:53:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Department](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Department_Name] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Employee]    Script Date: 09-12-2024 23:53:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Employee](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Employee_Name] [nvarchar](max) NULL,
	[Employee_Salary] [nvarchar](max) NULL,
	[Department_Id] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[tbl_Employee]  WITH CHECK ADD  CONSTRAINT [fk_tbl_Employee_foreign] FOREIGN KEY([Department_Id])
REFERENCES [dbo].[tbl_Department] ([Id])
GO
ALTER TABLE [dbo].[tbl_Employee] CHECK CONSTRAINT [fk_tbl_Employee_foreign]
GO
/****** Object:  StoredProcedure [dbo].[sp_Department_Employee_Details]    Script Date: 09-12-2024 23:53:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[sp_Department_Employee_Details]
@Emp_Id bigint = null,
@Dept_Id bigint = null,
@JSONOUT NVARCHAR(MAX) OUTPUT   
as 
begin
begin try
set @JSONOUT = (
select e.Id [Employee Id], e.Employee_Name, e.Employee_Salary, d.Department_Name From tbl_Employee e 
left join tbl_Department d on e.Department_Id = d.Id
where (e.Id=@Emp_Id or @Emp_Id=0) and (e.Department_Id=@Dept_Id or @Dept_Id=0)
FOR JSON PATH, ROOT('Details'));
END TRY    
    BEGIN CATCH    
          SET @JSONOUT = (    
            SELECT     
    ERROR_NUMBER() AS ErrorNumber,     
                ERROR_MESSAGE() AS ErrorMessage,    
                ERROR_SEVERITY() AS ErrorSeverity,    
                ERROR_STATE() AS ErrorState,    
                ERROR_LINE() AS ErrorLine,    
                ERROR_PROCEDURE() AS ErrorProcedure    
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER    
        );    
    END CATCH    
END    
GO
/****** Object:  StoredProcedure [dbo].[sp_Employee_Department_ManagementSystems]    Script Date: 09-12-2024 23:53:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[sp_Employee_Department_ManagementSystems]
(
@Id bigint = 0,
@Departmet_Name nvarchar(max) = null,
@Employee_Name nvarchar(max)=null,
@Employee_Salary nvarchar(max)=null,
@MODE CHAR(2) = '', -- E: Employee Add, D: Department Add, UE: Update Employee, UD: Update Department
@ErrorNo INT OUTPUT 
)
As begin
SET NOCOUNT ON;        
    DECLARE @ErrorFound BIT = 0; 
	declare @Dept_Id bigint;
	select @Dept_Id=Id from tbl_Employee where Id=@Id;
	begin try

	if (@MODE='E')
	BEGIN
	Insert into tbl_Employee (Employee_Name, Employee_Salary, Department_Id)
	values (@Employee_Name, @Employee_Salary, @Dept_Id)
	end

	else if (@MODE='D')
	Begin
	insert into tbl_Department(Department_Name) values (@Departmet_Name)
	end

	else if(@MODE='UE')
	begin
	IF EXISTS (SELECT * FROM tbl_Employee WHERE Id != @Id)        
            BEGIN        
                SET @ErrorNo = 2; -- Duplicate        
                SET @ErrorFound = 1;        
            END        
	update tbl_Employee set Employee_Name=@Employee_Name, Employee_Salary=@Employee_Salary
	where Id=@Id and Department_Id=@Dept_Id;
	end

	else if (@MODE='UD')
	begin
	IF EXists(select * from tbl_Department where Id != @Id)
	begin
	set @ErrorNo = 2; --Duplicate
	set @ErrorFound=1;
	end
	update tbl_Department set Department_Name=@Departmet_Name
	where Id=@Id
	end

	 IF (@ErrorFound = 0)        
        BEGIN        
            SET @ErrorNo = 0; -- No Error        
        END        
    END TRY        
    BEGIN CATCH        
        SET @ErrorNo = ERROR_NUMBER(); -- Set ErrorNo to the error number        
        SET @ErrorFound = 1; -- Indicate that an error occurred        
    END CATCH        
END;   
GO
