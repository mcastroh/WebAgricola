using Logistic.BackEnd.Data.Data;
using Logistic.BackEnd.Data.Repositories.Implementations;
using Logistic.BackEnd.Data.Repositories.Interfaces;
using Logistic.BackEnd.Data.UnitsOfWork.Implementations;
using Logistic.BackEnd.Data.UnitsOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LogisticContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CnSqlServer")));

builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

builder.Services.AddSwaggerGen(doc =>
{
    doc.SwaggerDoc("v1", new OpenApiInfo { Title = "Logistica API", Version = "v1" });
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    doc.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint("../swagger/v1/swagger.json", "Logística API v1");
    });
}

//app.UseSwaggerUI(option =>
//{
//    option.SwaggerEndpoint("../swagger/v1/swagger.json", "Logística API v1");
//    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
//    doc.IncludeXmlComments(xmlPath);
//});

//services.AddSwaggerGen(doc =>
//{
//    doc.SwaggerDoc("v1", new OpenApiInfo { Title = "Logistica API", Version = "v1" });

//});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();