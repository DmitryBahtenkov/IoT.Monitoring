using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfluxDB.Client;

namespace IoT.Processor.Api.Services
{
    public class InfluxDbService
    {

        private readonly string _host;
        private readonly string _token;

        public InfluxDbService(IConfiguration configuration)
        {
            _host = configuration.GetSection("Influx")["Host"];
            _token = configuration.GetSection("Influx")["Token"];
        }
        public async Task Write(Func<WriteApi, Task> action)
        {
            using var client = InfluxDBClientFactory.Create(_host, _token);
            using var write = client.GetWriteApi();
            await action(write);
        }

        public async Task<T> QueryAsync<T>(Func<QueryApi, Task<T>> action)
        {
            using var client = InfluxDBClientFactory.Create(_host, _token);
            var query = client.GetQueryApi();
            return await action(query);
        }
    }
}