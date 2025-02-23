using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iTFORMS.Models.Enums;

namespace iTFORMS.Models;

public class Question
{
    [Key]
    public Guid QuestionId {get; set;} = Guid.NewGuid();
    public string Title {get; set;}
    public Guid TemplateId { get; set; }
    public QuestionType QuestionType;
    public bool Displayed {get; set;}
    
    [ForeignKey("TemplateId")]
    public Template Template { get; set; }
    
    public List<QuestionOption> Options { get; set; } = [];
}
