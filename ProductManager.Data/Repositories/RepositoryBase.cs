using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManager.Data.Repositories
{
    public abstract class RepositoryBase
    {
        protected RepositoryBase(ProductManagerContext context)
        {
            Context = context;
        }

        protected ProductManagerContext Context { get; }

        protected Task<List<T>> GetList<T>(Func<SqlDataReader, T> mapper, string query, SqlParameter[] parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return ExecuteReader(async r =>
            {
                List<T> result = new List<T>();
                while (true)
                {
                    bool flag = await r.ReadAsync(cancellationToken);
                    if (flag)
                        result.Add(mapper(r));
                    else
                        break;
                }
                return result;
            }, query, parameters, cancellationToken);
        }

        protected Task<T> Get<T>(Func<SqlDataReader, T> mapper, string query, SqlParameter[] parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return ExecuteReader(async r =>
            {
                T result = default(T);
                bool flag = await r.ReadAsync(cancellationToken);
                if (flag)
                    result = mapper(r);
                return result;
            }, query, parameters, cancellationToken);
        }

        protected Task<int> ExecuteQuery(string query, SqlParameter[] parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetCommand(command => command.ExecuteNonQueryAsync(cancellationToken), query, parameters, cancellationToken);
        }

        protected Task<T> ExecuteScalar<T>(string query, SqlParameter[] parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetCommand(async command =>
            {
                object obj = await command.ExecuteScalarAsync(cancellationToken);
                object result = obj;
                return (T)Convert.ChangeType(result, typeof(T));
            }, query, parameters, cancellationToken);
        }

        private Task<T> ExecuteReader<T>(Func<SqlDataReader, Task<T>> mapper, string query, SqlParameter[] parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetCommand(async command =>
            {
                SqlDataReader sqlDataReader = await command.ExecuteReaderAsync(cancellationToken);
                SqlDataReader reader = sqlDataReader;
                T obj1;
                try
                {
                    T obj = await mapper(reader);
                    obj1 = obj;
                }
                finally
                {
                    reader?.Dispose();
                }
                return obj1;
            }, query, parameters, cancellationToken);
        }

        private async Task<T> GetCommand<T>(Func<SqlCommand, Task<T>> worker, string query, SqlParameter[] parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            SqlConnection connection = new SqlConnection(Context.ConnectionString);
            T obj1;
            try
            {
                var command = connection.CreateCommand();
                try
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);
                    await connection.OpenAsync(cancellationToken);
                    T obj = await worker(command);
                    obj1 = obj;
                }
                finally
                {
                    command.Dispose();
                }
            }
            finally
            {
                connection.Dispose();
            }
            return obj1;
        }
    }
}