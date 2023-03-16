using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace Core;

public static class HttpClientExtensions
{
    public async static Task<(HttpResponseMessage Response, dynamic Content)> Get(this HttpClient http, object requestUri) =>
        await Result<dynamic>(await http.GetAsync($"{requestUri}"));

    public async static Task<(HttpResponseMessage Response, dynamic Content)> Post(this HttpClient http, object requestUri, object request = null) =>
        await Result<dynamic>(await http.PostAsJsonAsync($"{requestUri}", request));

    public async static Task<(HttpResponseMessage Response, dynamic Content)> Post(this HttpClient http, object requestUri, HttpContent content) =>
        await Result<dynamic>(await http.PostAsync($"{requestUri}", content));

    public async static Task<(HttpResponseMessage Response, dynamic Content)> Put(this HttpClient http, object requestUri, object request = null) =>
        await Result<dynamic>(await http.PutAsJsonAsync($"{requestUri}", request));

    public async static Task<(HttpResponseMessage Response, dynamic Content)> Delete(this HttpClient http, object requestUri) =>
        await Result<dynamic>(await http.DeleteAsync($"{requestUri}"));

    private static async Task<(HttpResponseMessage, TResponse)> Result<TResponse>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.Redirect)
        {
            return (response, await GetResult<TResponse>(response));
        }

        throw await RequestFailure.From(response);
    }

    private static async Task<TResult> GetResult<TResult>(HttpResponseMessage response)
    {
        if (response.Content.Headers.ContentType is { MediaType: "application/json" })
            return Deserialize<TResult>(await response.Content.ReadAsStringAsync());

        return (dynamic)await response.Content.ReadAsStringAsync();
    }

    private static TResult Deserialize<TResult>(string content)
    {
        try
        {
            return JsonConvert.DeserializeObject<TResult>(content);
        }
        catch (Exception)
        {
            Console.WriteLine(content);
            throw;
        }
    }
}