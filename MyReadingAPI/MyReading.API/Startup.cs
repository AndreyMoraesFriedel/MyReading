using Microsoft.EntityFrameworkCore;
using MyReading.API.Data;
using Microsoft.OpenApi.Models;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // Configura os serviços da aplicação
    public void ConfigureServices(IServiceCollection services)
    {
        // Adiciona suporte para controladores MVC
        services.AddControllers();

        // Configura o contexto do banco de dados para usar MySQL
        services.AddDbContext<MyReadingContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                             new MySqlServerVersion(new Version(8, 0, 21))));

        // Adiciona suporte para Swagger (documentação da API)
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyReading API", Version = "v1" });
        });
    }

    // Configura o pipeline de solicitação HTTP
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            // Usa a página de exceção do desenvolvedor
            app.UseDeveloperExceptionPage();

            // Configura o Swagger para gerar a documentação da API
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyReading API v1"));
        }

        // Redireciona HTTP para HTTPS
        app.UseHttpsRedirection();

        // Configura o roteamento de solicitações
        app.UseRouting();

        // Usa autorização
        app.UseAuthorization();

        // Mapeia os controladores para os endpoints
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
