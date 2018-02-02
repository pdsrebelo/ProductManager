using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ProductManager.Data.Entities;

namespace ProductManager.Data.Repositories
{
    internal class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(ProductManagerContext context)
          : base(context)
        {
        }

        public Task<Product> GetProductById(int productId, CancellationToken cancellationToken = default(CancellationToken))
        {
            string str = "\r\nselect ProductKey, ProductAlternateKey, ProductSubcategoryKey, EnglishProductName, SafetyStockLevel, ListPrice\r\nfrom dbo.DimProduct\r\nwhere ProductKey = @ProductId".Trim();
            Func<SqlDataReader, Product> mapper = MapProduct;
            string query = str;
            SqlParameter[] parameters = new SqlParameter[1];

            const int index = 0;
            SqlParameter sqlParameter = new SqlParameter("@ProductId", SqlDbType.Int)
            {
                Value = productId
            };
            parameters[index] = sqlParameter;

            CancellationToken cancellationToken1 = cancellationToken;
            return Get(mapper, query, parameters, cancellationToken1);
        }

        public Task<List<Product>> GetProducts(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetList(MapProduct, "\r\nselect ProductKey, ProductAlternateKey, ProductSubcategoryKey, EnglishProductName, SafetyStockLevel, ListPrice\r\nfrom dbo.DimProduct\r\n".Trim(), null, cancellationToken);
        }

        public Task<List<Product>> GetProductsByProductSubCategoryId(int productSubCategoryId, CancellationToken cancellationToken = default(CancellationToken))
        {
            string str = "\r\nselect ProductKey, ProductAlternateKey, ProductSubcategoryKey, EnglishProductName, SafetyStockLevel, ListPrice\r\nfrom dbo.DimProduct\r\nwhere ProductSubcategoryKey = @ProductSubcategoryId".Trim();
            Func<SqlDataReader, Product> mapper = MapProduct;
            string query = str;
            SqlParameter[] parameters = new SqlParameter[1];

            const int index = 0;
            SqlParameter sqlParameter = new SqlParameter("@ProductSubcategoryId", SqlDbType.Int)
            {
                Value = productSubCategoryId
            };
            parameters[index] = sqlParameter;

            CancellationToken cancellationToken1 = cancellationToken;
            return GetList(mapper, query, parameters, cancellationToken1);
        }

        public Task<int> CreateProduct(Product product, CancellationToken cancellationToken = default(CancellationToken))
        {
            string query = "\r\ninsert into dbo.DimProduct(ProductAlternateKey, ProductSubcategoryKey, EnglishProductName, SafetyStockLevel, ListPrice, [SpanishProductName], [FrenchProductName], [FinishedGoodsFlag], [Color])\r\nvalues (@ProductKey, @ProductSubcategoryId, @Name, @StockLevel, @Price, '', '', 0, '')\r\n\r\nselect SCOPE_IDENTITY()".Trim();
            SqlParameter[] sqlParameterArray = new SqlParameter[5];

            const int index1 = 0;
            SqlParameter sqlParameter1 = new SqlParameter("@ProductKey", SqlDbType.VarChar, 25)
            {
                Value = product.Key,
                IsNullable = true
            };
            sqlParameterArray[index1] = sqlParameter1;

            const int index2 = 1;
            SqlParameter sqlParameter2 = new SqlParameter("@ProductSubcategoryId", SqlDbType.Int)
            {
                Value = product.ProductSubcategoryId,
                IsNullable = true
            };
            sqlParameterArray[index2] = sqlParameter2;

            const int index3 = 2;
            SqlParameter sqlParameter3 = new SqlParameter("@Name", SqlDbType.VarChar, 50)
            {
                Value = product.Name
            };
            sqlParameterArray[index3] = sqlParameter3;

            const int index4 = 3;
            SqlParameter sqlParameter4 = new SqlParameter("@StockLevel", SqlDbType.SmallInt)
            {
                Value = product.StockLevel,
                IsNullable = true
            };
            sqlParameterArray[index4] = sqlParameter4;

            const int index5 = 4;
            SqlParameter sqlParameter5 = new SqlParameter("@Price", SqlDbType.Money)
            {
                Value = product.Price,
                IsNullable = true
            };
            sqlParameterArray[index5] = sqlParameter5;

            SqlParameter[] parameters = sqlParameterArray;
            return ExecuteScalar<int>(query, parameters, cancellationToken);
        }

        public Task<int> UpdateProduct(Product product, CancellationToken cancellationToken = default(CancellationToken))
        {
            string query = "\r\nupdate dbo.DimProduct\r\nset ProductAlternateKey = @ProductKey,\r\n    ProductSubcategoryKey = @ProductSubcategoryId, \r\n    EnglishProductName = @Name,\r\n    SafetyStockLevel = @StockLevel, \r\n    ListPrice = @Price\r\nwhere ProductKey = @ProductId".Trim();
            SqlParameter[] sqlParameterArray = new SqlParameter[6];
            int index1 = 0;
            SqlParameter sqlParameter1 = new SqlParameter("@ProductId", SqlDbType.Int)
            {
                Value = product.Id
            };
            sqlParameterArray[index1] = sqlParameter1;

            const int index2 = 1;
            SqlParameter sqlParameter2 = new SqlParameter("@ProductKey", SqlDbType.VarChar, 25)
            {
                Value = product.Key,
                IsNullable = true
            };
            sqlParameterArray[index2] = sqlParameter2;

            const int index3 = 2;
            SqlParameter sqlParameter3 = new SqlParameter("@ProductSubcategoryId", SqlDbType.Int)
            {
                Value = product.ProductSubcategoryId,
                IsNullable = true
            };
            sqlParameterArray[index3] = sqlParameter3;

            const int index4 = 3;
            SqlParameter sqlParameter4 = new SqlParameter("@Name", SqlDbType.VarChar, 50)
            {
                Value = product.Name
            };
            sqlParameterArray[index4] = sqlParameter4;

            const int index5 = 4;
            SqlParameter sqlParameter5 = new SqlParameter("@StockLevel", SqlDbType.SmallInt)
            {
                Value = product.StockLevel,
                IsNullable = true
            };
            sqlParameterArray[index5] = sqlParameter5;

            const int index6 = 5;
            SqlParameter sqlParameter6 = new SqlParameter("@Price", SqlDbType.Money)
            {
                Value = product.Price,
                IsNullable = true
            };
            sqlParameterArray[index6] = sqlParameter6;

            SqlParameter[] parameters = sqlParameterArray;
            return ExecuteQuery(query, parameters, cancellationToken);
        }

        public Task<int> DeleteProduct(int productId, CancellationToken cancellationToken = default(CancellationToken))
        {
            Tuple<string, SqlParameter[]> tuple = DeleteProductQry(productId);
            return ExecuteQuery(tuple.Item1, tuple.Item2, cancellationToken);
        }

        [Obsolete("Use DeleteProduct instead")]
        public int DeleteProductSync(int productId)
        {
            Tuple<string, SqlParameter[]> tuple = DeleteProductQry(productId);
            using (SqlConnection sqlConnection = new SqlConnection(Context.ConnectionString))
            {
                using (SqlCommand command = sqlConnection.CreateCommand())
                {
                    command.CommandText = tuple.Item1;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddRange(tuple.Item2);
                    sqlConnection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        public Task<bool> IsValidProductKey(string productKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            string query = "\r\nselect count(1)\r\nfrom dbo.DimProduct\r\nwhere ProductAlternateKey = @ProductKey".Trim();
            SqlParameter[] parameters = new SqlParameter[1];

            const int index = 0;
            SqlParameter sqlParameter = new SqlParameter("@ProductKey", SqlDbType.VarChar, 25)
            {
                Value = productKey
            };
            parameters[index] = sqlParameter;

            CancellationToken cancellationToken1 = cancellationToken;
            return Get(r => r.GetInt32(0) == 0, query, parameters, cancellationToken1);
        }

        private static Product MapProduct(SqlDataReader r)
        {
            return new Product
            {
                Id = r.GetInt32(0),
                Key = r.GetString(1),
                ProductSubcategoryId = r.IsDBNull(2) ? new int?() : r.GetInt32(2),
                Name = r.GetString(3),
                StockLevel = r.IsDBNull(4) ? new short?() : r.GetInt16(4),
                Price = r.IsDBNull(5) ? new decimal?() : r.GetDecimal(5)
            };
        }

        private Tuple<string, SqlParameter[]> DeleteProductQry(int productId)
        {
            string str = "\r\ndelete sr\r\nfrom dbo.FactInternetSalesReason as sr\r\ninner join dbo.FactInternetSales as s on sr.[SalesOrderNumber] = s.[SalesOrderNumber] and sr.[SalesOrderLineNumber] = s.[SalesOrderLineNumber]\r\nwhere s.ProductKey = @ProductId\r\n\r\ndelete from dbo.FactInternetSales\r\nwhere ProductKey = @ProductId\r\n\r\ndelete from dbo.FactResellerSales\r\nwhere ProductKey = @ProductId\r\n\r\ndelete from dbo.DimProduct\r\nwhere ProductKey = @ProductId\r\n".Trim();
            SqlParameter[] sqlParameterArray1 = new SqlParameter[1];

            const int index = 0;
            SqlParameter sqlParameter = new SqlParameter("@ProductId", SqlDbType.Int)
            {
                Value = productId
            };
            sqlParameterArray1[index] = sqlParameter;

            SqlParameter[] sqlParameterArray2 = sqlParameterArray1;
            return Tuple.Create(str, sqlParameterArray2);
        }
    }
}