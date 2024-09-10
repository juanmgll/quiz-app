using Microsoft.EntityFrameworkCore;

namespace QuizApi.Data
{
    public class DbInitializer
    {
        public static void Initialize(QuizDbContext context)
        {
            // esure database exist
            context.Database.Migrate();
        }
    }
}
