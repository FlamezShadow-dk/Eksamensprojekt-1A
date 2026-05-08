Use SnapPex
GO

ALTER TABLE Component
Add ImagePath NVARCHAR(255);
GO

ALTER PROCEDURE spInsertComponent
	@ComponentName NVARCHAR(50),
	@AmountPerMachine INT,
	@AmountInStock INT,
	@ImagePath NVARCHAR(255)
AS
BEGIN
	INSERT INTO COMPONENT(ComponentName, AmountPerMachine, AmountInStock, ImagePath)
	VALUES(@ComponentName, @AmountPerMachine, @AmountInStock, @ImagePath);
END
GO

ALTER TABLE Machine
ADD ProductionReference NVARCHAR(10) NOT NULL
DEFAULT '';
GO

ALTER PROCEDURE spInsertMachine
	@MachineStatus INT,
	@ProductionReference NVARCHAR(10)
AS
BEGIN
	INSERT INTO MACHINE(MachineStatus, ProductionReference)
	VALUES(@MachineStatus, @ProductionReference)
	SELECT SCOPE_IDENTITY()
END
GO
