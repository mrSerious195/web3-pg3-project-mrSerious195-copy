using Microsoft.AspNetCore.Authorization;
using SiteNews.domain.Interfaces;
using SiteNews.Domain.Services;
using SiteNews.Persistence;
using SiteNews.Persistence.interfaces;
using VoxAuth;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(builder.Configuration.GetValue<string>("HttpsFrontend"))
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer",options => 
    {
        options.Authority = builder.Configuration.GetValue<string>("HttpsIdentityServer"); 
        options.TokenValidationParameters.ValidateAudience = false; 
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("User", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("scope",
                "VoxelGallery.Api");
          });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Admin", policy =>
        policy.Requirements.Add(
            new VoxIdentityRequirements( 
                "VoxelGallery.Api",
                "admin")));
builder.Services.AddScoped<IAuthorizationHandler, VoxAuthHandler>();
builder.Services.AddAuthorization();


builder.Services.Configure<NewsRepositoryOptions>(
    builder.Configuration.GetSection(
            nameof(NewsRepositoryOptions) 
        )
);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<INewsService,NewsService>();
builder.Services.AddScoped<INewsRepo, NewsRepo>();





var app = builder.Build();
app.UseRouting(); 
app.UseCors("AllowFrontend");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapOpenApi();              
app.MapScalarApiReference();   
app.Run();

