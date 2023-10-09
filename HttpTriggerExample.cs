using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;


namespace Company.Function
{
    public static class HttpTriggerExample
{
    [FunctionName("HttpTriggerExample")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        string endpoint = "https://shell-dev-cepdm-prime.c3iot.ai/api/1/cepdm/prime/User?action=remove";
        string token = "eJwNxsEBwCAIA8CVjICQcUBw/xHae50sEc3o9qns3nfZpufW13UyvZchA4dFlUMzmgZMwiBa4W8pMNf0WCMuBdUB35lvQP45GFakC8Wt4u3YJC7RM356bn8UYiM7";
        Dictionary<string, string> values = new Dictionary<string, string>
        {
            { "userid", "5" }
        };
        log.LogInformation("C# HTTP trigger function processed a request.");
        log.LogInformation($"user_id removed:{values}");
        var client= new HttpClient();
     
      
        HttpClient _client=new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endpoint);
        request.Content = new FormUrlEncodedContent(values);
        var response = await _client.SendAsync(request);
        //var json= await response.Content.ReadAsStringAsync();
        //dynamic data = JsonConvert.DeserializeObject(json);
        bool status=response.IsSuccessStatusCode;

        string responseMessage = status
            ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            : $"Hello, {status}. This HTTP triggered function executed successfully.";


            //dispose HttpClient
        client.Dispose();

        return new OkObjectResult(response);

    }
}
}
