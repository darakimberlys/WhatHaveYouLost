using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    private readonly INewsService _newsService;
    private readonly IPasswordEncryptor _passwordEncryptor;
    private readonly IJwtTokenValidator _tokenValidator;

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
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Direcionamento para a página de login
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    public IActionResult LoginPage()
    {
        return View(new LoginModel());
    }

    /// <summary>
    /// Validação do formulário de login
    /// </summary>
    /// <param name="loginModel"></param>
    /// <returns></returns>
    [AllowAnonymous]
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
                return RedirectToAction("Manage");
            }
            else
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
        return View(new CreateNewsModel());
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
            await _newsService.CreateNewsAsync(model);

            return RedirectToAction("Manage");
        }

        return RedirectToAction("CreateNews");
    }

    /// <summary>
    /// Direcionamento para a página de gerenciamento das notícias
    /// </summary>
    /// <returns></returns>
    public IActionResult Manage()
    {
        var model = new ManageModel
        {
            NewsItems = _newsRepository.ReadAllNews(),
        };

        return View(model);
    }

    /// <summary>
    /// Visualizar todas as notícias existentes no banco de dados
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    public IActionResult ReadAllNews()
    {
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
            await _newsService.UpdateNewsAsync(model);

            return RedirectToAction("Manage");
        }

        return RedirectToAction("UpdateNews");
    }

    public IActionResult DeleteNews(int id)
    {
        _newsRepository.DeleteNews(id);
        return RedirectToAction("Manage");
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

    public IActionResult SignOut()
    {
        return RedirectToAction("Index");
    }
}