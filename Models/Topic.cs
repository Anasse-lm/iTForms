using System;
using System.ComponentModel.DataAnnotations;

namespace iTFORMS.Models;

public class Topic()
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    public List<TemplateTopic> TemplateTopics { get; set; } = [];
}

