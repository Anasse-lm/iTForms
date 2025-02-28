using System;
using System.ComponentModel.DataAnnotations;

namespace iTFORMS.Models;

public class Tag
{
    [Key]
    public Guid TagId { get; set; } = Guid.NewGuid();

    [Required]
    public string? Content { get; set; }
    public List<TemplateTag> TemplateTags {get; set;} = [];
}