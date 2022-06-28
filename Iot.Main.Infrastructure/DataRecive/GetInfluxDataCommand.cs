using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfluxDB.Client.Core.Exceptions;
using Iot.Main.Domain.Models.DeviceModel;
using Iot.Main.Domain.Models.InputModel;
using Iot.Main.Domain.Shared.Units;
using Iot.Main.Infrastructure.Builders;
using Iot.Main.Infrastructure.Commands;

namespace Iot.Main.Infrastructure.DataRecive;
public class GetInfluxDataCommand
{
    private readonly InfluxDbCommand _influxDbCommand;
    private readonly IRepository<Device> _repository;
    public GetInfluxDataCommand(
        InfluxDbCommand influxDbCommand,
        IRepository<Device> repository)
    {
        _repository = repository;
        _influxDbCommand = influxDbCommand;
    }

    public async Task<InputData[]> Get(int deviceId)
    {
        var device = await _repository.BuIdAsync(deviceId);

        if (device is null)
        {
            throw new NotFoundException("Девайс не найден");
        }

        var token = device.Token;

        var flux = new FluxQueryBuilder()
            .FromBucket("main")
            .WithNullRange()
            .WithFields(nameof(InputData.Humidity), nameof(InputData.Pressure), nameof(InputData.Temp))
            .WithEqual("Device", token)
            .WithPivot("_time", "_field", "_value")
            .BuildYield("mean");

        // var flux = "from(bucket: \"main\")" +
        //         "|> range(start: 0)" +
        //         "|> filter(fn: (r) => r[\"_field\"] == \"Humidity\" or r[\"_field\"] == \"Pressure\" or r[\"_field\"] == \"Temp\")" +
        //         $"|> filter(fn: (r) => r[\"Device\"] == \"{token}\")" +
        //         "|> pivot(rowKey: [\"_time\"], columnKey: [\"_field\"], valueColumn: \"_value\")" +
        //         "|> yield(name: \"mean\")";

        return await _influxDbCommand.QueryAsync<InputData[]>(async reader =>
        {
            var tables = await reader.QueryAsync(flux, "main");
            return tables.SelectMany(table =>
                table.Records.Select(record =>
                    new InputData
                    {
                        Time = record.GetTime().GetValueOrDefault().ToDateTimeUtc(),
                        Humidity = (double)record.Values[nameof(InputData.Humidity)],
                        Pressure = (double)record.Values[nameof(InputData.Pressure)],
                        Temp = (double)record.Values[nameof(InputData.Temp)],
                    })).ToArray();
        });
    }
}

