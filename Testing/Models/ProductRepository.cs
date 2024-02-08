using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Testing.Models
{
	public class ProductRepository : IProductRepository
	{
        private readonly IDbConnection _conn;

		public ProductRepository(IDbConnection conn)
		{
            _conn = conn; 
		}

        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("SELECT * FROM PRODUCTS;");
        }

        public Product GetProduct(int id)
        {
            return _conn.QuerySingle<Product>("SELECT * FROM PRODUCTS WHERE PRODUCTID = @id",
                new { id = id});
        }

        public void UpdateProduct(Product product)
        {
             _conn.Execute("UPDATE PRODUCTS SET NAME = @name, PRICE = @price WHERE PRODUCTID = @id",
              new { name = product.Name, price = product.Price, id = product.ProductID });
        }

        public IEnumerable<Category> GetCategories()
        {
            return _conn.Query<Category>("SELECT * FROM CATEGORIES");
        }

        public void InsertProduct(Product productToInsert)
        {
            _conn.Execute("INSERT INTO PRODUCTS(NAME, PRICE, CATEGORYID) VALUES(@name, @price, @categoryid);",
                new { name = productToInsert.Name, price = productToInsert.Price, categoryid = productToInsert.CategoryID });
        }

        public Product AssignCategory()
        {
            var categoryList = GetCategories();
            var product = new Product();
            product.Categories = categoryList;
            return product;
        }
    }
}

