using System.ComponentModel.DataAnnotations.Schema;

namespace iTFORMS.Models;

public class Response
{
    public Guid Id { get; set; }
    public Guid TemplateId { get; set; }

    [ForeignKey("TemplateId")]
    public Template? Template { get; set; }
    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }

    public DateTime SubmittedAt { get; set; }
}
