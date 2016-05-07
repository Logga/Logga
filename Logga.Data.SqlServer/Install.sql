IF NOT EXISTS (select * from sysobjects where name='LoggaEntry' and xtype='U')
		CREATE TABLE [dbo].[LoggaEntry](
			[LoggaEntryId] [bigint] IDENTITY(1,1) NOT NULL,
			[DateError] [datetime] NOT NULL,
			[Source] [nvarchar](max) NULL,
			[Target] [nvarchar](max) NULL,
			[Type] [nvarchar](max) NULL,
			[Message] [nvarchar](max) NULL,
			[StackTrace] [nvarchar](max) NULL,
			[User] [nvarchar](max) NULL,
			[Host] [nvarchar](max) NULL,
			[InnerException] [nvarchar](max) NULL,
		 CONSTRAINT [PK_dbo.LoggaEntry] PRIMARY KEY CLUSTERED 
		(
			[LoggaEntryId] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
