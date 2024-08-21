INSERT INTO dbo.Customer (c_name, c_email, c_address, c_username, c_password)
VALUES ('Frank Garcia', 'fg@yahoo.com', 'Pardo, Cebu City', 'fg123', 'secret'),
       ('Niño Monta', 'nm123@yahoo.com', 'Talisay City, Cebu', 'nm1234', 'password123'),
       ('Jun Mar Lemosnero', 'jml69@gmail.com', 'Tuyabang, Oroquieta City', 'jml789', 'jmlsecret'),
       ('Jesus Villaceran', 'omegazigma@gmail.com', 'Consolacion, Cebu', 'jv2000', 'omega123'),
       ('Rose Ygot', 'Rosywhite@gmail.com', 'Lapu-lapu City, Cebu', 'rosy888', 'rosepassword'),
       ('Roger Intong', 'rogint@gmail.com', 'Tanjay, Negros Occ.', 'rint098', 'roger123');

INSERT INTO dbo.Items (isbn, title, author, genre, price, i_type)
VALUES (980, 'Data Structures and Algorithms', 'Malik', 'Education', 500.00, 'Book'),
       (124, 'Harry Potter', 'JK Rowling', 'Fantasy, Drama, Fiction', 700.00, 'Book'),
       (125, 'Snow White and the Seven Dwarfs', 'Grimm', 'Fantasy, Drama, Fiction', 300.00, 'Ebook'),
       (126, 'The Legends End', 'Keishi Otomo', 'Action, Fantasy', 350.00, 'Audiobook'),
       (127, 'Snow White and the Seven Dwarfs', 'Grimm', 'Fantasy, Drama, Fiction', 350.00, 'Audiobook'),
       (128, 'Harry Potter', 'JK Rowling', 'Fantasy, Drama, Fiction', 400.00, 'Ebook'),
       (129, 'The Legends End', 'Keishi Otomo', 'Action, Fantasy', 450.00, 'Book');

INSERT INTO [dbo].[Orders] (o_date, ship_address, c_id)
VALUES
    ('2023-06-10', N'Pardo, Cebu City', 1),
    ('2023-07-10', N'Talisay City, Cebu', 3),
    ('2023-07-10', N'Oroquieta City', 2),
    ('2023-08-10', N'Consolacion, Cebu', 4),
    ('2023-08-10', N'Lapu-lapu City, Cebu', 5),
    ('2023-08-10', N'Tanjay, Negros Occ.', 6);

EXEC DeleteCustomer @c_id = 9;

CREATE TABLE [dbo].[Admin] (
    [a_id]       INT            IDENTITY (1, 1) NOT NULL,
    [a_username] NVARCHAR (50)  NULL,
    [a_password] NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([c_id] ASC)
);

