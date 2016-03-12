CREATE TABLE [dbo].[Users] (
    [Id]       INT           NOT NULL,
    [Username] NVARCHAR (20) NOT NULL,
    [Password] NCHAR (16)    NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Username] ASC)
);

