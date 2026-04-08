using System.Net.Http;
using System.Text;
using System.Text.Json;

public class TranslatorService
{
    private readonly HttpClient _httpClient;

    public TranslatorService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> Traducir(string texto)
    {
        var requestBody = new
        {
            q = texto,
            source = "auto",
            target = "en",
            format = "text"
        };

        var content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync(
            "https://translate.libregalaxy.org/translate",
            content
        );

        if (!response.IsSuccessStatusCode)
            throw new Exception("Error al traducir");

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonDocument.Parse(json);

        return result.RootElement
                     .GetProperty("translatedText")
                     .GetString()!;
    }
}