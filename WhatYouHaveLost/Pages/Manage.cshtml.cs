using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhatYouHaveLost.Repository;

namespace WhatYouHaveLost.Pages;

[Authorize]
public class ManageModel : PageModel
{
    private readonly INewsRepository _newsRepository; 
    public IEnumerable<Repository.Data.News> NewsItems { get; set; }

    public ManageModel(INewsRepository newsRepository) 
    {
        _newsRepository = newsRepository;
    }

    public async Task OnGetAsync()
    {
        NewsItems = await _newsRepository.ReadAllNewsAsync();
    }
}