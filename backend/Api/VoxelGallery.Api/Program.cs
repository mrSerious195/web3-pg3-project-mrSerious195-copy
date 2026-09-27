using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Vox.IdentityServer.Domein.InterFaces;
using Vox.IdentityServer.Domein.Services;
using Vox.IdentityServer.Persistence;
using Vox.IdentityServer.Persistence.Interfaces;
using VoxAuth;
using VoxelGallery.Api;
using VoxelGallery.Domain.Interfaces;
using VoxelGallery.Domain.Services;
using VoxelGallery.Persistence;
using VoxelGallery.Persistence.DataModel;
using VoxelGallery.Persistence.Interfaces;
using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);





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

    builder.Services.AddHttpClient("Blob", client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration.GetValue<string>("HttpsBlob")!
    );
});

  builder.Services.AddHttpClient("IdentityServer", client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration.GetValue<string>("HttpsIdentityServer")!
    );
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
                "admin")))
    .AddPolicy("m2m", policy =>
        policy.RequireClaim("scope",
                 "VoxelGallery.Api.m2m"));
builder.Services.AddScoped<IAuthorizationHandler, VoxAuthHandler>();

builder.Services.AddAuthorization();
builder.Services.AddHttpClient();


builder.Services.AddDbContext<VoxDatabaseContext>();

builder.Services.Configure<DbOptions>(
     builder.Configuration.GetSection(
            nameof(DbOptions) 
        )
);
// TO DO ^^^^^^^^^^^^^  "Postgres"???
builder.Configuration.AddAzureKeyVault( 
    new Uri("https://vox-secrets.vault.azure.net/"), 
    new DefaultAzureCredential() 
);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IVoxelService,VoxelService>();
builder.Services.AddScoped<ISceneDataRepo, VoxelRepo>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IUserRepo,UserRepo>();
builder.Services.AddScoped<IFavoriteRepo,FavoriteRepo>();
builder.Services.AddScoped<IFavoriteService,FavoriteService>();
builder.Services.AddScoped<ILikeService,LikeService>();
builder.Services.AddScoped<ILikeRepo,LikeRepo>();
builder.Services.AddScoped<ICommentService,CommentService>();
builder.Services.AddScoped<ICommentRepo,CommentRepo>();
QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community; 
var app = builder.Build();


app.UseRouting(); 
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler(app.Environment.IsDevelopment() ? "/error-development" : "/error");
app.MapControllers();
app.MapOpenApi();              
app.MapScalarApiReference();   
app.Run();

