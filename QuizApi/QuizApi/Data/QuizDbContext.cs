using Microsoft.EntityFrameworkCore;
using QuizApi.Models;

namespace QuizApi.Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options) { }

        public DbSet<Category> Category { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Answers> Answer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().HasMany(q => q.Answers).WithOne().HasForeignKey(a => a.QuestionId);


            // DEFAULT DATA
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, CategoryName = "History" },
                new Category { Id = 2, CategoryName = "Geography" },
                new Category { Id = 3, CategoryName = "General culture" }
            );

            modelBuilder.Entity<Question>().HasData(
                new Question { Id = 1, CategoryId = 1, Title = "Who was the first President of the United States?" },
                new Question { Id = 2, CategoryId = 1, Title = "In which year did the Titanic sink?" },
                new Question { Id = 3, CategoryId = 2, Title = "What is the largest ocean on Earth?" },
                new Question { Id = 4, CategoryId = 2, Title = "Which mountain range separates Europe from Asia?" },
                new Question { Id = 5, CategoryId = 3, Title = "Which famous scientist developed the theory of relativity?" },
                new Question { Id = 6, CategoryId = 3, Title = "Who wrote the play Romeo and Juliet?" }
            );

            modelBuilder.Entity<Answers>().HasData(
                new Answers { Id = 1, AnswerText = "George Washington", IsCorrect = true, QuestionId = 1 },
                new Answers { Id = 2, AnswerText = "Thomas Jefferson", IsCorrect = false, QuestionId = 1 },
                new Answers { Id = 3, AnswerText = "Abraham Lincoln", IsCorrect = false, QuestionId = 1 },
                new Answers { Id = 4, AnswerText = "John Adams", IsCorrect = false, QuestionId = 1 },

                new Answers { Id = 5, AnswerText = "1912", IsCorrect = true, QuestionId = 2 },
                new Answers { Id = 6, AnswerText = "1905", IsCorrect = false, QuestionId = 2 },
                new Answers { Id = 7, AnswerText = "1915", IsCorrect = false, QuestionId = 2 },
                new Answers { Id = 8, AnswerText = "1920", IsCorrect = false, QuestionId = 2 },

                new Answers { Id = 9, AnswerText = "Pacific Ocean", IsCorrect = true, QuestionId = 3 },
                new Answers { Id = 10, AnswerText = "Atlantic Ocean", IsCorrect = false, QuestionId = 3 },
                new Answers { Id = 11, AnswerText = "Indian Ocean", IsCorrect = false, QuestionId = 3 },
                new Answers { Id = 12, AnswerText = "Arctic Ocean", IsCorrect = false, QuestionId = 3 },

                new Answers { Id = 13, AnswerText = "The Alps", IsCorrect = true, QuestionId = 4 },
                new Answers { Id = 14, AnswerText = "The Rockies", IsCorrect = false, QuestionId = 4 },
                new Answers { Id = 15, AnswerText = "The Andes", IsCorrect = false, QuestionId = 4 },
                new Answers { Id = 16, AnswerText = "The Himalayas", IsCorrect = false, QuestionId = 4 },

                new Answers { Id = 17, AnswerText = "Albert Einstein", IsCorrect = true, QuestionId = 5 },
                new Answers { Id = 18, AnswerText = "Isaac Newton", IsCorrect = false, QuestionId = 5 },
                new Answers { Id = 19, AnswerText = "Galileo Galilei", IsCorrect = false, QuestionId = 5 },
                new Answers { Id = 20, AnswerText = "Nikola Tesla", IsCorrect = false, QuestionId = 5 },

                new Answers { Id = 21, AnswerText = "William Shakespeare", IsCorrect = true, QuestionId = 6 },
                new Answers { Id = 22, AnswerText = "Charles Dickens", IsCorrect = false, QuestionId = 6 },
                new Answers { Id = 23, AnswerText = "Mark Twain", IsCorrect = false, QuestionId = 6 },
                new Answers { Id = 24, AnswerText = "Jane Austen", IsCorrect = false, QuestionId = 6 }
            );
        }
    }
}
