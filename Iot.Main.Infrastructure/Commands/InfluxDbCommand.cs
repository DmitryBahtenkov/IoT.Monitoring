using InfluxDB.Client;
using Microsoft.Extensions.Configuration;

namespace Iot.Main.Infrastructure.Commands
{
    public class InfluxDbCommand
    {
		private readonly string _host;
		private readonly string _token;

		public InfluxDbCommand(IConfiguration configuration)
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