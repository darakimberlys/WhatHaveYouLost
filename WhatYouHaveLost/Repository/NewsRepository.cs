using Dapper;
using Microsoft.Data.SqlClient;
using WhatYouHaveLost.Repository.Data;

namespace WhatYouHaveLost.Repository;

public class NewsRepository : INewsRepository
{
    private readonly SqlConnection _connection;

    public NewsRepository(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("NewsDataCS");
        _connection = new SqlConnection(connectionString);
    }
    
    public NewsData GetNewsContent(string selectedNews)
    {
        const string query = "SELECT * FROM NewsData WHERE NewsName = @NewsName";
        
        var listResult = _connection.QueryFirstOrDefault<NewsData>(query, 
            new
        {
            NewsName = selectedNews
        });

        return listResult;
    }
}