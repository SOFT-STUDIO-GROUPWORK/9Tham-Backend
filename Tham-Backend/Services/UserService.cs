﻿using System.Security.Claims;

namespace Tham_Backend.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetEmail()
    {
        var result = string.Empty;
        if (_httpContextAccessor.HttpContext != null)
            result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        return result;
    }
    
    public string GetRole()
    {
        var result = string.Empty;
        if (_httpContextAccessor.HttpContext != null)
            result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        return result;
    }
}