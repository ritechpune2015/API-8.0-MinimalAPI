using Microsoft.EntityFrameworkCore;
using MinimalAPI.Models;
using System;

namespace MinimalAPI.EndPoints
{
    public static class ProductsEndpoints
    {
        public static void MapProductsEndpoints(this IEndpointRouteBuilder routes)

        {
            routes.MapGet("/products", async (CompanyContext cc) =>
            {
                return await cc.Products.ToListAsync();
            });

            routes.MapPost("/products", async (CompanyContext cc, Product product) =>
            {
                cc.Products.Add(product);
                await cc.SaveChangesAsync();
                return
                Results.Created($"/products/{product.ProductID}",
                product);
            });


            routes.MapPut("/products/{id}", async (int id,
            Product updatedProduct, CompanyContext db) =>
            {
                var product = await
    db.Products.FindAsync(id);
                if (product is null) return Results.NotFound();

                product.ProductName = updatedProduct.ProductName;
                product.Price = updatedProduct.Price;
                product.MfgName = updatedProduct.MfgName;
                await db.SaveChangesAsync(); return
                Results.Ok(product);
            });


            routes.MapDelete("/products/{id}", async (int id, CompanyContext cntx) =>
            {
                if (id == 0)
                    return Results.BadRequest();

                var rec = await cntx.Products.FindAsync(id);

                if (rec == null)
                    return Results.NotFound();
                cntx.Remove(rec);
                await cntx.SaveChangesAsync();
                return Results.NoContent();

            });
        }
    }
}
