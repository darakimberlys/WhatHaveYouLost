using System.ComponentModel.DataAnnotations;

namespace WhatYouHaveLost.Views.Models;

public class CreateNewsModel
{

    [Required(ErrorMessage = "O campo Título é obrigatório.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "O campo Conteúdo é obrigatório.")]
    public string Content { get; set; }

    [Required(ErrorMessage = "O campo URL da Imagem é obrigatório.")]
    public string ImageLink { get; set; }

    [Required(ErrorMessage = "O campo URL Fonte é obrigatório.")]
    public string AuthorLink { get; set; }
}