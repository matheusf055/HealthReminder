using System;

namespace HealthReminder.Domain.Common.Auditable
{
    public interface IUpdateAuditable
    {
        Guid? UpdateUserId { get; set; }
        string UpdateUser { get; set; }
        DateTime? UpdateDate { get; set; }
    }
}
