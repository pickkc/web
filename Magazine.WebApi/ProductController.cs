using Magazine.Core.Models;
using Magazine.Core.Services;

namespace Magazine.WebApi
{
    public static class ProductController
    {
        public static void RegisterRoutes(RouteGroupBuilder group)
        {
            group.MapPost("/", (IProductService productService, Product product) =>
            {
                var addedProduct = productService.Add(product);
                return Results.Created($"/products/{addedProduct.Id}", addedProduct);
            });

            group.MapDelete("/{id:guid}", (IProductService productService, Guid id) =>
            {
                var removedProduct = productService.Remove(id);
                return removedProduct != null ? Results.Ok(removedProduct) : Results.NotFound();
            });

            group.MapPut("/{id:guid}", (IProductService productService, Guid id, Product updatedProduct) =>
            {
                if (id != updatedProduct.Id) return Results.BadRequest("ID в теле запроса не совпадает с URL");

                var editedProduct = productService.Edit(updatedProduct);
                return editedProduct != null ? Results.Ok(editedProduct) : Results.NotFound();
            });

            group.MapGet("/search", (IProductService productService, string name) =>
            {
                var foundProduct = productService.Search(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
                return foundProduct != null ? Results.Ok(foundProduct) : Results.NotFound();
            });
        }
    }
}
