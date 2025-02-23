using System;
using iTFORMS.Models;

namespace iTFORMS.IServices;

public interface ITopicService
{
    Task<Topic> CreateTopicAsync(Topic topic);
}
