using NorthWind.Core.Models;
using NorthWind.Repository;
using NorthWind.Repository.Repositories;
using NorthWind.Service.Services;

Service<Products> service = new Service<Products>();

Products product = new Products()
{
    CategoryID = 1,
    Discontinued = false,
    ProductName = "new product",
    ID = 0,
    QuantityPerUnit = "",
    ReorderLevel = 0,
    SupplierID = 1,
    UnitPrice = 10,
    UnitsInStock = 10
};

Products newProduct = await service.AddAsync(product);

List<Products> products = await service.GetAllAsync();

if (AppDbContext.Connection.State == System.Data.ConnectionState.Open)
    AppDbContext.Connection.Close();

Console.ReadLine();