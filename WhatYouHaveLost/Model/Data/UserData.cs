using Microsoft.AspNetCore.Identity;

namespace WhatYouHaveLost.Model.Data;

public class UserData : IdentityUser
{
    public int UserId { get; }
    public Guid ID { get; }
    public string LoginName { get; set; }
    public string Password { get; set; }
}