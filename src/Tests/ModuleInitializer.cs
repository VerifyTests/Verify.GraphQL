public static class ModuleInitializer
{
    #region enable

    [ModuleInitializer]
    public static void Initialize() =>
        VerifyGraphQL.Initialize();

    #endregion

    [ModuleInitializer]
    public static void InitializeOther() =>
        VerifierSettings.InitializePlugins();
}
