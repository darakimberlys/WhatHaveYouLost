using Dapper;
using Microsoft.Data.SqlClient;

namespace WhatYouHaveLost.Repository;

public class NewsRepository : INewsRepository
{
    private readonly SqlConnection _connection;

    public NewsRepository(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        _connection = new SqlConnection(connectionString);
    }
    
    public async Task<IEnumerable<NewsData>> GetNewsContent(string selectedNews)
    {
        const string query = "SELECT * FROM Exemplos";
        return await _connection.QueryAsync<NewsData>(query);
    }
}