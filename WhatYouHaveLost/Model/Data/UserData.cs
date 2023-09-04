using System.ComponentModel.DataAnnotations;

namespace WhatYouHaveLost.Model.Data;

public class UserData
{
    public int UserId { get; set; }
    
    [Required]
    [Display(Name = "Nome de Usuário")]
    public string LoginName { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public BinaryData Password { get; set; }
    public object Salt { get; set; }
}