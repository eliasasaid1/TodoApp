using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace TodoApp.Data;

public class ToDoAppContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public ToDoAppContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("MainConnection");
    }
    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}
