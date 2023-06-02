using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Context;

public class DapperContext
{
    private readonly IConfiguration configuration;

    public DapperContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(configuration.GetConnectionString("Default"));
    }
}
