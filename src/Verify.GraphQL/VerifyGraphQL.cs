namespace VerifyTests;

public static class VerifyGraphQL
{
    static List<JsonConverter> converters =
    [
        new ExecutionResultConverter(),
        new ExecutionErrorConverter()
    ];

    public static bool Initialized { get; private set; }

    public static void Initialize()
    {
        if (Initialized)
        {
            throw new("Already Initialized");
        }

        Initialized = true;

        InnerVerifier.ThrowIfVerifyHasBeenRun();
        VerifierSettings.AddExtraSettings(_ => _.Converters.AddRange(converters));
    }
}
