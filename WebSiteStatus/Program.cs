using WebSiteStatus;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(@"C:\Temp\workerservice\Logfile.txt")
    .CreateLogger();

var builder = Host.CreateApplicationBuilder(args);
builder.Services
    .AddWindowsService()
    .AddHostedService<Worker>()
    .AddSerilog();


try
{
    Log.Information("Starting the service");

   

    var host = builder.Build();
    host.Run();

}
catch (Exception ex)
{

    Log.Fatal(ex, "There was a problem starting the service");
    return;
}
finally
{
    Log.CloseAndFlush();
}
