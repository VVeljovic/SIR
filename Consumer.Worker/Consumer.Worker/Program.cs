using Consumer.Worker;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog; 

var builder = Host.CreateApplicationBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Seq("http://localhost:5341/")
    .WriteTo.Console()
    .MinimumLevel.Information()
    .MinimumLevel.Override("MassTransit", Serilog.Events.LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)

    .CreateLogger();

builder.Services.AddSerilog();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<Worker>();

    x.AddEntityFrameworkOutbox<ApplicationDbContext>(o =>
    {
        o.DuplicateDetectionWindow = TimeSpan.FromSeconds(3);
        o.UsePostgres();
        o.UseBusOutbox(cfg =>
        {
            cfg.MessageDeliveryLimit = 10; 
        });
    });
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("user");
            h.Password("password");
        });

        cfg.ReceiveEndpoint("nov", x =>
        {
            x.ConfigureConsumer<Worker>(context);
        });
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(cfg =>
{
    cfg.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
}
);
var host = builder.Build();

host.Run();
