using Microsoft.EntityFrameworkCore;
using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Model.Data;

namespace WhatYouHaveLost.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<UserData> GetUserDataAsync(string userName)
    {
        var user = await _context.User.FirstOrDefaultAsync(u => u.LoginName == userName);
        return user;
    }
}