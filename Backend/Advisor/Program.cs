using Api.Config;
using Application.Advisors;
using Domain.Data;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.OData;
using Microsoft.Azure.Management.Storage.Fluent.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddCors(options =>
{
    // this defines a CORS policy called "default"
    options.AddPolicy("default", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(AutoMapping));

builder.Services.AddDbContext<AdvisorContext>(options =>
{
    options.UseInMemoryDatabase("InMemoryDb");
});

builder.Services.AddControllers()
    .AddOData(options =>
    {
        options.TimeZone = TimeZoneInfo.Utc;
        options.Count().Filter().Expand().Select().OrderBy().SetMaxTop(200)
            .AddRouteComponents("odata", Advisor.Config.EdmModelBuilder.GetEdmModel());
    })
    .AddFluentValidation(options =>
     {
         options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
     });

builder.Services.AddMediatR(typeof(GetAdvisorQueryable).GetTypeInfo().Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("default");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
