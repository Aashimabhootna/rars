using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;

public class OAuthService
{
    private readonly HttpClient _client;

    /// <summary>
    /// Constructor. Instantiate a new <see cref="HttpClient"/> for submitting requests to the identity provider.
    /// </summary>
    public OAuthService()
    {
        _client = new HttpClient();
    }

    /// <summary>
    /// Send a POST request to C3 endpoint.
    /// </summary>
    /// <param name="endpoint">The C3 endpoint URL.</param>
    /// <param name="token">The bearer token.</param>
    /// <param name="values">Params to pass to the request.</param>
    /// <returns>A boolean indicating whether the request was successful or not.</returns>
    public async Task<bool> SendRequestToC3Endpoint(string endpoint, string token, Dictionary<string, string> values)
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endpoint);
        request.Content = new FormUrlEncodedContent(values);

        var response = await _client.SendAsync(request);

        return response.IsSuccessStatusCode;
    }
}

public class YourClass
{
    private OAuthService oAuthService;

    public YourClass()
    {
        oAuthService = new OAuthService();
    }

    public async Task CallC3EndpointAsync()
    {
        string endpoint = "https://shell-dev-cepdm-prime.c3iot.ai/api/1/ssw/prime/User?action=remove";
        string token = "eJwNxsEBwCAIA8CVjICQcUBw/xHae50sEc3o9qns3nfZpufW13UyvZchA4dFlUMzmgZMwiBa4W8pMNf0WCMuBdUB35lvQP45GFakC8Wt4u3YJC7RM356bn8UYiM7";
        Dictionary<string,string> values = new Dictionary<string,string>
        {
            { "userid", "value1" }
        
        };
        bool isSuccess = await oAuthService.SendRequestToC3Endpoint(endpoint, token, values);

        // Handle isSuccess according to your application logic.
    }
}
