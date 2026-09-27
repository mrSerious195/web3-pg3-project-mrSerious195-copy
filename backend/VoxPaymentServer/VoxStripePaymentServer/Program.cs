using Stripe;
using VoxStripePaymentServer.models;
using Microsoft.AspNetCore.Authorization;
using VoxStripePaymentServer;
using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<StripeOptions>(builder.Configuration.GetSection(nameof(StripeOptions)));

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ChargeService>();
builder.Services.AddScoped<ProductService>();

builder.Configuration.AddAzureKeyVault( 
    new Uri("https://vox-secrets.vault.azure.net/"), 
    new DefaultAzureCredential() 
);


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


      builder.Services.AddHttpClient("VoxData", client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration.GetValue<string>("HttpsVoxData")!
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
          }).AddPolicy("m2m", policy =>
        policy.RequireClaim(
                "scope",
                 "VoxelGallery.Api.m2m"));
builder.Services.AddAuthorization();
builder.Services.AddHttpClient();

var app = builder.Build();
app.UseCors("AllowFrontend");


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapOpenApi();              
app.MapScalarApiReference();   
app.Run();
