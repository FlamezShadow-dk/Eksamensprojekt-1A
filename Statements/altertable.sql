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
BEGINs
	INSERT INTO COMPONENT(ComponentName, AmountPerMachine, AmountInStock, ImagePath)
	VALUES(@ComponentName, @AmountPerMachine, @AmountInStock, @ImagePath);
END
GO

ALTER PROCEDURE spInsertMachine
	@MachineStatus INT
AS
BEGIN
	INSERT INTO MACHINE(MachineStatus)
	VALUES(@MachineStatus)
	SELECT SCOPE_IDENTITY()
END