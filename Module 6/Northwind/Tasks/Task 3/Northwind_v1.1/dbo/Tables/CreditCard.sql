CREATE TABLE [dbo].[CreditCard] (
    [CreditCardID]     INT           IDENTITY (1, 1) NOT NULL,
    [CreditCardNumber] NVARCHAR (12) NOT NULL,
    [ExpiryDate]       DATETIME      NOT NULL,
    [CardHolderName]   NVARCHAR (20) NOT NULL,
    [HolderID]         INT           NULL,
    CONSTRAINT [FK_CreditCard_Employee] FOREIGN KEY ([HolderID]) REFERENCES [dbo].[Employees] ([EmployeeID])
);

