using System;

namespace HealthReminder.Domain.Common
{
    public interface IUser
    {
        Guid Id { get; }
        string Name { get; }
        string Email { get; }
    }
}