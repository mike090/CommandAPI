using AutoMapper;
using CommandAPI.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(s =>
{
    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
