using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WhatYouHaveLost.Data.Repository.Configurations;
using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Model.Data;
using WhatYouHaveLost.Services.Interfaces;
using WhatYouHaveLost.Views.Models;

namespace WhatYouHaveLost.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly INewsRepository _newsRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IPasswordEncryptor _passwordEncryptor;

    public HomeController(INewsRepository newsRepository,
        IAuthenticationService authenticationService,
        IPasswordEncryptor passwordEncryptor)
    {
        _newsRepository = newsRepository;
        _authenticationService = authenticationService;
        _passwordEncryptor = passwordEncryptor;
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
            var encriptedPassword = _passwordEncryptor.EncryptPassword(loginModel.Password);
            var result = await _authenticationService.LoginAsync(
                new UserData
                {
                LoginName = loginModel.UserName,
                Password = encriptedPassword
                });

            if (result.Item1)
            {
                Request.Headers.Authorization = new StringValues(result.Item2);
                
                return RedirectToAction("Manage");
            }else
            {
                ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                return View();
            }
        }
        
        return View(loginModel);
        
    }
    
    public IActionResult AddNews()
    {
        return View();
    }

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