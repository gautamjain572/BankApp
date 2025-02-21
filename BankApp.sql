
-- null problem --
SELECT * 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'AccountHolderDetails' 
AND COLUMN_NAME IN ('PANCard', 'AadharCard', 'ATMCardNumber');

SELECT * FROM AccountHolderDetails 
WHERE PANCard IS NULL OR AadharCard IS NULL OR ATMCardNumber IS NULL;

SELECT * FROM AccountHolderDetails  -- our case
WHERE PANCard = '' OR AadharCard = '' OR ATMCardNumber = '';

UPDATE AccountHolderDetails -- we cannot insert null here
SET PANCard = NULL
WHERE PANCard = '';

UPDATE AccountHolderDetails -- for fix
SET PANCard = 'TEMP12345F'
WHERE PANCard IS NULL OR PANCard = '';
-- null problem --
SELECT AccountID FROM AccountHolderDetails ORDER BY AccountID; -- check manually inserted ids
DBCC CHECKIDENT ('AccountHolderDetails', RESEED, 1); -- force ressed ids i sequecnal -- risky
CREATE SEQUENCE AccountID_Seq START WITH 1 INCREMENT BY 1; -- strict sequncal


-- create database
create database BankApp
-- create table 1
CREATE TABLE BankDetails (
    BankID INT PRIMARY KEY IDENTITY(1,1),
    BankName VARCHAR(255) NOT NULL,
    ISFCCode VARCHAR(255) NOT NULL,
    Branch VARCHAR(255) NOT NULL,
    BankAddress VARCHAR(255) NOT NULL,
    City VARCHAR(255) NOT NULL
);
-- create table 2
CREATE TABLE AccountHolderDetails (
    AccountID INT PRIMARY KEY IDENTITY(1,1),
    AccountHolderName VARCHAR(255) NOT NULL,
    Gender VARCHAR(10) CHECK (Gender IN ('Male', 'Female', 'Other')),
    AccountNumber VARCHAR(50) UNIQUE NOT NULL,
    BankID INT NOT NULL,  -- Foreign key referencing BankDetails
    Balance DECIMAL(18,2) DEFAULT 0.00,
	PANCard VARCHAR(10) UNIQUE NOT NULL,
    AadharCard VARCHAR(12) UNIQUE NOT NULL,
    ATMCardNumber VARCHAR(20) UNIQUE NOT NULL,
    CVV INT CHECK (CVV BETWEEN 100 AND 999),
    ATMPin CHAR(4) NOT NULL,
    PhoneNumber VARCHAR(20) UNIQUE NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    Address VARCHAR(500),
    CreatedDate DATETIME DEFAULT GETDATE(),
	CONSTRAINT FK_Bank FOREIGN KEY (BankID) REFERENCES BankDetails(BankID) ON DELETE CASCADE ON UPDATE CASCADE
);
-- select table data
select * from BankDetails
select * from AccountHolderDetails

--store procedure --

-- bank detail insert --
alter PROCEDURE InsertBankDetails
(  
    @BankName VARCHAR(255),  
    @ISFCCode VARCHAR(255),  
    @Branch VARCHAR(255),  
    @BankAddress VARCHAR(255),  
    @City VARCHAR(255),  
    -- Output parameters for success/failure status  
    @isBankAdded BIT OUTPUT,  
    @bankAddedStatus VARCHAR(400) OUTPUT  
)  
AS  
BEGIN  
    SET @isBankAdded = 0;  
    SET @bankAddedStatus = 'Something went wrong in SP';  
    -- Check for empty fields  
    IF @BankName = '' OR @ISFCCode = '' OR @Branch = '' OR @BankAddress = '' OR @City = ''  
    BEGIN  
        SET @bankAddedStatus = 'All fields are required';  
        RETURN;  
    END  
    -- Check for duplicate ISFC Code  
    IF EXISTS (SELECT 1 FROM BankDetails WHERE ISFCCode = @ISFCCode)  -- 1 is just a placeholder value --if true
    BEGIN  
        SET @bankAddedStatus = CONCAT('Duplicate ISFC Code is not allowed (', @ISFCCode, ')');  
        RETURN;  
    END  
    DECLARE @noOfBanksAdded INT = 0;  
    -- Insert bank details  
    INSERT INTO BankDetails (BankName, ISFCCode, Branch, BankAddress, City)  
    VALUES (@BankName, @ISFCCode, @Branch, @BankAddress, @City);  
    SET @noOfBanksAdded = @@ROWCOUNT;  
    SET @isBankAdded = IIF(@noOfBanksAdded > 0, 1, 0);  
    -- Success message  
    IF (@isBankAdded = 1)  
    BEGIN  
        SET @bankAddedStatus = CONCAT(@BankName, ' has been successfully added to BankDetails');  
    END  
END;
DECLARE @isBankAdded BIT, @bankAddedStatus VARCHAR(400); 
EXEC InsertBankDetails  
    @BankName = 'State Bank of India',  
    @ISFCCode = '76423746',  
    @Branch = 'Mumbai Main',  
    @BankAddress = '123, MG Road, Mumbai',  
    @City = 'Mumbai',  
    @isBankAdded = @isBankAdded OUTPUT,  
    @bankAddedStatus = @bankAddedStatus OUTPUT;  
select @isBankAdded as status , @bankAddedStatus as message; --execution bank detail insert

-- bank detail get --
CREATE PROCEDURE GetAllBankDetails
AS  
BEGIN  
    SET NOCOUNT ON;  
    SELECT BankID, BankName, ISFCCode, Branch, BankAddress, City  
    FROM BankDetails;  
END;
EXEC GetAllBankDetails; -- execution bank detail get

-- bank detail Update --
alter PROCEDURE UpdateBankDetails  
(  
    @ID INT,  
    @BankName VARCHAR(255),  
    @ISFCCode VARCHAR(255),  
    @Branch VARCHAR(255),  
    @BankAddress VARCHAR(255),  
    @City VARCHAR(255),  
    -- Output parameters for success/failure status  
    @isBankUpdated BIT OUTPUT,  
    @bankUpdateStatus VARCHAR(400) OUTPUT  
)  
AS  
BEGIN  
    SET @isBankUpdated = 0;  
    SET @bankUpdateStatus = 'Something went wrong in SP';  
    -- Check if the bank ID exists  
    IF NOT EXISTS (SELECT 1 FROM BankDetails WHERE BankID = @ID)  
    BEGIN  
        SET @bankUpdateStatus = CONCAT('Bank ID ', @ID, ' not found.');  
        RETURN;  
    END  
    -- Check for empty fields  
    IF @BankName = '' OR @ISFCCode = '' OR @Branch = '' OR @BankAddress = '' OR @City = ''  
    BEGIN  
        SET @bankUpdateStatus = 'All fields are required';  
        RETURN;  
    END  
    -- Check for duplicate ISFC Code (excluding current record)  
    IF EXISTS (SELECT 1 FROM BankDetails WHERE ISFCCode = @ISFCCode AND BankID <> @ID)  
    BEGIN  
        SET @bankUpdateStatus = CONCAT('Duplicate ISFC Code is not allowed (', @ISFCCode, ')');  
        RETURN;  
    END  
    -- Update the record  
    UPDATE BankDetails  
    SET  
        BankName = @BankName,  
        ISFCCode = @ISFCCode,  
        Branch = @Branch,  
        BankAddress = @BankAddress,  
        City = @City  
    WHERE BankID = @ID;  
    -- Check if any row was affected  
    IF @@ROWCOUNT > 0  
    BEGIN  
        SET @isBankUpdated = 1;  
        SET @bankUpdateStatus = CONCAT('Bank details updated successfully for ID ', @ID);  
    END  
END;
DECLARE @isBankUpdated BIT, @bankUpdateStatus VARCHAR(400); 
EXEC UpdateBankDetails  
    @ID = 1,  
    @BankName = 'HDFC Bank',  
    @ISFCCode = 'HDFC0001234',  
    @Branch = 'Chennai Main',  
    @BankAddress = '456, Anna Salai, Chennai',  
    @City = 'Chennai',  
    @isBankUpdated = @isBankUpdated OUTPUT,  
    @bankUpdateStatus = @bankUpdateStatus OUTPUT;  -- execution bank detail Update

-- Account holder details Insert --
alter PROCEDURE InsertAccountHolderDetails  
(  
    @AccountHolderName VARCHAR(255),  
    @Gender VARCHAR(10),  
    @AccountNumber VARCHAR(50),  
    @BankID INT,  
    @Balance DECIMAL(18,2) = 0.00,  
    @PANCard VARCHAR(10),  
    @AadharCard VARCHAR(12),  
    @ATMCardNumber VARCHAR(20),  
    @CVV INT,  
    @ATMPin CHAR(4),  --Have Default PIN is 1234  
    @PhoneNumber VARCHAR(20),  
    @Email VARCHAR(255),  
    @Address VARCHAR(500),  
    -- Output parameters for success/failure status  
    @isAccountAdded BIT OUTPUT,  
    @accountAddedStatus VARCHAR(400) OUTPUT  
)  
AS  
BEGIN  
    SET @isAccountAdded = 0;  
    SET @accountAddedStatus = 'Something went wrong in SP';  
    -- Check for empty required fields  
    IF @AccountHolderName = '' OR @Gender = '' OR @AccountNumber = '' OR @PANCard = '' OR   --here @AccountNumber should be need to null 
       @AadharCard = '' OR @ATMCardNumber = '' OR @CVV IS NULL OR @PhoneNumber = '' OR @Email = ''  -- also add mob or email uique validations
    BEGIN  
        SET @accountAddedStatus = 'All required fields must be provided';  
        RETURN;  
    END  
    -- Validate Gender  
    IF @Gender NOT IN ('Male', 'Female', 'Other')  
    BEGIN  
        SET @accountAddedStatus = 'Invalid gender value. Allowed: Male, Female, Other';  
        RETURN;  
    END  
    -- Check if BankID exists  
    IF NOT EXISTS (SELECT 1 FROM BankDetails WHERE BankID = @BankID)  
    BEGIN  
        SET @accountAddedStatus = CONCAT('Invalid BankID: ', @BankID);  
        RETURN;  
    END  
    -- Check for duplicate Account Number  
    IF EXISTS (SELECT 1 FROM AccountHolderDetails WHERE AccountNumber = @AccountNumber)  
    BEGIN  
        SET @accountAddedStatus = CONCAT('Duplicate Account Number not allowed (', @AccountNumber, ')');  
        RETURN;  
    END  
    -- Check for duplicate PAN Card  
    IF EXISTS (SELECT 1 FROM AccountHolderDetails WHERE PANCard = @PANCard)  
    BEGIN  
        SET @accountAddedStatus = CONCAT('Duplicate PAN Card not allowed (', @PANCard, ')');  
        RETURN;  
    END  
    -- Check for duplicate Aadhar Card  
    IF EXISTS (SELECT 1 FROM AccountHolderDetails WHERE AadharCard = @AadharCard)  
    BEGIN  
        SET @accountAddedStatus = CONCAT('Duplicate Aadhar Card not allowed (', @AadharCard, ')');  
        RETURN;  
    END  
    -- Check for duplicate ATM Card Number  
    IF EXISTS (SELECT 1 FROM AccountHolderDetails WHERE ATMCardNumber = @ATMCardNumber)  
    BEGIN  
        SET @accountAddedStatus = CONCAT('Duplicate ATM Card Number not allowed (', @ATMCardNumber, ')');  
        RETURN;  
    END 
	-- Check for duplicate Phone Number
    IF EXISTS (SELECT 1 FROM AccountHolderDetails WHERE PhoneNumber = @PhoneNumber)  
    BEGIN  
        SET @accountAddedStatus = CONCAT('Duplicate Phone Number not allowed (', @PhoneNumber, ')');  
        RETURN;  
    END
	-- Check for duplicate Email 
    IF EXISTS (SELECT 1 FROM AccountHolderDetails WHERE Email = @Email)  
    BEGIN  
        SET @accountAddedStatus = CONCAT('Duplicate Email not allowed (', @Email, ')');  
        RETURN;  
    END
    -- Insert account holder details  
    INSERT INTO AccountHolderDetails (AccountHolderName, Gender, AccountNumber, BankID, Balance, PANCard, AadharCard, ATMCardNumber, CVV, ATMPin, PhoneNumber, Email, Address, CreatedDate)  
    VALUES (@AccountHolderName, @Gender, @AccountNumber, @BankID, @Balance, @PANCard, @AadharCard, @ATMCardNumber, @CVV, @ATMPin, @PhoneNumber, @Email, @Address, GETDATE());  
    -- Check if insertion was successful  
    IF @@ROWCOUNT > 0  
    BEGIN  
        SET @isAccountAdded = 1;  
        SET @accountAddedStatus = CONCAT('Account created successfully for ', @AccountHolderName);  
    END  
END;
DECLARE @isAccountAdded BIT, @accountAddedStatus VARCHAR(400);
EXEC InsertAccountHolderDetails  
    @AccountHolderName = 'John Doe',  
    @Gender = 'Male',  --validate
    @AccountNumber = '1234567890129', --unique -- auto generate by api
    @BankID = 4,  
    @Balance = 5000.00,  -- minimum 2000
    @PANCard = 'dsasdas',  --unique
    @AadharCard = 'sadas4d',  --unique
    @ATMCardNumber = 'sd4a4dasd',  --unique
    @CVV = 123,  
    @ATMPin = '1234',  -- Will default to '1234' by api
    @PhoneNumber = 'w4323',  --unique
    @Email = 'john.doe@2343.com',  --unique
    @Address = '123, Main Street, New York',  
    @isAccountAdded = @isAccountAdded OUTPUT,  
    @accountAddedStatus = @accountAddedStatus OUTPUT;  
SELECT @isAccountAdded AS IsAccountAdded, @accountAddedStatus AS StatusMessage; -- execution Account holder details Insert

-- Account holder details Get --
CREATE PROCEDURE GetAllAccountHolders  
AS  
BEGIN  
    SELECT  
        AccountID,  
        AccountHolderName,  
        Gender,  
        AccountNumber,  
        BankID,  
        Balance,  
        PANCard,  
        AadharCard,  
        ATMCardNumber,  
        CVV,  
        ATMPin,  
        PhoneNumber,  
        Email,  
        Address,  
        CreatedDate  
    FROM AccountHolderDetails  
    ORDER BY CreatedDate DESC;  -- Latest accounts first  
END;
EXEC GetAllAccountHolders; -- execution Account holder details

-- Account holder details Update --
alter PROCEDURE UpdateAccountHolderDetails
(
    @AccountID INT,
    @AccountHolderName VARCHAR(255),
    @Gender VARCHAR(10),
    @AccountNumber VARCHAR(50),
    @BankID INT,
    @PANCard VARCHAR(10),
    @AadharCard VARCHAR(12),
    @ATMCardNumber VARCHAR(20),
    @CVV INT,
    @ATMPin CHAR(4),
    @PhoneNumber VARCHAR(20),
    @Email VARCHAR(255),
    @Address VARCHAR(500),
    -- Output parameters
    @isUpdateSuccessful BIT OUTPUT,
    @updateStatusMessage VARCHAR(400) OUTPUT
)
AS
BEGIN
    --SET NOCOUNT ON;
    -- Default response values
    SET @isUpdateSuccessful = 0;
    SET @updateStatusMessage = 'Something went wrong. Please try again.';

    -- Check if the account exists
    IF NOT EXISTS (SELECT 1 FROM AccountHolderDetails WHERE AccountID = @AccountID)
    BEGIN
        SET @updateStatusMessage = 'Invalid Account ID.';
        RETURN;
    END
    -- Validate Gender
    IF @Gender NOT IN ('Male', 'Female', 'Other')
    BEGIN
        SET @updateStatusMessage = 'Invalid gender. It must be Male, Female, or Other.';
        RETURN;
    END
    -- Validate Bank ID
    IF NOT EXISTS (SELECT 1 FROM BankDetails WHERE BankID = @BankID)
    BEGIN
        SET @updateStatusMessage = 'Invalid Bank ID.';
        RETURN;
    END
    -- Validate Unique Constraints
    IF EXISTS (SELECT 1 FROM AccountHolderDetails WHERE AccountNumber = @AccountNumber AND AccountID <> @AccountID)
    BEGIN
        SET @updateStatusMessage = 'Account Number already exists.';
        RETURN;
    END
    IF EXISTS (SELECT 1 FROM AccountHolderDetails WHERE PANCard = @PANCard AND AccountID <> @AccountID)
    BEGIN
        SET @updateStatusMessage = 'PAN Card already exists.';
        RETURN;
    END
    IF EXISTS (SELECT 1 FROM AccountHolderDetails WHERE AadharCard = @AadharCard AND AccountID <> @AccountID)
    BEGIN
        SET @updateStatusMessage = 'Aadhar Card already exists.';
        RETURN;
    END
    IF EXISTS (SELECT 1 FROM AccountHolderDetails WHERE ATMCardNumber = @ATMCardNumber AND AccountID <> @AccountID)
    BEGIN
        SET @updateStatusMessage = 'ATM Card Number already exists.';
        RETURN;
    END
    IF EXISTS (SELECT 1 FROM AccountHolderDetails WHERE PhoneNumber = @PhoneNumber AND AccountID <> @AccountID)
    BEGIN
        SET @updateStatusMessage = 'Phone Number already exists.';
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM AccountHolderDetails WHERE Email = @Email AND AccountID <> @AccountID)
    BEGIN
        SET @updateStatusMessage = 'Email already exists.';
        RETURN;
    END
    -- Validate CVV
    IF @CVV < 100 OR @CVV > 999
    BEGIN
        SET @updateStatusMessage = 'Invalid CVV. It must be a 3-digit number.';
        RETURN;
    END
    -- Update the AccountHolderDetails table
    UPDATE AccountHolderDetails
    SET 
        AccountHolderName = @AccountHolderName,
        Gender = @Gender,
        AccountNumber = @AccountNumber,
        BankID = @BankID,
        PANCard = @PANCard,
        AadharCard = @AadharCard,
        ATMCardNumber = @ATMCardNumber,
        CVV = @CVV,
        ATMPin = @ATMPin,
        PhoneNumber = @PhoneNumber,
        Email = @Email,
        Address = @Address
    WHERE AccountID = @AccountID;
    -- Check if update was successful
    IF @@ROWCOUNT > 0
    BEGIN
        SET @isUpdateSuccessful = 1;
        SET @updateStatusMessage = 'Account details updated successfully.';
    END
END;
DECLARE @isUpdateSuccessful BIT, @updateStatusMessage VARCHAR(400);
EXEC UpdateAccountHolderDetails  
    @AccountID = 8,  
    @AccountHolderName = 'John Doe',  
    @Gender = 'Male',  
    @AccountNumber = 'ACC123456789',  
    @BankID = 2,  
    @PANCard = '',  
    @AadharCard = '',  
    @ATMCardNumber = '',  
    @CVV = 456,  
    @ATMPin = '',  
    @PhoneNumber = '',  
    @Email = 'john.doe@.com',  
    @Address = '123 Main Street, NY',  
    @isUpdateSuccessful = @isUpdateSuccessful OUTPUT,  
    @updateStatusMessage = @updateStatusMessage OUTPUT;  
SELECT @isUpdateSuccessful AS IsUpdateSuccessful, @updateStatusMessage AS StatusMessage; -- execution Account holder details Update

-- Withdraw Amount API
CREATE PROCEDURE WithdrawAmount
(
    @ATMCardNumber VARCHAR(20),
    @CVV INT,
    @ATMPin CHAR(4),
    @WithdrawalAmount DECIMAL(18,2),
    -- Output parameters to return status
    @isWithdrawalSuccessful BIT OUTPUT,
    @withdrawalStatusMessage VARCHAR(400) OUTPUT
)
AS
BEGIN
    SET @isWithdrawalSuccessful = 0;
    SET @withdrawalStatusMessage = 'Something went wrong. Please try again.';
    -- Check if the ATM card details are valid
    IF NOT EXISTS (
        SELECT 1 FROM AccountHolderDetails 
        WHERE ATMCardNumber = @ATMCardNumber AND CVV = @CVV AND ATMPin = @ATMPin
    )
    BEGIN
        SET @withdrawalStatusMessage = 'Invalid ATM details. Please check your ATM Card Number, CVV, or PIN.';
        RETURN;
    END
    -- Check if sufficient balance is available
    DECLARE @CurrentBalance DECIMAL(18,2);
    SELECT @CurrentBalance = Balance 
    FROM AccountHolderDetails 
    WHERE ATMCardNumber = @ATMCardNumber;
    IF @CurrentBalance < @WithdrawalAmount
    BEGIN
        SET @withdrawalStatusMessage = 'Insufficient balance. Withdrawal failed.';
        RETURN;
    END
    -- Deduct the withdrawal amount from the balance
    UPDATE AccountHolderDetails
    SET Balance = Balance - @WithdrawalAmount
    WHERE ATMCardNumber = @ATMCardNumber;
    -- Check if the update was successful
    IF @@ROWCOUNT > 0
    BEGIN
        SET @isWithdrawalSuccessful = 1;
        SET @withdrawalStatusMessage = CONCAT('Withdrawal of ', @WithdrawalAmount, ' successful. Remaining balance: ', @CurrentBalance - @WithdrawalAmount);
    END
END;
DECLARE @isWithdrawalSuccessful BIT, @withdrawalStatusMessage VARCHAR(400);
EXEC WithdrawAmount  
    @ATMCardNumber = '4000123412341234',  
    @CVV = 123,  
    @ATMPin = '1234',  
    @WithdrawalAmount = 5000,  
    @isWithdrawalSuccessful = @isWithdrawalSuccessful OUTPUT,  
    @withdrawalStatusMessage = @withdrawalStatusMessage OUTPUT;  
SELECT @isWithdrawalSuccessful AS IsWithdrawalSuccessful, @withdrawalStatusMessage AS StatusMessage; -- execution Withdraw Amount

-- Deposit Money API 
alter PROCEDURE DepositAmount
(
    @AccountNumber VARCHAR(50),
    @AccountHolderName VARCHAR(255),
    @DepositAmount DECIMAL(18,2), 
    -- Output parameters
    @isDepositSuccessful BIT OUTPUT,
    @depositStatusMessage VARCHAR(400) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    -- Default response values
    SET @isDepositSuccessful = 0;
    SET @depositStatusMessage = 'Something went wrong. Please try again.';
    -- Validate account existence
    IF NOT EXISTS (
        SELECT 1 FROM AccountHolderDetails 
        WHERE AccountNumber = @AccountNumber AND AccountHolderName = @AccountHolderName
    )
    BEGIN
        SET @depositStatusMessage = 'Invalid Account Number or Account Holder Name.';
        RETURN;
    END

    -- Ensure deposit amount is greater than ₹100
    IF @DepositAmount IS NULL OR @DepositAmount <= 100
    BEGIN
        SET @depositStatusMessage = 'Invalid deposit amount. It must be greater than ₹100.';
        RETURN;
    END
    -- Update balance
    UPDATE AccountHolderDetails
    SET Balance = Balance + @DepositAmount
    WHERE AccountNumber = @AccountNumber;
    -- Check if the update was successful
    IF @@ROWCOUNT > 0
    BEGIN
        SET @isDepositSuccessful = 1;
        SET @depositStatusMessage = 'Deposit successful.';
    END
END;
DECLARE @isDepositSuccessful BIT, @depositStatusMessage VARCHAR(400);
EXEC DepositAmount  
    @AccountNumber = '123456789012',  
    @AccountHolderName = 'John Doe',  
    @DepositAmount = 5000,  
    @isDepositSuccessful = @isDepositSuccessful OUTPUT,  
    @depositStatusMessage = @depositStatusMessage OUTPUT;  
SELECT @isDepositSuccessful AS IsDepositSuccessful, @depositStatusMessage AS StatusMessage; -- execution Deposit Money


-- for R
select * from BankDetails
select * from AccountHolderDetails






