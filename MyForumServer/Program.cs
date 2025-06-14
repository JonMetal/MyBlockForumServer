using MyBlockForumServer.Auth;
using MyBlockForumServer.DataBase.Entities;
using MyBlockForumServer.DataBase.Repositories;
using MyBlockForumServer.DataBase.Services;
using MyBlockForumServer.Extensions;
using Thread = MyBlockForumServer.DataBase.Entities.Thread;

namespace MyBlockForumServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.Services.AddScoped<IRepository<Thread>, ThreadRepository>();
            builder.Services.AddScoped<IRepository<ThreadTheme>, ThreadThemeRepository>();
            builder.Services.AddScoped<IThreadService, ThreadService>();

            builder.Services.AddScoped<IRepository<Message>, MessageRepository>();
            builder.Services.AddScoped<IMessageService, MessageService>();

            builder.Services.AddScoped<IRepository<Role>, RoleRepository>();
            builder.Services.AddScoped<IRepository<User>, UserRepository>();
            builder.Services.AddScoped<IRepository<Status>, StatusRepository>();
            builder.Services.AddScoped<IRepository<UserThread>, UserThreadRepository>();
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

            builder.Services.AddControllers(
                options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApiAAuthentication(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(l =>
            l.WithOrigins("http://localhost:5173")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod()
            );

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
