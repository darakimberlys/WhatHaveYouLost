using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Model.Data;
using WhatYouHaveLost.Services.Interfaces;
using WhatYouHaveLost.Views.Models;

namespace WhatYouHaveLost.Controllers;

public class HomeController : Controller
{
    private readonly INewsRepository _newsRepository;
    private readonly IAuthenticationService _authenticationService;

    public HomeController(INewsRepository newsRepository,
        IAuthenticationService authenticationService)
    {
        _newsRepository = newsRepository;
        _authenticationService = authenticationService;
    }

    /// <summary>
    /// Página inicial
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    [AllowAnonymous]
    public IActionResult LoginPage()
    {
        return View(new LoginModel());
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> LoginPage(LoginModel loginModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _authenticationService.LoginAsync(
                new UserData
                {
                LoginName = loginModel.UserName,
                Password = loginModel.Password 
                });

            if (result)
            {
                return RedirectToAction("Manage");
            }
            {
                ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                return View();
            }
        }

        return View(loginModel);
    }
    
    [Authorize]
    public IActionResult AddNews()
    {
        return View();
    }

    [Authorize]
    public IActionResult Manage()
    {
        return View();
    }

    /// <summary>
    /// Visualizar todas as notícias existentes no banco de dados
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    public IActionResult News()
    {
        var newsModel = new NewsModel
        {
            NewsList = _newsRepository.ReadAllNews()
        };

        return View(newsModel);
    }

    /// <summary>
    /// Ver detalhes da notícia clicada na página de todas as notícias
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [Route("Home/Details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var detailsModel = new DetailsModel
        {
            News = await _newsRepository.GetCompleteNewsByIdAsync(id)
        };

        return View(detailsModel);
    }
}