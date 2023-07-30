using IdentityServer;
using IdentityServer.Data;
using IdentityServer.Services.User;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = typeof(Program).Assembly.GetName().Name;
var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AspNetIdentityDbContext>(options =>
    options.UseSqlServer(defaultConnectionString,
         b => b.MigrationsAssembly(assembly)));
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AspNetIdentityDbContext>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Identity Server", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,
            },
            new List<string>()
          }
        });
});
builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(defaultConnectionString, opt => opt.MigrationsAssembly(assembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(defaultConnectionString, opt => opt.MigrationsAssembly(assembly));
    })
    .AddDeveloperSigningCredential();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();

#region Initialized Database

//using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope())
//{
//    serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

//    var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
//    context.Database.Migrate();
//    if (!context.Clients.Any())
//    {
//        foreach (var client in Config.GetClients())
//        {
//            context.Clients.Add(client.ToEntity());
//        }
//        context.SaveChanges();
//    }

//    if (!context.IdentityResources.Any())
//    {
//        foreach (var resource in Config.GetIdentityResources())
//        {
//            context.IdentityResources.Add(resource.ToEntity());
//        }
//        context.SaveChanges();
//    }

//    if (!context.ApiScopes.Any())
//    {
//        foreach (var resource in Config.ApiScopes.ToList())
//        {
//            context.ApiScopes.Add(resource.ToEntity());
//        }

//        context.SaveChanges();
//    }

//    if (!context.ApiResources.Any())
//    {
//        foreach (var resource in Config.GetApis())
//        {
//            context.ApiResources.Add(resource.ToEntity());
//        }
//        context.SaveChanges();
//    }
//}

#endregion Initialized Database

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
app.UseIdentityServer();
app.Run();