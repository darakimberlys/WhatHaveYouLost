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
    }

    /// <summary>
    /// Página inicial
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        ViewBag.Token = token;
       
        return View();
    }

    /// <summary>
    /// Direcionamento para a página de login
    /// </summary>
    /// <returns></returns>
    public IActionResult LoginPage()
    {
        ViewBag.Token = token;

        return View(new LoginModel());
    }

    /// <summary>
    /// Validação do formulário de login
    /// </summary>
    /// <param name="loginModel"></param>
    /// <returns></returns>
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
        
        return RedirectToAction("LoginPage");
    }
    
    /// <summary>
    /// Direciona para a página do formulário de criação da nova notícia
    /// </summary>
    /// <returns></returns>
    public IActionResult CreateNews()
    {
        var isValid = _tokenValidator.ValidateToken(token);

        if (isValid)
        {
            ViewBag.Token = token;
            
            return View(new CreateNewsModel());
        }

        return RedirectToAction("LoginPage");    
    }

    /// <summary>
    /// Valida o formulário de criação da nova notícia e salva no banco
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateNews(CreateNewsModel model)
    {
        if (ModelState.IsValid)
        {
            var isValid = _tokenValidator.ValidateToken(token);
            
            if (isValid)
            { 
                _newsService.CreateNews(model);
                
                return RedirectToAction("Manage");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                return RedirectToAction("LoginPage");
            }
        }
        
        return RedirectToAction("CreateNews");
    }
    
    /// <summary>
    /// Direcionamento para a página de gerenciamento das notícias
    /// </summary>
    /// <returns></returns>
    public IActionResult Manage()
    {
        var isValid = _tokenValidator.ValidateToken(token);

        if (isValid)
        {
            ViewBag.Token = token;

            var model = new ManageModel
            {
                NewsItems = _newsRepository.ReadAllNews(),
            };
            
            return View(model);
        }

        return RedirectToAction("LoginPage");
    }

    /// <summary>
    /// Visualizar todas as notícias existentes no banco de dados
    /// </summary>
    /// <returns></returns>
    public IActionResult ReadAllNews()
    {
        ViewBag.Token = token;

        return View(new NewsModel
            {
                NewsList = _newsRepository.ReadAllNews()
            });
    }
    
    /// <summary>
    /// Direcionamento para a página de atuzalização das notícias existentes
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> UpdateNews(int id)
    {
        var isValid = _tokenValidator.ValidateToken(token);

        if (isValid)
        {
            ViewBag.Token = token;

            var news = await _newsRepository.GetCompleteNewsByIdAsync(id);
            return View
            (
                new UpsertModel
            {
                Id = id,
                Title = news.Title,
                Content = news.Content,
                ImageLink = news.Image,
                AuthorLink = news.Author,
            });
        }

        return RedirectToAction("LoginPage");
    }
    
    /// <summary>
    /// Validação e atualização da notícia
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> UpdateNews(UpsertModel model)
    {
        if (ModelState.IsValid)
        {
            ViewBag.Token = token;

            var isValid = _tokenValidator.ValidateToken(token);
            
            if (isValid)
            { 
                await _newsService.UpdateNews(model);
                
                return RedirectToAction("Manage");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                return RedirectToAction("LoginPage");
            }
        }
        
        return RedirectToAction("UpdateNews");
    }
    
    public IActionResult DeleteNews(int id)
    {
        var isValid = _tokenValidator.ValidateToken(token);

        if (isValid)
        {
            _newsRepository.DeleteNews(id);
            return RedirectToAction("Manage");
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
        ViewBag.Token = token;

        var detailsModel = new DetailsModel
        {
            News = await _newsRepository.GetCompleteNewsByIdAsync(id)
        };

        return View(detailsModel);
    }
    
    public IActionResult SignOut()
    {
        token = string.Empty;

        return RedirectToAction("Index");    
    }
}