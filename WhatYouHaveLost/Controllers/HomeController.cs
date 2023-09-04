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
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(UserData userData)
        {
            if (ModelState.IsValid)
            {
               var signInResult = await _authenticationService.LoginAsync(userData.LoginName, userData.Password);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Manage");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login inválido. Verifique seu nome de usuário e senha.");
                }
            }
            return View(userData);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.SignOutAsync();
            return RedirectToAction("Index", "Home");
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
        public IActionResult News()
        {
            var newsModel = new NewsModel(_newsRepository)
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

