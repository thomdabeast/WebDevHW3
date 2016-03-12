CREATE TABLE [dbo].[Comments] (
    [Id]     INT            NOT NULL,
    [PostID] INT            NOT NULL,
    [Date]   NVARCHAR (128) NOT NULL,
    [Body]   NVARCHAR (MAX) NOT NULL,
    [Name]   NVARCHAR (128) NOT NULL,
    [UserID] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([PostID]) REFERENCES [dbo].[Posts] ([Id]) ON DELETE CASCADE,
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);

