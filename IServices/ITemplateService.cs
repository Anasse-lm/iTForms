using iTFORMS.Models;


namespace iTFORMS.IServices;

public interface ITemplateService
{
    Task<Template> PublishTemplateAsync(Template template);
    Task<Template> GetTemplateByIdAsync(Guid templateId);
    Task<List<Template>> GetTemplatesByUserAsync(Guid userId);
}
