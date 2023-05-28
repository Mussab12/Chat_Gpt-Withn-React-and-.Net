using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;

namespace ChatGPTAPI.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string OpenAIAPIEndpoint = "https://api.openai.com/v1/completions";
        private const string ModelName = "text-davinci-002"; // Change this to the name of the model you want to use

        [HttpGet]
        [Route("api/chat")]
        public async Task<ActionResult<string>> GetChatResponse(string message)
        {
            var requestData = new
            {
                prompt = message,
                max_tokens = 150,
                temperature = 0.5f,
                n = 1,
                stop = "\n"
            };

            var requestDataJson = JsonSerializer.Serialize(requestData);

            var request = new HttpRequestMessage(HttpMethod.Post, OpenAIAPIEndpoint);
            request.Content = new StringContent(requestDataJson, Encoding.UTF8, "application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "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");
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<dynamic>(responseBody);

                var chatResponse = responseObject.choices[0].text.ToString();
                return Ok(chatResponse);
            }
            else
            {
                return BadRequest("Failed to retrieve chat response.");
            }
        }
    }

}


