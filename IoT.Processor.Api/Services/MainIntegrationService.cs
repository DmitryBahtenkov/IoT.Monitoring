using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using IoT.Processor.Api.Models;

namespace IoT.Processor.Api.Services
{
    public class MainIntegrationService
    {
        private const string JsonType = "application/json";

		private readonly ILogger<MainIntegrationService> _logger;
		private readonly IConfiguration _configuration;

        public MainIntegrationService(
            IConfiguration configuration,
			ILogger<MainIntegrationService> logger)
        {
			_logger = logger;
            _configuration = configuration;
        }

        private async Task SendData(string token, Input data)
        {
            var url = _configuration["Url"];
            var message = new HttpRequestMessage(HttpMethod.Post, url);
            var content = JsonSerializer.Serialize(data);
            message.Content = new StringContent(content, Encoding.Default, JsonType);
            message.Headers.Add("device-token", token);

            var eventId = Guid.NewGuid().ToString();

            _logger.LogInformation(eventId,
                "Send data to main service. Url: {url}, device: {device}, data: {data}",
                url, token, content);

            using var httpClient = new HttpClient();

            var result = await httpClient.SendAsync(message);
            
            var raw = await result.Content.ReadAsStringAsync();
            _logger.LogInformation(eventId, "Received response from main service. Code: {code}, raw: {raw}", result.StatusCode, raw);
        }

        public void FireAndForget(string token, Input data)
        {
            Task.Run(async () =>
            {
                try
                {
                    await SendData(token, data);
                }
                catch (System.Exception e)
                {
                    _logger.LogError(e, "Error with sending data");
                }
            });
        }
    }
}