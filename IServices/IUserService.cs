using System;

namespace iTFORMS.IServices;

public interface IUserService
{
    Task<Guid> GetCurrentUserId();
}
