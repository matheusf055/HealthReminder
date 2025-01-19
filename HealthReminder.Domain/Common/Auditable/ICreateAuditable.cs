using System;

namespace HealthReminder.Domain.Common.Auditable
{
    public interface ICreateAuditable
    {
        Guid CreateUserId { get; set; }
        string CreateUser { get; set; }
        DateTime CreateDate { get; set; }
    }
}
