using System.Data;
using BUUME.Application.Abstractions.Data;
using Npgsql;

namespace BUUME.Infrastructure;

internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public IDbConnection GetOpenConnection()
    {
        NpgsqlConnection connection = dataSource.OpenConnection();

        return connection;
    }
}
