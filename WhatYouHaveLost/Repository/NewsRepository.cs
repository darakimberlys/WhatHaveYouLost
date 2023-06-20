using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WhatYouHaveLost.Repository.Data;

namespace WhatYouHaveLost.Repository;

public class NewsRepository : INewsRepository
{
    private readonly SqlConnection _connection;

    public NewsRepository(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        _connection = new SqlConnection(connectionString);
    }
    
    public async Task<NewsData> GetNewsContent(string selectedNews)
    {
        const string query = "SELECT * FROM NewsTable WHERE NewsName = @Selected";
        
        var listResult = await _connection.QueryAsync<NewsData>(query, new
        {
            Selected = selectedNews
        });

        return listResult.FirstOrDefault();
    }
}