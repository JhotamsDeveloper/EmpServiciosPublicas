using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using EmpServiciosPublicas.Aplication.Models;
using EmpServiciosPublicas.Infrastructure.Email;
using EmpServiciosPublicas.Infrastructure.Persistence;
using EmpServiciosPublicas.Infrastructure.Repositories;
using EmpServiciosPublicas.Infrastructure.UploadFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmpServiciosPublicas.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmpServiciosPublicosDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPQRSDRepository, PQRSDRepository>();

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUploadFilesService, UploadFilesService>();

            return services;
        }
    }
}
