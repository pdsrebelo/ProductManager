using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ProductManager.Data.Entities;

namespace ProductManager.Data.Repositories
{
    internal class ProductCategoryRepository : RepositoryBase, IProductCategoryRepository
    {
        public ProductCategoryRepository(ProductManagerContext context)
            : base(context)
        {
        }

        public Task<ProductCategory> GetProductCategoryById(int productId, CancellationToken cancellationToken = default(CancellationToken))
        {
            string str = "\r\nselect ProductCategoryKey, ProductCategoryAlternateKey, EnglishProductCategoryName\r\nfrom DimProductCategory\r\nwhere ProductCategoryKey = @ProductId\r\n".Trim();
            Func<SqlDataReader, ProductCategory> mapper = MapProductCategory;
            string query = str;
            SqlParameter[] parameters = new SqlParameter[1];
            int index = 0;
            SqlParameter sqlParameter = new SqlParameter("@ProductId", SqlDbType.Int)
            {
                Value = productId
            };
            parameters[index] = sqlParameter;
            CancellationToken cancellationToken1 = cancellationToken;
            return Get(mapper, query, parameters, cancellationToken1);
        }

        public Task<List<ProductCategory>> GetProductCategories(CancellationToken cancellationToken = default(CancellationToken))
        {
            string query =
                "\r\nselect ProductCategoryKey, ProductCategoryAlternateKey, EnglishProductCategoryName\r\nfrom DimProductCategory\r\n".Trim();
            return GetList(MapProductCategory, query, null, cancellationToken);
        }

        private static ProductCategory MapProductCategory(SqlDataReader r)
        {
            return new ProductCategory
            {
                Id = r.GetInt32(0),
                Key = r.IsDBNull(1) ? new int?() : r.GetInt32(1),
                Name = r.GetString(2)
            };
        }
    }
}