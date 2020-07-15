CREATE TABLE [RealCompare].[j_MainKass](
	[id]			int				IDENTITY(1,1) NOT NULL,
	[Data]			date			not null,
	[MainKass]		numeric(16,2)	not null,
	[ChessBoard]	numeric(16,2)	null,
	[RealSQL]		numeric(16,2)	null,
	[isVVO]			bit				not null default 0,
	[id_Creator]	int				not	null,
	[DateCreate]	datetime		null,
	[id_Editor]		int				null,
	[DateEdit]		datetime		null,
 CONSTRAINT [PK_j_MainKass] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [RealCompare].[j_MainKass] ADD CONSTRAINT FK_j_MainKass_id_Creator FOREIGN KEY (id_Creator)  REFERENCES [dbo].[ListUsers] (id)
GO

ALTER TABLE [RealCompare].[j_MainKass] ADD CONSTRAINT FK_j_MainKass_id_Editor FOREIGN KEY (id_Editor)  REFERENCES [dbo].[ListUsers] (id)
GO


CREATE TABLE [RealCompare].[j_RequestOfDifference](
	[id]				int				IDENTITY(1,1) NOT NULL,
	[id_MainKass]		int				not	null,
	[id_RequestRepair]	int				not	null,
	[isVVO]				bit				not null default 0,
	[SourceDifference]	int				not	null default 1,
 CONSTRAINT [PK_j_RequestOfDifference] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [RealCompare].[j_RequestOfDifference] ADD CONSTRAINT FK_j_RequestOfDifference_id_MainKass FOREIGN KEY (id_MainKass)  REFERENCES [RealCompare].[j_MainKass] (id)
GO

ALTER TABLE [RealCompare].[j_RequestOfDifference] ADD CONSTRAINT FK_j_RequestOfDifference_id_RequestRepair FOREIGN KEY (id_RequestRepair)  REFERENCES [Repair].[j_RequestRepair] (id)
GO