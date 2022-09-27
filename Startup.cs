using System.Collections;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Mbis.Cognito.Abstract.Auth;
using Mbis.Cognito.Abstract.User;
using Mbis.Cognito.Configuration.Settings;
using Mbis.Cognito.Infastracture.Builders;
using Mbis.Cognito.Service.Auth;
using Mbis.Cognito.Service.User;

namespace Mbis.Cognito;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        services.Configure<AWS>(Configuration.GetSection("AWS"));
        services.AddServices();
        services.AddAutoMapper(typeof(Startup));
        services.AddCognitoIdentity();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();
        app.UseAuthentication();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/",
                async context =>
                {
                    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
                });
        });
    }
}