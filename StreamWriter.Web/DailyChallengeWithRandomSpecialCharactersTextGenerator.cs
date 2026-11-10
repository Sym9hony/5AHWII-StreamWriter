using System.Text;
using StreamWriter.Web.Controllers;

namespace StreamWriter.Web
{
    // Adds randomly selected QWERTZ keyboard special characters to generated text.
    public class DailyChallengeWithRandomSpecialCharactersTextGenerator : IDailyChallengeTextGenerator
    {
        private static readonly char[] SpecialCharacters =
        [
            '!', '"', '§', '$', '%', '&', '/', '(', ')', '=', '?', '*', '+',
            '#', '\'', '-', '.', ',', ';', ':', '_'
        ];

        private readonly IDailyChallengeTextGenerator _inner;
        private readonly double _specialCharacterRatio;

        // The ratio describes how many special characters to add per original text character.
        public DailyChallengeWithRandomSpecialCharactersTextGenerator(
            IDailyChallengeTextGenerator inner,
            double specialCharacterRatio)
        {
            ArgumentNullException.ThrowIfNull(inner);

            if (!double.IsFinite(specialCharacterRatio) || specialCharacterRatio < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(specialCharacterRatio),
                    "The special character ratio must be a finite, non-negative number.");
            }

            _inner = inner;
            _specialCharacterRatio = specialCharacterRatio;
        }

        public async Task<string> GetText()
        {
            string text = await _inner.GetText();
            int specialCharacterCount = GetSpecialCharacterCount(text.Length);
            StringBuilder result = new(text);

            for (int i = 0; i < specialCharacterCount; i++)
            {
                int insertIndex = Random.Shared.Next(result.Length + 1);
                char specialCharacter = SpecialCharacters[Random.Shared.Next(SpecialCharacters.Length)];
                result.Insert(insertIndex, specialCharacter);
            }

            return result.ToString();
        }

        private int GetSpecialCharacterCount(int textLength)
        {
            if (textLength == 0 || _specialCharacterRatio == 0)
            {
                return 0;
            }

            double specialCharacterCount = textLength * _specialCharacterRatio;

            return checked((int)Math.Ceiling(specialCharacterCount));
        }
    }
}
