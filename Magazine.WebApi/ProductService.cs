using System;
using System.Collections.Generic;
using System.Linq;
using Magazine.Core.Models;

namespace Magazine.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products = new();

        /// <summary>
        /// Добавляет новый товар в список.
        /// </summary>
        public Product Add(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            product.Id = Guid.NewGuid(); // Генерируем новый ID
            _products.Add(product);
            return product;
        }

        /// <summary>
        /// Удаляет товар по ID. Возвращает удаленный товар или null, если его не было.
        /// </summary>
        public Product? Remove(Guid id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
                return product;
            }
            return null;
        }

        /// <summary>
        /// Изменяет данные товара. Возвращает измененный товар или null, если товар не найден.
        /// </summary>
        public Product? Edit(Product updatedProduct)
        {
            var product = _products.FirstOrDefault(p => p.Id == updatedProduct.Id);
            if (product != null)
            {
                product.Name = updatedProduct.Name;
                product.Definition = updatedProduct.Definition;
                product.Price = updatedProduct.Price;
                product.Image = updatedProduct.Image;
                return product;
            }
            return null;
        }

        /// <summary>
        /// Ищет товар по переданному условию. Возвращает найденный товар или null.
        /// </summary>
        public Product? Search(Func<Product, bool> predicate)
        {
            return _products.FirstOrDefault(predicate);
        }
    }
}
