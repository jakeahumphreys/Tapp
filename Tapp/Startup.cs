using System.IO;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Tapp.Common.Helpers;
using Tapp.Notes.Data;
using Tapp.Settings;

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
        if (!Directory.Exists(FilePathHelper.AppDataFolder))
            Directory.CreateDirectory(FilePathHelper.AppDataFolder);
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

        services.AddSingleton<IExportService, ExportService>();

#if DEBUG
        services.AddBlazorWebViewDeveloperTools();
#endif
    }

    private static ISessionFactory CreateSessionFactory()
    {
        var connectionString = $"Data Source={FilePathHelper.DbFile};Version=3;New=True;";
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