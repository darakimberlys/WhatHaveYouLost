using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Model.Data;

namespace WhatYouHaveLost.Views.Models;

[Authorize]
public class ManageModel : PageModel
{
    private readonly INewsRepository _newsRepository; 
    public IEnumerable<News> NewsItems { get; set; }

    public ManageModel(INewsRepository newsRepository) 
    {
        _newsRepository = newsRepository;
    }

    public void OnGetAsync()
    {
        NewsItems =  _newsRepository.ReadAllNews();
    }
}