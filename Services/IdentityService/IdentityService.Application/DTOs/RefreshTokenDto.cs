﻿namespace IdentityService.Application.DTOs;

public record RefreshTokenDto
{
    public required string RefreshToken { get; init; }
}
