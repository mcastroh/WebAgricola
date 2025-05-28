using Logistic.BackEnd.Data.Data;
using Logistic.BackEnd.Data.Repositories.Implementations;
using Logistic.BackEnd.Data.Repositories.Interfaces;
using Logistic.BackEnd.Data.UnitsOfWork.Implementations;
using Logistic.BackEnd.Data.UnitsOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Logistic.BackEnd.API.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LogisticContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("CnSqlServer"))
       );
        return services;
    }

    //public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.Configure<PaginationOptions>(options => configuration.GetSection("Pagination").Bind(options));
    //    services.Configure<PasswordOptions>(options => configuration.GetSection("PasswordOptions").Bind(options));
    //    return services;
    //}

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        //services.AddScoped(typeof(IRepository<>), typeof(LogisticRepository<>));
        //services.AddScoped<IUnitOfWork, UnitOfWork>();

        //services.AddFluentValidationAutoValidation();
        //services.AddScoped<IValidator<UsuarioDto>, UsuarioValidator>();

        ////builder.Services.AddScoped<IValidator<PostDto>, PostValidator>();

        //services.AddSingleton<IPasswordService, PasswordService>();
        //services.AddTransient<ISecurityService, SecurityService>();
        //services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

        //services.AddSingleton<IUriService>(opcion =>
        //{
        //    var accessor = opcion.GetRequiredService<IHttpContextAccessor>();
        //    var request = accessor.HttpContext.Request;
        //    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
        //    return new UriService(uri);
        //});

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services, string xmlFileName)
    {
        services.AddSwaggerGen(doc =>
        {
            doc.SwaggerDoc("v1", new OpenApiInfo { Title = "Logistica API", Version = "v1" });
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
            doc.IncludeXmlComments(xmlPath);
        });

        return services;
    }

    //public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.AddAuthentication(options =>
    //    {
    //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //    }).AddJwtBearer(options =>
    //    {
    //        options.TokenValidationParameters = new TokenValidationParameters
    //        {
    //            ValidateIssuer = true,
    //            ValidateAudience = true,
    //            ValidateLifetime = true,
    //            ValidateIssuerSigningKey = true,
    //            ValidIssuer = configuration["Authentication:Issuer"],
    //            ValidAudience = configuration["Authentication:Audience"],
    //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:SecretKey"]))
    //        };
    //    });

    //    return services;
    //}
}