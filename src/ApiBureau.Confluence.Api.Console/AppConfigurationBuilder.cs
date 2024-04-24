using System.Reflection;

public static class AppConfigurationBuilder
{
    public static void SetupConfiguration(string[] args, IConfigurationBuilder config)
    {
        var entryAssembly = Assembly.GetEntryAssembly();

        if (entryAssembly != null)
        {
            config.AddUserSecrets(entryAssembly);
        }
    }
}