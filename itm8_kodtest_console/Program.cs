// See https://aka.ms/new-console-template for more information
using itm8_kodtest_console.Data;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using itm8_kodtest_console.Utilities;


try
{
    var pollyAsync = PollyUtils.PollyAsyncRetry();
    await pollyAsync.ExecuteAsync (async () =>
        {
            using HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(@"http://localhost:5187/WeatherInformation");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to call API");
                return;
            }

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("API call success!\n");
            List<CityInformation>? cities = [];

            if (content is not null)
            {
                cities = JsonConvert.DeserializeObject<List<CityInformation>>(content);

                cities = cities.OrderBy(x => x.Country).ThenByDescending(y => y.Temperature).ToList();
            }
            else
            {
                Console.WriteLine("API returned null");
            }

            foreach (var city in cities)
            {
                Console.WriteLine($"{city.City}, temperature at {city.ValidTime.Value.ToString("HH:mm")} {(city.ValidTime.Value.Hour > DateTime.Now.Hour ? "is" : "was")} {city.Temperature}°C");
            }
        });
}
catch (Exception ex)
{
    Console.WriteLine($"Something went belly up!\n\n{ex.Message}");
}