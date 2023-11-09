public static class ConfigurationBuilder
{
    public static void SetupConfiguration(string[] args, IConfigurationBuilder config)
    {
        config.AddUserSecrets<Program>();
    }
}