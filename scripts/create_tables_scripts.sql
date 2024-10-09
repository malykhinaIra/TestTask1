CREATE SCHEMA Accounting;
GO

CREATE TABLE Accounting.Departments
(
    Identifier INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    CONSTRAINT UK_Department_Name UNIQUE(Name)
)
GO

CREATE TABLE Accounting.Positions
(
    Identifier INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    CONSTRAINT UK_Position_Title UNIQUE(Title)
)
GO

CREATE TABLE Accounting.Employees
(
    Identifier INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    Patronymic NVARCHAR(255) NULL,
    Address NVARCHAR(255) NULL,
    DateOfBirth DATE NOT NULL,
    DateOfEmployment DATE NOT NULL,
    PhoneNumber NVARCHAR(255) NOT NULL,
    Salary DECIMAL(18, 2) NOT NULL,
    DepartmentIdentifier INT NOT NULL,
    PositionIdentifier INT NOT NULL,
    CONSTRAINT FK_Employee_Position FOREIGN KEY (PositionIdentifier) REFERENCES Accounting.Positions(Identifier),
)
GO
