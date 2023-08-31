using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WhatYouHaveLost.Pages;

public class LoginModel : PageModel
{
    public async Task<IActionResult> OnPostAsync(string username, string password)
    {
        if (username == "usuarioteste" && password == "senhateste")
        {
            var claims = new[]
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, username)
            };

            var identity = new System.Security.Claims.ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new System.Security.Claims.ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToPage("/manage");
        }

        ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
        return Page();
    }
}