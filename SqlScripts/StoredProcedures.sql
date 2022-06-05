
create proc prc_Products_Select
as
begin
	select ProductName from Products
end
go


alter proc prc_Products_Insert
(
	@ID int,
	@ProductName nvarchar(40),
	@SupplierID int,
	@CategoryID int,
	@QuantityPerUnit nvarchar(20),
	@UnitPrice money,
	@UnitsInStock smallint,
	@UnitsOnOrder smallint,
	@ReorderLevel smallint,
	@Discontinued bit
)
as
begin
	insert into Products values
	(@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, @Discontinued)

	select * from Products where ID = SCOPE_IDENTITY()

end