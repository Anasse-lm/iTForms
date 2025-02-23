using System;

namespace iTFORMS.Models;

public class TemplateTopic
{
    public Guid TemplateId { get; set; }
    public Template Template { get; set; }
    
    public Guid TopicId { get; set; }
    public Topic Topic { get; set; }
}
