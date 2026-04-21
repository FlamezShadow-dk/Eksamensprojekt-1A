Use SnapPex;

GO

CREATE PROCEDURE spInsertComponent
	@ComponentName NVARCHAR(50),
	@AmountPerMachine INT,
	@AmountInStock INT
AS
BEGIN
	INSERT INTO COMPONENT(ComponentName, AmountPerMachine, AmountInStock)
	VALUES(@ComponentName, @AmountPerMachine, @AmountInStock);
END;