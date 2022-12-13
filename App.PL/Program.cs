using App.BLL;
using App.DAL.Contexts;
using App.DAL.Repositories;
using App.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<NorthwindContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Northwind"));
});

builder.Services.AddAutoMapper(typeof(EmployeeMapProfile).Assembly);
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseExceptionHandler(configure =>
{
    configure.Run(async context =>
    {
        context.Response.ContentType = "text/plain";
        context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
        var ex = context.Features.Get<IExceptionHandlerPathFeature>();
        await context.Response.WriteAsync(ex?.Error?.Message ?? "Internal Server Error.");
    });
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
