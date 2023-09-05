using System.ComponentModel.DataAnnotations;

namespace WhatYouHaveLost.Model.Data;

public class UserData
{
    public int UserId { get; }
    
    [Required(ErrorMessage = "O campo Usuário é obrigatório.")]
    [Display(Name = "Nome de Usuário")]
    public string LoginName { get; set; }
    
    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Password { get; set; }
    public Guid Salt { get; }
}