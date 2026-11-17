using CustomerManagement.Api.Data;
using CustomerManagement.Api.Repositories;
using CustomerManagement.Api.Repositories.Interfaces;
using CustomerManagement.Api.Services;
using CustomerManagement.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using CustomerManagement.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

//Register controllers
builder.Services.AddControllers();

//Regiser EF Core with PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"));

});


builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

//Register OpenAPI
builder.Services.AddOpenApi();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
