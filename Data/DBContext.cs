using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using iTFORMS.Models;

namespace iTFORMS.Data;

public class DBContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {}
    
    public DbSet<Template> Templates { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionOption> Options { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<TemplateTopic> TemplateTopics { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TemplateTag> TemplateTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(u => u.LockoutEnabled)
                .HasDefaultValue(false);
        });

        modelBuilder.Entity<Template>(entity =>
        {
            entity.Property(t => t.TemplateId)
                .ValueGeneratedNever();
            entity.HasIndex(t => t.CreatedAt);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.Property(q => q.QuestionId)
                .ValueGeneratedOnAdd();
            
            entity.Property(q => q.Order)
                .IsRequired()
                .HasDefaultValue(1);
        });

        modelBuilder.Entity<Template>()
            .HasMany(t => t.Questions)
            .WithOne(q => q.Template)
            .HasForeignKey(q => q.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Template>()
            .HasOne(t => t.User)
            .WithMany(u => u.Templates)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Question>()
            .HasMany(q => q.Options)
            .WithOne(o => o.Question)
            .HasForeignKey(o => o.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TemplateTopic>(entity =>
        {
            entity.HasKey(tt => new { tt.TemplateId, tt.TopicId });

            entity.HasOne(tt => tt.Template)
                .WithMany(t => t.TemplateTopics)
                .HasForeignKey(tt => tt.TemplateId);

            entity.HasOne(tt => tt.Topic)
                .WithMany(t => t.TemplateTopics)
                .HasForeignKey(tt => tt.TopicId);
        });

        modelBuilder.Entity<TemplateTag>(entity =>
        {
            entity.HasKey(tt => new { tt.TemplateId, tt.TagId });

            entity.HasOne(tt => tt.Template)
                .WithMany(t => t.TemplateTags)
                .HasForeignKey(tt => tt.TemplateId);

            entity.HasOne(tt => tt.Tag)
                .WithMany(t => t.TemplateTags)
                .HasForeignKey(tt => tt.TagId);
        });
    }
}
