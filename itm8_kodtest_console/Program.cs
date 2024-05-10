// See https://aka.ms/new-console-template for more information
using itm8_kodtest_console.Data;
using Newtonsoft.Json;
using System.Net.Http.Headers;

try
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

    if (content is null)
    {
        Console.WriteLine("API returned null");
    }

    List<CityInformation>? cities = JsonConvert.DeserializeObject<List<CityInformation>>(content);

    cities = cities.OrderBy(x => x.Country).ThenByDescending(y => y.Temperature).ToList();

    foreach (var city in cities)
    {
        Console.WriteLine($"{city.City}, temperature is now {city.Temperature}°C");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Something went belly up!\n\n{ex.Message}");
}