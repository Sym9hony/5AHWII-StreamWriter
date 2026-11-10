using StreamWriter.Web.Controllers;

namespace StreamWriter.Web
{
    // Adds an artificial delay before obtaining text from another generator.
    public class DelayDailyChallengeTextGenerator : IDailyChallengeTextGenerator
    {
        private readonly TimeSpan _delay;
        private readonly IDailyChallengeTextGenerator _inner;

        // Configure the delay and the generator that supplies the actual text.
        public DelayDailyChallengeTextGenerator(
            TimeSpan delay,
            IDailyChallengeTextGenerator inner)
        {
            _delay = delay;
            _inner = inner;
        }

        // Wait before delegating the request to the wrapped generator.
        public async Task<string> GetText()
        {
            await Task.Delay(_delay);
            return await _inner.GetText();
        }
    }
}
