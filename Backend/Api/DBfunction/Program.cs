using A2_project_work.ApplicationCore.Abstracts.Repositories;
using A2_project_work.ApplicationCore.Interfaces.Repositories;
using A2_project_work.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration(con =>
    {
        con.AddUserSecrets<Program>(optional: true, reloadOnChange: false);
    })
    .ConfigureServices(s =>
    {
        var config = s.BuildServiceProvider().GetService<IConfiguration>();
        s.AddScoped<ILogRepository, LogRepository>();
        s.AddScoped<IPicRepository, PicRepository>();
    })
    .Build();

host.Run();
