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



INSERT INTO Accounting.Departments (Name)
VALUES
    (N'Фінансовий відділ'),
    (N'Відділ кадрів'),
    (N'Відділ маркетингу');

INSERT INTO Accounting.Positions (Title)
VALUES
    (N'Менеджер'),
    (N'Аналітик'),
    (N'Керівник'),
    (N'Бухгалтер'),
    (N'Рекрутер');

INSERT INTO Accounting.Employees (FirstName, LastName, Patronymic, Address, DateOfBirth, DateOfEmployment, PhoneNumber, Salary, DepartmentIdentifier, PositionIdentifier)
VALUES
    (N'Олександр', N'Іванов', N'Іванович', N'Київ, вул. Шевченка, 1', '1985-05-10', '2010-02-01', N'+380501234567', 15000.00, 1, 1),
    (N'Марія', N'Петренко', N'Сергіївна', N'Львів, пр. Свободи, 2', '1990-08-20', '2015-03-15', N'+380505678912', 12000.00, 2, 2),
    (N'Ігор', N'Сидоров', N'Петрович', N'Одеса, вул. Дерибасівська, 3', '1987-03-25', '2012-06-10', N'+380501234890', 14000.00, 1, 3),
    (N'Наталія', N'Коваль', N'Олексіївна', N'Харків, вул. Пушкіна, 4', '1982-12-30', '2008-11-20', N'+380507654321', 16000.00, 3, 4),
    (N'Олег', N'Семенко', N'Володимирович', N'Дніпро, вул. Грушевського, 5', '1995-07-05', '2020-01-01', N'+380509876543', 11000.00, 2, 1),
    (N'Анна', N'Мельник', N'Ігорівна', N'Запоріжжя, вул. Незалежності, 6', '1992-02-14', '2019-05-01', N'+380504321678', 13000.00, 1, 2),
    (N'Сергій', N'Дзюба', N'Степанович', N'Полтава, вул. Леніна, 7', '1989-10-15', '2016-04-05', N'+380509876543', 17000.00, 3, 3),
    (N'Тетяна', N'Громова', N'Анатоліївна', N'Суми, вул. Котляревського, 8', '1991-09-18', '2018-07-07', N'+380503456789', 12500.00, 2, 4),
    (N'Владислав', N'Борисенко', N'Олександрович', N'Черкаси, вул. Шевченка, 9', '1984-11-21', '2013-10-10', N'+380501234567', 14500.00, 1, 5),
    (N'Юлія', N'Савченко', N'Ігорівна', N'Чернівці, вул. Федьковича, 10', '1986-04-28', '2011-08-16', N'+380507654321', 15500.00, 3, 1);



CREATE PROCEDURE Accounting.GetAllPositions
AS
BEGIN
    SELECT * FROM Accounting.Positions;
END
GO

CREATE PROCEDURE Accounting.GetAllDepartments
AS
BEGIN
    SELECT * FROM Accounting.Departments;
END
GO

CREATE PROCEDURE Accounting.GetEmployees
    @DepartmentIdentifier INT = NULL,
    @PositionIdentifier INT = NULL,
    @SearchTerm NVARCHAR(255) = NULL
AS
BEGIN
    SELECT *
    FROM Accounting.Employees
    WHERE (@DepartmentIdentifier IS NULL OR DepartmentIdentifier = @DepartmentIdentifier)
      AND (@PositionIdentifier IS NULL OR PositionIdentifier = @PositionIdentifier)
      AND (@SearchTerm IS NULL OR
           FirstName LIKE '%' + @SearchTerm + '%' OR
           LastName LIKE '%' + @SearchTerm + '%' OR
           PhoneNumber LIKE '%' + @SearchTerm + '%');
END;
GO

CREATE PROCEDURE Accounting.GetEmployee
@Identifier INT
AS
BEGIN
    SELECT *
    FROM Accounting.Employees
    WHERE (Identifier = @Identifier);
END;
GO

CREATE PROCEDURE Accounting.GetPosition
@Identifier INT
AS
BEGIN
    SELECT *
    FROM Accounting.Positions
    WHERE (Identifier = @Identifier);
END;
GO

CREATE PROCEDURE Accounting.GetPositionsByDepartment
@DepartmentIdentifier INT
AS
BEGIN
    SELECT p.*
    FROM Accounting.Positions p
    WHERE p.Title IN (
        SELECT DISTINCT pos.Title
        FROM Accounting.Employees e
                 INNER JOIN Accounting.Positions pos ON e.PositionIdentifier = pos.Identifier
        WHERE e.DepartmentIdentifier = @DepartmentIdentifier
    );
END;
GO

CREATE PROCEDURE Accounting.GetDepartment
@Identifier INT
AS
BEGIN
    SELECT *
    FROM Accounting.Departments
    WHERE (Identifier = @Identifier);
END;
GO

CREATE PROCEDURE Accounting.UpdateEmployee
    @Identifier INT,
    @FirstName NVARCHAR(255),
    @LastName NVARCHAR(255),
    @Patronymic NVARCHAR(255),
    @Address NVARCHAR(255),
    @DateOfBirth DATE,
    @DateOfEmployment DATE,
    @PhoneNumber NVARCHAR(50),
    @Salary DECIMAL(18, 2),
    @DepartmentIdentifier INT,
    @PositionIdentifier INT
AS
BEGIN
    UPDATE Accounting.Employees
    SET
        FirstName = COALESCE(@FirstName, FirstName),
        LastName = COALESCE(@LastName, LastName),
        Patronymic = COALESCE(@Patronymic, Patronymic),
        Address = COALESCE(@Address, Address),
        DateOfBirth = COALESCE(@DateOfBirth, DateOfBirth),
        DateOfEmployment = COALESCE(@DateOfEmployment, DateOfEmployment),
        PhoneNumber = COALESCE(@PhoneNumber, PhoneNumber),
        Salary = COALESCE(@Salary, Salary),
        DepartmentIdentifier = COALESCE(@DepartmentIdentifier, DepartmentIdentifier),
        PositionIdentifier = COALESCE(@PositionIdentifier, PositionIdentifier)
    WHERE Identifier = @Identifier;
END;
GO

CREATE PROCEDURE Accounting.GetSalaryReport
    @DepartmentIdentifier INT = NULL,
    @PositionIdentifier INT = NULL
AS
BEGIN
    SELECT SUM(Salary) AS TotalSalary,
           AVG(Salary) AS AverageSalary,
           MIN(Salary) AS MinSalary,
           MAX(Salary) AS MaxSalary
    FROM Accounting.Employees
    WHERE (@DepartmentIdentifier IS NULL OR DepartmentIdentifier = @DepartmentIdentifier)
      AND (@PositionIdentifier IS NULL OR PositionIdentifier = @PositionIdentifier)
END;
GO
