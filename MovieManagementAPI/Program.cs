using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieManagementAPI;
using MovieManagementAPI.DbContext;
using MovieManagementAPI.Services;
using Newtonsoft.Json.Converters; 

// Ensure this is the correct namespace for MovieDbContext // Ensure this is the correct namespace for DTOs

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//used for json patch document and for returning 406 ( not acceptable if client sends you unsupported format like xml , but here we have added AddXmlDataContractSerializerFormatters(); which will help to accept xml format as well 
builder.Services.AddControllers(options =>
{
   options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson(options =>
{
    // This tells Newtonsoft to serialize enums as strings
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
})
.AddXmlDataContractSerializerFormatters();

// Add DbContext with SQL Server provider

builder.Services.AddDbContext<MovieDbContext>(
     dbContextOptions => dbContextOptions.UseSqlite(
        builder.Configuration["ConnectionStrings:MovieInfoDBConnectionString"]
    ));

//builder.Services.AddSingleton<MovieDataStore>();

builder.Services.AddScoped<IMovieRepository, MovieRepository>();

builder.Services.AddScoped<IMovieDetailsRepository, MovieDetailsRepository>();

builder.Services.AddScoped<IMovieCastRepository, MovieCastRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["Authentication:SecretForKey"]))
        }; 
    }
);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustBeNikhil", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("name", "nikhil");
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();



app.MapControllers();

app.Run();

