CREATE TABLE [dbo].[Posts] (
    [Id]     INT            NOT NULL,
    [Title]  NVARCHAR (250) NOT NULL,
    [Date]   DATETIME2 (7)  NOT NULL,
    [Body]   NVARCHAR (MAX) NOT NULL,
    [UserId] INT            NOT NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

