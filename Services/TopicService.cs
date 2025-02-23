using iTFORMS.Data;
using iTFORMS.IServices;
using iTFORMS.Models;
using Microsoft.EntityFrameworkCore;


namespace iTFORMS.Services;

public class TopicService(DBContext context): ITopicService
{
    private readonly DBContext _context = context;

    public async Task<Topic> CreateTopicAsync(Topic topic)
    {
        await _context.Topics.AddAsync(topic);
        await _context.SaveChangesAsync();
        return topic;
    }

     public async Task<IEnumerable<Topic>> GetAllTopicsAsync()
        {
            return await _context.Topics
                .OrderBy(t => t.Name)
                .ToListAsync();
        }
}
