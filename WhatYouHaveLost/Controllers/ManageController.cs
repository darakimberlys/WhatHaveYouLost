using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WhatYouHaveLost.Data;
using WhatYouHaveLost.Data.Repository.Configurations;
using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Model.Data;
using WhatYouHaveLost.Services.Interfaces;
using WhatYouHaveLost.Views.Models;

namespace WhatYouHaveLost.Controllers;

[ApiController]
[Route("manager")]
public class ManageController : Controller
{
    private readonly INewsRepository _newsRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly INewsService _newsService;
    private readonly IPasswordEncryptor _passwordEncryptor;

    public ManageController(INewsRepository newsRepository,
        INewsService newsService,
        IAuthenticationService authenticationService,
        IPasswordEncryptor passwordEncryptor)
    {
        _newsRepository = newsRepository;
        _newsService = newsService;
        _authenticationService = authenticationService;
        _passwordEncryptor = passwordEncryptor;
    }

    /// <summary>
    /// Validação do formulário de login
    /// </summary>
    /// <param name="loginModel"></param>
    /// <returns></returns>
    [HttpGet("token")]
    public async Task<IActionResult> GenerateToken([FromBody] LoginModel loginModel)
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
                return Accepted(new TokenModel()
                {
                    BearerToken = result.Item2
                });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                return Forbid(result.Item2);
            }
        }

        return Forbid();
    }

    /// <summary>
    /// Valida o formulário de criação da nova notícia e salva no banco
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> CreateNews([FromBody] CreateNewsModel model)
    {
        if (ModelState.IsValid)
        {
            await _newsService.CreateNews(model);

            return Accepted();
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
            return Forbid();
        }
    }

    /// <summary>
    /// Visualizar todas as notícias existentes no banco de dados
    /// </summary>
    /// <returns></returns>
    [HttpGet("getNews")]
    public IActionResult ReadAllNews()
    {
        return Accepted(_newsRepository.ReadAllNews());
    }

    /// <summary>
    /// Validação e atualização da notícia
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateNews([FromRoute]UpsertModel model, int id)
    {
        if (ModelState.IsValid)
        {
            model.Id = id;
            await _newsService.UpdateNews(model);

            return Accepted();
        }

        return Forbid();
    }

    [Authorize]
    [HttpDelete("deleteById/{id}")]
    public IActionResult DeleteNews(int id)
    {
        _newsRepository.DeleteNews(id);
        return Accepted();
    }

    /// <summary>
    /// Ver detalhes da notícia clicada na página de todas as notícias
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [Route("getNews/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var result = await _newsRepository.GetCompleteNewsByIdAsync(id);

        if (result is null)
        { 
            return NotFound(result);
        }
        
        return Accepted(result);
    }
}