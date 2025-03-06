using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTFORMS.Models;

public class QuestionOption
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    public string Text { get; set; } = string.Empty;
    public Guid QuestionId { get; set; }
        
    [ForeignKey("QuestionId")]
    public Question Question { get; set; }
    public bool IsCorrect { get; set; }
}