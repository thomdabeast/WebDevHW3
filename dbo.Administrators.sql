CREATE TABLE [dbo].[Administrators] (
    [Id] INT NOT NULL,
    FOREIGN KEY ([Id]) REFERENCES [dbo].[Users] ([Id])
);

