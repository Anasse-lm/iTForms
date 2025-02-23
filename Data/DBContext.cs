using iTFORMS.Models;
using Microsoft.EntityFrameworkCore;

namespace iTFORMS.Data;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) 
            : base(options) { }
    public DbSet<Template> Templates { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionOption> Options { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<TemplateTopic> TemplateTopics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Template>(entity =>
        {
            entity.Property(t => t.TemplateId)
                .ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.Property(q => q.QuestionId)
                .ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<QuestionOption>(entity =>
        {
            entity.Property(o => o.Id)
                .ValueGeneratedOnAdd();
        });
        
        modelBuilder.Entity<Template>()
            .HasMany(t => t.Questions)
            .WithOne(q => q.Template)
            .HasForeignKey(q => q.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Question>()
            .HasKey(q => q.QuestionId);

        modelBuilder.Entity<Question>()
            .HasMany(q => q.Options)
            .WithOne(o => o.Question)
            .HasForeignKey(o => o.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TemplateTopic>()
            .HasKey(tt => new { tt.TemplateId, tt.TopicId });

        modelBuilder.Entity<TemplateTopic>()
            .HasOne(tt => tt.Template)
            .WithMany(t => t.TemplateTopics)
            .HasForeignKey(tt => tt.TemplateId);

        modelBuilder.Entity<TemplateTopic>()
            .HasOne(tt => tt.Topic)
            .WithMany(t => t.TemplateTopics)
            .HasForeignKey(tt => tt.TopicId);
        }
}
