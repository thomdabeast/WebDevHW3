CREATE TABLE [dbo].[Table] (
    [Id]    INT            NOT NULL,
    [Title] NVARCHAR (250) NOT NULL,
    [Date]  DATETIME       NOT NULL,
    [Body]  NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

