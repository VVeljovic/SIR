using Consumer.Worker;
using Consumer.Worker.Data;
using Consumer.Worker.Data.Interfaces;
using Consumer.Worker.Data.Repository;
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
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<Worker>();

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

        cfg.ReceiveEndpoint("sensor-data", x =>
        {
            x.ConcurrentMessageLimit = 1;
            x.ConfigureConsumer<Worker>(context);
        });
    });
});

builder.Services.AddScoped<IAggregationRepository, AggregationRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(cfg =>
{
    cfg.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
}
);


var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}

host.Run();
