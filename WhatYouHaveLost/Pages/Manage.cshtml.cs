using Microsoft.AspNetCore.Mvc.RazorPages;
using WhatYouHaveLost.Repository;
using WhatYouHaveLost.Repository.Data;


namespace WhatYouHaveLost.Pages
{
    public class ManageModel : PageModel
    {
        private readonly INewsRepository _newsRepository; 
        public IEnumerable<News> NewsItems { get; set; }

        public ManageModel(INewsRepository newsRepository) 
        {
            _newsRepository = newsRepository;
        }

        public async Task OnGetAsync()
        {
            NewsItems = await _newsRepository.ReadAllNewsAsync();
        }
    }
}