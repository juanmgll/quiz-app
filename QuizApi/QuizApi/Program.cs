
using Microsoft.EntityFrameworkCore;
using QuizApi.Data;
using QuizApi.Data.Repositories;
using QuizApi.UseCase;

namespace QuizApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configura CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            builder.Services.AddDbContext<QuizDbContext>(sqlBuilder =>
            {
                sqlBuilder.UseSqlServer(builder.Configuration.GetConnectionString("Conection1"));
            });

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
            builder.Services.AddScoped<IAnswerRepository, AnswersRepository>();

            builder.Services.AddScoped<IUpdateCategoryUseCase, UpdateCategoryUseCase>();
            builder.Services.AddScoped<IUpdateQuestionUseState, UpdateQuestionUseCase>();
            builder.Services.AddScoped<IUpdateAnswerUseCase, UpdateAnswerUseCase>();
            builder.Services.AddScoped<IGetQuestionUseCase, GetQuestionUseCase>();

            var app = builder.Build();

            // Initialize database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<QuizDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al inicializar la base de datos: {ex.Message}");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAllOrigins");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
