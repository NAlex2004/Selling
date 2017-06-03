
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE AddTempSale
	@SessionId uniqueidentifier,
	@Date datetime,
	@ManagerName varchar(100),
	@CustomerName varchar(255),
	@ProductName varchar(255),
	@Total float = 0,
	@result int output
	--@errMsg varchar(100) output
	-- @result < 0 - insertion	failed
AS
BEGIN
	SET NOCOUNT ON;

	set @result = -1

    declare @ManagerId int
	declare @CustomerId int
	declare @ProductId int
	declare @IdentityTable table (Id int)

	select @ManagerId = m.Id
	from Managers m
	where m.LastName = @ManagerName

	select @CustomerId = c.Id
	from Customers c
	where c.CustomerName = @CustomerName

	select @ProductId = p.Id
	from Products p
	where p.ProductName = @ProductName

	BEGIN TRANSACTION Tr

	BEGIN TRY

		if (@ManagerId is null)
		begin
			insert into Managers (LastName)
			  output inserted.Id into @IdentityTable
			values (@ManagerName)

			select @ManagerId = Id 
			from @IdentityTable
		end

		if (@CustomerId is null)
		begin
			insert into Customers (CustomerName)
			  output inserted.Id into @IdentityTable
			values (@CustomerName)

			select @CustomerId = Id 
			from @IdentityTable
		end

		if (@ProductId is null)
		begin
			insert into Products (ProductName, Price)
			  output inserted.Id into @IdentityTable
			values (@ProductName, @Total)

			select @ProductId = Id 
			from @IdentityTable
		end
		 
		insert into tmpSales (SessionId, SaleDate, ManagerId, CustomerId, ProductId, Total)
		values (@SessionId, @Date, @ManagerId, @CustomerId, @ProductId, @Total)

		COMMIT TRANSACTION Tr

		set @result = 0

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION Tr
	END CATCH

END
GO
