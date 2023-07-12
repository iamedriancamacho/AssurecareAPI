using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;

namespace Voxscribe.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeechToTextController : ControllerBase
    {
        private readonly SpeechRecognizer recognizer;

        public SpeechToTextController(SpeechRecognizer recognizer)
        {
            this.recognizer = recognizer;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var result = await recognizer.RecognizeOnceAsync();
            return Ok(result.Text);
        }
    }
}
