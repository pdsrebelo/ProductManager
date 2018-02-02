using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ProductManager.Data.Entities;

namespace ProductManager.Data.Repositories
{
    internal class ProductSubCategoryRepository : RepositoryBase, IProductSubCategoryRepository
    {
        public ProductSubCategoryRepository(ProductManagerContext context)
          : base(context)
        {
        }

        public Task<ProductSubcategory> GetProductSubcategoryById(int productSubcategoryId, CancellationToken cancellationToken = default(CancellationToken))
        {
            string str = "\r\nselect ProductSubcategoryKey, ProductSubcategoryAlternateKey, ProductCategoryKey, EnglishProductSubcategoryName\r\nfrom DimProductSubcategory\r\nwhere ProductSubcategoryKey = @ProductSubcategoryId\r\n".Trim();
            Func<SqlDataReader, ProductSubcategory> mapper = MapProductCategory;
            string query = str;
            SqlParameter[] parameters = new SqlParameter[1];
            const int index = 0;
            SqlParameter sqlParameter = new SqlParameter("@ProductSubcategoryId", SqlDbType.Int)
            {
                Value = productSubcategoryId
            };
            parameters[index] = sqlParameter;
            CancellationToken cancellationToken1 = cancellationToken;
            return Get(mapper, query, parameters, cancellationToken1);
        }

        public Task<List<ProductSubcategory>> GetProductSubcategoriesByProductCategoryId(int productCategoryId, CancellationToken cancellationToken = default(CancellationToken))
        {
            string str = "\r\nselect ProductSubcategoryKey, ProductSubcategoryAlternateKey, ProductCategoryKey, EnglishProductSubcategoryName\r\nfrom DimProductSubcategory\r\nwhere ProductCategoryKey = @ProductCategoryId\r\n".Trim();
            Func<SqlDataReader, ProductSubcategory> mapper = MapProductCategory;
            string query = str;
            SqlParameter[] parameters = new SqlParameter[1];
            const int index = 0;
            SqlParameter sqlParameter = new SqlParameter("@ProductCategoryId", SqlDbType.Int)
            {
                Value = productCategoryId
            };
            parameters[index] = sqlParameter;
            CancellationToken cancellationToken1 = cancellationToken;
            return GetList(mapper, query, parameters, cancellationToken1);
        }

        public Task<List<ProductSubcategory>> GetProductSubcategories(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetList(MapProductCategory, "\r\nselect ProductSubcategoryKey, ProductSubcategoryAlternateKey, ProductCategoryKey, EnglishProductSubcategoryName\r\nfrom DimProductSubcategory\r\n".Trim(), null, cancellationToken);
        }

        private static ProductSubcategory MapProductCategory(SqlDataReader r)
        {
            return new ProductSubcategory
            {
                Id = r.GetInt32(0),
                Key = r.IsDBNull(1) ? new int?() : r.GetInt32(1),
                ProductCategoryId = r.IsDBNull(2) ? new int?() : r.GetInt32(2),
                Name = r.GetString(3)
            };
        }
    }
}