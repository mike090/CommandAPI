using CommandAPI.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();

var connectionStringBuilder = new Npgsql.NpgsqlConnectionStringBuilder();
connectionStringBuilder.ConnectionString = builder.Configuration["ConnectionStrings:PostgresSqlConnection"];
connectionStringBuilder.Username = builder.Configuration["UserID"];
connectionStringBuilder.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<CommandContext>(opts => opts.UseNpgsql(
    connectionStringBuilder.ConnectionString
));

var app = builder.Build();

app.MapControllers();

app.Run();
