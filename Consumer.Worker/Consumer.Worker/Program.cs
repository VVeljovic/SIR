using Consumer.Worker;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog; 

var builder = Host.CreateApplicationBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Seq(builder.Configuration["Seq:Url"] ?? "http://localhost:5341/")
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
        var rabbitHost = builder.Configuration["RabbitMq:Host"] ?? "localhost";
        var rabbitUser = builder.Configuration["RabbitMq:Username"] ?? "user";
        var rabbitPass = builder.Configuration["RabbitMq:Password"] ?? "password";
        cfg.Host(rabbitHost, "/", h =>
        {
            h.Username(rabbitUser);
            h.Password(rabbitPass);
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
