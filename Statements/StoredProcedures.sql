Use SnapPex;

GO

CREATE PROCEDURE spInsertComponent
	@ComponentName NVARCHAR(50),
	@AmountPerMachine INT,
	@AmountInStock INT,
	@ImagePath NVARCHAR(255)
AS
BEGIN
	INSERT INTO COMPONENT(ComponentName, AmountPerMachine, AmountInStock, ImagePath)
	VALUES(@ComponentName, @AmountPerMachine, @AmountInStock, @ImagePath);
END;

GO

CREATE PROCEDURE spInsertMachine
	@MachineNr INT,
	@MachineStatus INT
AS
BEGIN
	INSERT INTO MACHINE(MachineNr, MachineStatus)
	VALUES(@MachineNr, @MachineStatus)
END


GO

CREATE PROCEDURE spInsertScrew
	@RingClipSize INT,
	@AmountInStock INT
AS
BEGIN
	INSERT INTO SCREW(RingClipSize, AmountInStock)
	VALUES(@RingClipSize, @AmountInStock);
END;

GO

CREATE PROCEDURE spInsertRental
	@CompanyName NVARCHAR(100),
	@StartDate DATE,
	@EndDate DATE
AS
BEGIN
	INSERT INTO RENTAL(CompanyName, StartDate, EndDate)
	VALUES(@CompanyName, @StartDate, @EndDate)
END

GO