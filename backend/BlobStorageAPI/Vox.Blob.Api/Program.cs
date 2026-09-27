using Microsoft.AspNetCore.Authorization;
using Vox.Blob.Domain;
using Vox.Blob.Domein.Interfaces;
using Vox.Blob.Persistence;
using Vox.Blob.Persistence.Interfaces;
using VoxAuth;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("voxdataApi", policy =>
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
                "admin")))
    .AddPolicy("m2m", policy =>
        policy.RequireClaim(
                "scope",
                 "VoxelGallery.Api.m2m"));
builder.Services.AddScoped<IAuthorizationHandler, VoxAuthHandler>();


builder.Services.Configure<VoxBlobOptions>(
    builder.Configuration.GetSection(nameof(VoxBlobOptions))
);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IVoxFileRepo,VoxFileRepo>();
builder.Services.AddScoped<IPreviewImageRepo,PreviewImageRepo>();
builder.Services.AddScoped<IVoxFileService,VoxFileService>();
builder.Services.AddScoped<IPreviewImageService,PreviewImageService>();

var app = builder.Build();

app.UseRouting(); 
app.UseCors("voxdataApi");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapOpenApi();              
app.MapScalarApiReference();  
app.Run();

