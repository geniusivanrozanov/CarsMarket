﻿namespace IdentityService.Application.Exceptions;

public class LoginFailedException : Exception
{
    public LoginFailedException(string message) : base(message)
    {
    }
}
