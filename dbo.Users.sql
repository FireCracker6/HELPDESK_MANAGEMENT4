CREATE TABLE [dbo].[Users] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [FirstName]   NVARCHAR (MAX)   NOT NULL,
    [LastName]    NVARCHAR (MAX)   NOT NULL,
    [Email]       NVARCHAR (MAX)   NULL,
    [PhoneNumber] NVARCHAR (MAX)   NULL,
    [CreatedAt]   DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

