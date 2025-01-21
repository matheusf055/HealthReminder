using System;
using System.Security.Claims;

namespace HealthReminder.Domain.Common
{
    public class UserToken : IUser
    {
        public UserToken(ClaimsPrincipal user)
        {
            Id = Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException("Usuário não autenticado."));
            Name = user.FindFirst(ClaimTypes.Name)?.Value ?? throw new UnauthorizedAccessException("Usuário não autenticado.");
            Email = user.FindFirst(ClaimTypes.Email)?.Value ?? throw new UnauthorizedAccessException("Usuário não autenticado.");
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
    }
}