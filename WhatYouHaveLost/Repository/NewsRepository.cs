using WhatYouHaveLost.Repository.Data;

namespace WhatYouHaveLost.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly ApplicationDbContext _context;

        public NewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public NewsData GetNewsContent(string selectedNews)
        {
            return _context.NewsData.FirstOrDefault(news => news.Id == selectedNews);
        }

        public List<NewsData> GetAllNews()
        {
            return _context.NewsData.ToList();
        }
    }
}