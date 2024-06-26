using EducationCore.Application.Business;
using EducationCore.Application.Contracts.Business;
using EducationCore.Application.Contracts.Configurations;
using EducationCore.Application.Contracts.Services;
using EducationCore.Application.Contracts.Utilities;
using EducationCore.Application.Services;
using EducationCore.Application.Utilities;
using EducationCore.Data.Entities;
using EducationCore.Data.Entity_Framework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Writers;
using Polly;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var assembly = typeof(Program).Assembly.GetName().Name;
var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<EducationDbContext>(options =>
    options.UseSqlServer(defaultConnectionString,
         b => b.MigrationsAssembly(assembly)));
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:7230";
        options.Audience = "educationcore";
    });
builder.Services.Configure<RemoteServiceConfig>(builder.Configuration.GetSection("RemoteServices"));

#region services

builder.Services.AddTransient<IUserBusiness, UserBusiness>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddScoped<IAPIUtil, APIUtil>();

#endregion services

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<EducationDbContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("corsapp");
app.UseAuthorization();

app.MapControllers();

app.Run();