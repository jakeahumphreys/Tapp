using System.IO;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Tapp.Notes.Data;

namespace Tapp;

public static class Startup
{
    public static IServiceProvider? Services { get; private set; }

    public static void Init()
    {
        CreateAppDirectory();
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(SetupServices)
            .Build();
        Services = host.Services;
    }

    private static void CreateAppDirectory()
    {
        var appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Tapp");

        if (!Directory.Exists(appDataFolder))
            Directory.CreateDirectory(appDataFolder);
    }

    private static void SetupServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddWpfBlazorWebView();
        services.AddMudServices();

        var sessionFactory = CreateSessionFactory();
        services.AddSingleton(sessionFactory);

        services.AddScoped(factory => sessionFactory.OpenSession());
        services.AddSingleton<INoteRepository, NoteRepository>();
        services.AddSingleton<INoteService, NoteService>();

#if DEBUG
        services.AddBlazorWebViewDeveloperTools();
#endif
    }
    
    public static ISessionFactory CreateSessionFactory()
    {
        var appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Tapp");

        var dbFile = Path.Combine(appDataFolder, "userdata.sqlite");
        var connectionString = $"Data Source={dbFile};Version=3;New=True;";
        return Fluently.Configure()
            .Database(SQLiteConfiguration.Standard.ConnectionString(connectionString))
            .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())
                .Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()))
            .ExposeConfiguration(config =>
            {
                new SchemaUpdate(config).Execute(false, true);
            })
            .BuildSessionFactory();
    }
}