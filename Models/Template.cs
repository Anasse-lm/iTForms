using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iTFORMS.Models.Enums;

namespace iTFORMS.Models
{
    public class Template
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid TemplateId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        
        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public TemplateType TemplateType { get; set; }
        public AccessType AccessType { get; set; }
        public Guid TopicId {get; set;}

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User {get; set;}

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [MaxLength(255)]
        public string? ImageUrl { get; set; }
        
        [MaxLength(10)]
        public string Language { get; set; } = "en";
        public List<Question> Questions { get; set; } = [];
        public List<TemplateTopic> TemplateTopics { get; set; } = [];
        public List<TemplateTag> TemplateTags {get; set;} = [];
    }
}