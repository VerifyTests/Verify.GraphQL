namespace VerifyTests;

public static class VerifyGraphQL
{
    static List<JsonConverter> converters =
    [
        new ResultConverter(),
        new ErrorConverter(),
        new RequestConverter(),
        new MessageConverter(),
        new LocationConverter(),
        new ExecutionNodeConverter()
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
