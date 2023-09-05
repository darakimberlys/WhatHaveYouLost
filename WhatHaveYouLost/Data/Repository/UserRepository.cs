using Microsoft.EntityFrameworkCore;
using WhatYouHaveLost.Data.Repository.Interfaces;

namespace WhatYouHaveLost.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> UserExistAsync(string userName)
    {
        var user = await _context.User.FirstOrDefaultAsync(u => u.LoginName == userName);
        return user is not null && string.Equals(user.LoginName, userName, StringComparison.InvariantCultureIgnoreCase);
    }
}