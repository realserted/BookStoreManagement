CREATE TABLE [dbo].[Admin] (
    [admin_id]      INT            IDENTITY (1, 1) NOT NULL,
    [admin_name]    NVARCHAR (50)  NULL,
    [admin_email]   NVARCHAR (100) NULL,
    [admin_username] NVARCHAR (50)  NULL,
    [admin_password] NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([admin_id] ASC)
);

INSERT INTO [Admin] ([admin_name], [admin_email], [admin_username], [admin_password])
VALUES ('Admin', 'admin@example.com', 'admin', 'admin123');