using AssurecareAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenAI_API;
using OpenAI_API.Completions;

namespace AssurecareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAIController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<OpenAI>> GetPrompt(string Query)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string? temp = configuration.GetSection("ApiKeys").GetSection("ApiKey_OpenAI").Value;

            if (string.IsNullOrEmpty(temp))
            {
                return BadRequest();
            }
            else
            {
                OpenAIAPI openAIAPI = new(apiKeys: temp);
                CompletionRequest completionRequest = new()
                {
                    Prompt = Query,
                    Model = OpenAI_API.Models.Model.DavinciText,
                    MaxTokens = 100
                };

                var completions = await openAIAPI.Completions.CreateCompletionAsync(completionRequest);
                string? result = null;
                if (!completions.Completions.Any())//completions.Result.completions
                {
                    return BadRequest();
                }
                else
                {
                    foreach (var completion in completions.Completions)//completions.Result.completions
                    {
                        result += completion.Text;
                    }
                    Console.WriteLine(result);
                    return Ok(result);
                }
            }
        }
    }
}