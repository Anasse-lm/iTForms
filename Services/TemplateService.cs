using iTFORMS.IServices;
using iTFORMS.Models;
using Microsoft.EntityFrameworkCore;
using iTFORMS.Data;

namespace iTFORMS.Services;

public class TemplateService(DBContext context) : ITemplateService
{
    private readonly DBContext _context = context;

    public Task<Template> GetTemplateByIdAsync(Guid templateId)
    {
        throw new NotImplementedException();
    }

    public async Task<Template> PublishTemplateAsync(Template template)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
    
        try
        {
            foreach (var question in template.Questions)
            {
                question.QuestionId = Guid.NewGuid();
                question.TemplateId = template.TemplateId; // Set foreign key
                
                foreach (var option in question.Options)
                {
                    option.Id = Guid.NewGuid();
                    option.QuestionId = question.QuestionId; // Set foreign key
                }
            }
            await _context.Templates.AddAsync(template);
            
            // Handle existing topics
            foreach (var templateTopic in template.TemplateTopics)
            {
                if (templateTopic.Topic?.Id != Guid.Empty)
                {
                    _context.Entry(templateTopic.Topic!).State = EntityState.Unchanged;
                }
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return template;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public Task<bool> ValidateTemplateAsync(Template template)
    {
        throw new NotImplementedException();
    }
}