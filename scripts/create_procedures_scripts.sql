CREATE PROCEDURE Accounting.GetAllPositions
AS
BEGIN
    SELECT * FROM Accounting.Positions;
END
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

CREATE PROCEDURE Accounting.GetAllDepartments
AS
BEGIN
    SELECT * FROM Accounting.Departments;
END
GO

CREATE PROCEDURE Accounting.GetDepartmentsByPosition
@PositionIdentifier INT
AS
BEGIN
    SELECT d.*
    FROM Accounting.Departments d
    WHERE d.Name IN (
        SELECT DISTINCT dep.Name
        FROM Accounting.Employees e
                 INNER JOIN Accounting.Departments dep ON e.DepartmentIdentifier = dep.Identifier
        WHERE e.PositionIdentifier = @PositionIdentifier
    );
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
           Patronymic LIKE '%' + @SearchTerm + '%');
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