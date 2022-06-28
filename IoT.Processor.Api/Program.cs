using System.Net;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using IoT.Processor.Api.Models;
using IoT.Processor.Api.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<InfluxDbService>();
builder.Services.AddScoped<MainIntegrationService>();
builder.Services.AddHttpClient();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.KnownProxies.Add(IPAddress.Parse("78.140.241.46"));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.MapPost("/api/data", async (
    HttpContext context, 
    [FromBody] Input input, 
    [FromServices] InfluxDbService service,
    [FromServices] MainIntegrationService integrationService) => 
{
    var token = context.Request.Headers["device-token"];

    await service.Write(writer => 
    {
        var data = PointData.Measurement("Input")
            .Field(nameof(Input.Humidity), input.Humidity)
            .Field(nameof(Input.Pressure), input.Pressure)
            .Field(nameof(Input.Temp), input.Temp)
            .Tag("Device", token)
            .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

        writer.WritePoint(data, "main", "main");
        return Task.CompletedTask;
    });

    integrationService.FireAndForget(token, input);
});

app.MapGet("/api/data/{deviceId}", async (string deviceId, [FromServices] InfluxDbService service) => 
{
    var flux = "from(bucket: \"main\")" + 
        "|> range(start: 0)" + 
        "|> filter(fn: (r) => r[\"_field\"] == \"Humidity\" or r[\"_field\"] == \"Pressure\" or r[\"_field\"] == \"Temp\")" + 
        $"|> filter(fn: (r) => r[\"Device\"] == \"{deviceId}\")" +
        "|> pivot(rowKey: [\"_time\"], columnKey: [\"_field\"], valueColumn: \"_value\")" +
        "|> yield(name: \"mean\")";

    return await service.QueryAsync<Input[]>(async reader => 
    {
        var tables = await reader.QueryAsync(flux, "main");
        return tables.SelectMany(table =>
            table.Records.Select(record =>
                new Input
                {
                    Time = record.GetTime().GetValueOrDefault().ToDateTimeUtc(),
                    Humidity = (double) record.Values[nameof(Input.Humidity)],
                    Pressure = (double) record.Values[nameof(Input.Pressure)],
                    Temp = (double) record.Values[nameof(Input.Temp)],
                })).ToArray();
    });
});

app.Run();
