using Logistic.BackEnd.API.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContexts(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwagger(xmlFile);
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    //app.UseSwagger();
    //app.UseSwaggerUI(option => option.SwaggerEndpoint("../swagger/v1/swagger.json", "Logística API v1"));
}

app.UseSwagger();

app.UseSwaggerUI(option =>
{
    option.SwaggerEndpoint("../swagger/v1/swagger.json", "Agrícola.Logística API v1");
});

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();