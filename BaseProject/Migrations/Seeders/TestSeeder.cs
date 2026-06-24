using System.Security.Cryptography;
using BaseProject.Models.Data;
using BaseProject.Data;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Enums;
using BaseProject.Models.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Migrations.Seeders;

public static class TestSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ProjectDbContext>();

        SeedBarCode(context, 100);
        SeedBranchOffice(context, 10);
        SeedCustomers(context, 10);
        SeedProviders(context, 10);

        SeedJustifies(context);
        SeedWareHouses(context, 10);
        await context.SaveChangesAsync();

        SeedProducts(context, 10);
        await context.SaveChangesAsync();

        SeedSaleOrders(context, 10);
        await context.SaveChangesAsync();
    }

    #region BarCodes

    private static void SeedBarCode(ProjectDbContext context, int total)
    {
        for (var i = 0; i < total; i++)
        {
            context.BarCodes.Add(new BarCode()
            {
                Code = "BC" + Guid.NewGuid().ToString()[..10],
                InternalCode = "IC" + Guid.NewGuid().ToString()[..5],
            });
        }
    }

    #endregion

    #region BranchOffices

    private static void SeedBranchOffice(ProjectDbContext context, int total)
    {
        List<string> countries = ["SV", "HN", "NI"];
        for (var i = 0; i < total; i++)
        {
            context.BranchOffices.Add(new BranchOffice
            {
                Code = "BR" + Guid.NewGuid().ToString()[..3],
                Name = "BRANCH " + Guid.NewGuid().ToString()[..5],
                Country = countries.GetRandomElement(),
            });
        }
    }

    #endregion

    #region Customers

    private static void SeedCustomers(ProjectDbContext context, int total)
    {
        for (var i = 0; i < total; i++)
        {
            context.Customers.Add(new Customer
            {
                Code = "CSM" + Guid.NewGuid().ToString()[..(Customer.CodeLength - 3)],
                Name = "CUSTOMER " + Guid.NewGuid().ToString()[..5],
            });
        }
    }

    #endregion

    #region Justifies

    private static void SeedJustifies(ProjectDbContext context)
    {
        List<string> justifies =
        [
            "Producto Dañado",
            "Sin exitencia"
        ];
        foreach (var justify in justifies)
        {
            context.Justifications.Add(new Justification
            {
                Name = justify,
            });
        }
    }

    #endregion

    #region Products

    private static void SeedProducts(ProjectDbContext context, int total)
    {
        var serialInternal = context.BarCodes.Select(e => e.InternalCode);
        var serialExternal = context.BarCodes.Select(e => e.Code);
        var serials = serialInternal.Concat(serialExternal).Distinct().ToList();

        var wareHouses = context.WareHouses
            .Include(e => e.Aisles)
            .ToList();
        List<string> typeRack = ["A", "B"];

        for (var i = 0; i < total && serials.Count > 0; i++)
        {
            var code = serials.GetRandomElement();
            serials.Remove(code);

            var whareHouse = wareHouses.GetRandomElement();
            var aisle = whareHouse.Aisles.ToList().GetRandomElement();

            context.Products.Add(new Product
            {
                Code = code,
                Name = "CUSTOMER " + Guid.NewGuid().ToString()[..5],
                Detail = "CUSTOMER " + Guid.NewGuid().ToString()[..5],
                Location = $"{whareHouse.Code}-{aisle.Number}-{RandomNumberGenerator.GetInt32(aisle.TotalRack)}" +
                           $"-{typeRack.GetRandomElement()}-{RandomNumberGenerator.GetInt32(5)}",
            });
        }
    }

    #endregion

    #region Providers

    private static void SeedProviders(ProjectDbContext context, int total)
    {
        for (var i = 0; i < total; i++)
        {
            context.Providers.Add(new Provider
            {
                Code = "PVD" + Guid.NewGuid().ToString()[..(Provider.CodeLength - 3)],
                Name = "PROVIDER " + Guid.NewGuid().ToString()[..5],
            });
        }
    }

    #endregion

    #region WareHouses

    private static void SeedWareHouses(ProjectDbContext context, int total)
    {
        for (var i = 0; i < total; i++)
        {
            var warehouse = new WareHouse
            {
                Code = "WHS" + Guid.NewGuid().ToString()[..(WareHouse.CodeLength - 3)],
                Name = "WAREHOUSE " + Guid.NewGuid().ToString()[..5],
            };

            var aisles = RandomNumberGenerator.GetInt32(1,10);
            for (var j = 0; j < aisles; j++)
            {
                warehouse.Aisles.Add(new Aisle
                {
                    WareHouseCode = warehouse.Code,
                    Name = "AISLE " + Guid.NewGuid().ToString()[..5],
                    TotalRack = 5,
                    Number = j + 1,
                });
            }

            context.WareHouses.Add(warehouse);
        }
    }

    #endregion

    #region SaleOrders

    private static void SeedSaleOrders(ProjectDbContext context, int total)
    {
        var customers = context.Customers
            .Select(e => e.Code)
            .ToList();

        var products = context.Products
            .Select(e => e.Code)
            .ToList();

        for (var i = 0; i < total; i++)
        {
            var saleOrder = new SaleOrder
            {
                Id = i,
                CustomerCode = customers.GetRandomElement(),
                Status = OrderStatus.Pending,
            };

            var saleProducts = RandomNumberGenerator.GetInt32(10);
            for (var j = 0; j < saleProducts; j++)
            {
                var product = new SaleProduct
                {
                    SaleOrderId = saleOrder.Id,
                    ItemCode = products.GetRandomElement(),
                    Type = ProductType.Default,
                    AmountRequest = RandomNumberGenerator.GetInt32(20),
                };
                while (saleOrder.Products.Any(e=>e.ItemCode == product.ItemCode))
                {
                    product.ItemCode = products.GetRandomElement();
                }
                
                saleOrder.Products.Add(product);
            }

            context.SaleOrders.Add(saleOrder);
        }
    }

    #endregion
}