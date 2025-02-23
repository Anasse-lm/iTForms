using System.ComponentModel.DataAnnotations;
using iTFORMS.Models.Enums;

namespace iTFORMS.Models
{
    public class Template
    {
        [Key]
        public Guid TemplateId { get; set; } = Guid.NewGuid(); 

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public TemplateType TemplateType { get; set; }
        public AccessType AccessType { get; set; }
        public Guid TopicId {get; set;}

        
        [MaxLength(450)]
        public string AuthorId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [MaxLength(255)]
        public string ImageUrl { get; set; }
        
        [MaxLength(10)]
        public string Language { get; set; } = "en";
        public List<Question> Questions { get; set; } = [];
        public List<TemplateTopic> TemplateTopics { get; set; } = [];
        
    }
}