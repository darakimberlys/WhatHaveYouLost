using System.ComponentModel.DataAnnotations;

namespace WhatYouHaveLost.Model.Data;

public class UserData
{
    public int UserId { get; }
    public Guid ID { get; }
    public string LoginName { get; set; }
    public string Password { get; set; }
}