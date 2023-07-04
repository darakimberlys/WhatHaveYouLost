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
        const string query = "SELECT * FROM NewsData WHERE Id = @Id";

        var listResult = _connection.QueryFirstOrDefault<NewsData>(query,
            new
            {
                Id = selectedNews
            });

        return listResult;
    }

    public List<NewsData> GetAllNews()
    {
        const string query = "SELECT * FROM NewsData";

        var listResult = _connection.Query<NewsData>(query).ToList();

        return listResult;
    }
}