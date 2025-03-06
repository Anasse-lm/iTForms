using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTFORMS.Models;

public class Answer
{
    public Guid Id { get; set; }
    public Guid ResponseId { get; set; }

    [ForeignKey("ResponseId")]
    public Response? Response { get; set; }
    public Guid QuestionId { get; set; }
    
    [ForeignKey("QuestionId")]
    public Question? Question { get; set; }
    public Guid OptionId { get; set; }

    [MaxLength(2000)]
    public string? TextAnswer { get; set; }
}
