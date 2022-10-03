using CommandAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();

builder.Services.AddDbContext<CommandContext>(opts => opts.UseNpgsql(
    builder.Configuration["ConnectionStrings:PostgresSqlConnection"]
));

var app = builder.Build();

app.MapControllers();

app.Run();
