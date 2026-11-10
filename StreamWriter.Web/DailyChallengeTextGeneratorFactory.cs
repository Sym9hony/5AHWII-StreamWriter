using Microsoft.Extensions.Configuration;
using StreamWriter.Web.Controllers;

namespace StreamWriter.Web
{
    public static class DailyChallengeTextGeneratorFactory
    {
        public static IDailyChallengeTextGenerator Create(IConfiguration configuration)
        {
            string type = configuration["Type"] ?? "Rotating";

            IDailyChallengeTextGenerator generator = type.ToLowerInvariant() switch
            {
                "static" => new StaticDailyChallengeTextGenerator(
                    configuration["Text"] ?? throw new InvalidOperationException(
                        "DailyChallenge.Text is required for a Static generator.")),
                "rotating" => new RotatingDailyChallengeTextGenerator(
                    configuration.GetSection("Texts").Get<string[]>() ?? []),
                "delay" => new DelayDailyChallengeTextGenerator(
                    TimeSpan.FromSeconds(configuration.GetValue<double>("DelaySeconds")),
                    Create(RequiredInner(configuration))),
                "randomspecialcharacters" => new DailyChallengeWithRandomSpecialCharactersTextGenerator(
                    Create(RequiredInner(configuration)),
                    configuration.GetValue<double>("Ratio")),
                _ => throw new InvalidOperationException(
                    $"Unknown daily challenge generator type '{type}'.")
            };

            return generator;
        }

        private static IConfiguration RequiredInner(IConfiguration configuration)
        {
            IConfigurationSection inner = configuration.GetSection("Inner");
            if (!inner.Exists())
            {
                throw new InvalidOperationException("The Inner generator configuration is required.");
            }

            return inner;
        }
    }
}
