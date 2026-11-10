using StreamWriter.Web.Controllers;

namespace StreamWriter.Web
{
    // Provides configured texts in sequence and starts over after the last text.
    public class RotatingDailyChallengeTextGenerator : IDailyChallengeTextGenerator
    {
        private readonly string[] _texts;
        private int _textIndex;

        // Store the texts and begin with the first entry.
        public RotatingDailyChallengeTextGenerator(string[] texts)
        {
            _texts = texts;
            _textIndex = 0;
        }

        // Return the current text, then move to the next entry in the rotation.
        public Task<string> GetText()
        {
            string result = _texts[_textIndex];
            _textIndex = (_textIndex + 1) % _texts.Length;
            return Task.FromResult(result);
        }
    }
}
