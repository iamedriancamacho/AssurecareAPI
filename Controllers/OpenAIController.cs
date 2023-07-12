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
        //private readonly string API_KEY = "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";
        //private readonly string API_KEY2 = "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";
        private readonly string API_KEY3 = "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";
        [HttpGet]
        public async Task<ActionResult<OpenAI>> getPrompt(string Query)
        {
            OpenAIAPI openAIAPI = new OpenAIAPI(API_KEY3);
            string result = " ";
            CompletionRequest completionRequest = new()
            {
                Prompt = Query,
                Model = OpenAI_API.Models.Model.DavinciText,
                MaxTokens = 100
            };

            var completions = await openAIAPI.Completions.CreateCompletionAsync(completionRequest);

            if (completions.Completions.Count==0)//completions.Result.completions
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
    

