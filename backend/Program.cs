using CustomerManagement.Api.Data;
using CustomerManagement.Api.Repositories;
using CustomerManagement.Api.Repositories.Interfaces;
using CustomerManagement.Api.Services;
using CustomerManagement.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using CustomerManagement.Api.Middleware;
using CustomerManagement.Api.Auth;
using CustomerManagement.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Register controllers
builder.Services.AddControllers();

//Regiser EF Core with PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"));

});


builder.Services.AddSingleton<DemoUserStore>();


builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

//Register OpenAPI with JWT
builder.Services.AddOpenApiWithJwt();

//Authentication & Authorization
builder.Services.AddJwtAuthentication(builder.Configuration);

//CORS
builder.Services.AddFrontendCors(builder.Configuration);

var app = builder.Build();

//Cath unhandled exceptions from later middleware/endpoints
app.UseExceptionHandler();

//Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/openapi/v1.json",
            "Customer Management API v1");
    });
}


app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(CorsExtensions.FrontendCorsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
