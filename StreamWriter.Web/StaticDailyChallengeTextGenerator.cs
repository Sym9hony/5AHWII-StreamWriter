using StreamWriter.Web.Controllers;

namespace StreamWriter.Web
{
    // Provides the same challenge text for every request.
    public class StaticDailyChallengeTextGenerator : IDailyChallengeTextGenerator
    {
        private readonly string _text;

        public StaticDailyChallengeTextGenerator(string text)
        {
            _text = text;
        }

        public Task<string> GetText()
        {
            return Task.FromResult(_text);
        }
    }
}
