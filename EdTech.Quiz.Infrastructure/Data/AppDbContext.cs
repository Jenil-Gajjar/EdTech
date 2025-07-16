using EdTech.Quiz.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EdTech.Quiz.Infrastructure.Data;
using Quiz = Domain.Entities.Quiz;
public class AppDbContext : DbContext
{
    public AppDbContext()
    {

    }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Quiz> Quizzes => Set<Quiz>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Option> Options => Set<Option>();
    public DbSet<QuizQuestion> QuizQuestions => Set<QuizQuestion>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserQuizAttempt> UserQuizAttempts => Set<UserQuizAttempt>();
    public DbSet<UserAnswer> UserAnswers => Set<UserAnswer>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<QuizQuestion>(entity =>
        {
            entity.HasKey(qq => new { qq.QuestionId, qq.QuizId });
            entity.HasOne(qq => qq.Quiz).WithMany(qq => qq.QuizQuestions).HasForeignKey(qq => qq.QuizId);
            entity.HasOne(qq => qq.Question).WithMany(qq => qq.QuizQuestions).HasForeignKey(qq => qq.QuestionId);
        });

        modelBuilder.Entity<UserAnswer>()
            .HasOne(u => u.Attempt)
            .WithMany(u => u.Answers)
            .HasForeignKey(u => u.UserQuizAttemptId);
    }

}
