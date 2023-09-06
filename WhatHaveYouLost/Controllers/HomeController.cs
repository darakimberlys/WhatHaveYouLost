using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WhatYouHaveLost.Data.Repository.Configurations;
using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Model.Data;
using WhatYouHaveLost.Services.Interfaces;
using WhatYouHaveLost.Views.Models;

namespace WhatYouHaveLost.Controllers;

public class HomeController : Controller
{
    private readonly INewsRepository _newsRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly INewsService _newsService;
    private readonly IPasswordEncryptor _passwordEncryptor;
    private readonly IJwtTokenValidator _tokenValidator;
    private static string token = string.Empty;

    public HomeController(INewsRepository newsRepository,
        INewsService newsService,
        IAuthenticationService authenticationService,
        IPasswordEncryptor passwordEncryptor,
        IJwtTokenValidator tokenValidator)
    {
        _newsRepository = newsRepository;
        _newsService = newsService;
        _authenticationService = authenticationService;
        _passwordEncryptor = passwordEncryptor;
        _tokenValidator = tokenValidator;
        var test = token;
    }

    /// <summary>
    /// Página inicial
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult LoginPage()
    {
        return View(new LoginModel());
    }

    [HttpPost]
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
                token = result.Item2;
                return RedirectToAction("Manage");
            }else
            {
                ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                return View();
            }
        }
        
        return View(loginModel);
        
    }
    
    public IActionResult AddNews(string token)
    {
        var isValid = _tokenValidator.ValidateToken(token);

        if (isValid)
        {
            return View();
        }

        return RedirectToAction("LoginPage");    
    }

    public IActionResult Manage()
    {
        var isValid = _tokenValidator.ValidateToken(token);

        if (isValid)
        {
            var model = new ManageModel
            {
                NewsItems = _newsRepository.ReadAllNews(),
                Token = token
            };
            
            return View(model);
        }

        return RedirectToAction("LoginPage");
    }

    /// <summary>
    /// Visualizar todas as notícias existentes no banco de dados
    /// </summary>
    /// <returns></returns>
    public IActionResult News()
    {
        var newsModel = new NewsModel
        {
            NewsList = _newsRepository.ReadAllNews()
        };

        return View(newsModel);
    }
    
    public IActionResult UpdateNews(int id)
    {
        var isValid = _tokenValidator.ValidateToken(token);

        if (isValid)
        {
            _newsRepository.GetCompleteNewsByIdAsync(id);
            return View
            (
                new UpdateModel
            {
                Id = id
            });
        }

        return RedirectToAction("LoginPage");
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateNews(UpdateModel model)
    {
        if (ModelState.IsValid)
        {
            var isValid = _tokenValidator.ValidateToken(model.Token);
            
            if (isValid)
            { 
                await _newsService.UpdateNews(model);
                
                return RedirectToAction("Manage", new {token = model.Token});
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                return RedirectToAction("LoginPage");
            }
        }
        
        return View(model);
        
    }
    
    public IActionResult DeleteNews(int id)
    {
        var isValid = _tokenValidator.ValidateToken(token);

        if (isValid)
        {
            _newsService.DeleteNews(id);
            return RedirectToAction("Manage", new {token = token});
        }

        return RedirectToAction("LoginPage");
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