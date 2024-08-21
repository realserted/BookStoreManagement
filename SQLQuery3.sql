CREATE TABLE [dbo].[OrderItems] (
    [order_item_id] INT IDENTITY (1, 1) NOT NULL,
    [o_id]          INT NOT NULL,
    [i_id]          INT NOT NULL,
    [quantity]      INT NOT NULL,
    PRIMARY KEY CLUSTERED ([order_item_id] ASC),
    FOREIGN KEY ([o_id]) REFERENCES [dbo].[Orders] ([o_id]),
    FOREIGN KEY ([i_id]) REFERENCES [dbo].[Items] ([i_id])
);

DBCC CHECKIDENT ('[ORDERS]', RESEED, 1007); 

ALTER TABLE Orders
DROP COLUMN o_id;

ALTER TABLE Orders
ADD o_id INT IDENTITY(1,1) PRIMARY KEY;

SELECT MAX(o_id) + 1 FROM Orders;
