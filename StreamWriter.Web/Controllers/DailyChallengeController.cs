using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace StreamWriter.Web.Controllers
{
    // Exposes the daily challenge text through the HTTP API.
    [Route("daily-challenge")]
    [ApiController]
    public class DailyChallengeController : ControllerBase
    {
        private readonly IDailyChallengeTextGenerator _dailyChallengeTextGenerator;

        // Receive the configured text generator through dependency injection.
        public DailyChallengeController(IDailyChallengeTextGenerator dailyChallengeTextGenerator)
        {
            _dailyChallengeTextGenerator = dailyChallengeTextGenerator;
        }

        // Return the next challenge text to the client.
        [HttpGet("text")]
        public async Task<string> GetDailyChallengeText()
        {
            string text = await _dailyChallengeTextGenerator.GetText();
            return text;
        }
    }

    // Defines the contract used by all challenge text generators.
    public interface IDailyChallengeTextGenerator
    {
        Task<string> GetText();
    }
}
