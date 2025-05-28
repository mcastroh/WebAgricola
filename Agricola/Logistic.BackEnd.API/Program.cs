using Logistic.BackEnd.Data.Data;
using Logistic.BackEnd.Data.Repositories.Implementations;
using Logistic.BackEnd.Data.Repositories.Interfaces;
using Logistic.BackEnd.Data.UnitsOfWork.Implementations;
using Logistic.BackEnd.Data.UnitsOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LogisticContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CnSqlServer")));

builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint("../swagger/v1/swagger.json", "Logística API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();