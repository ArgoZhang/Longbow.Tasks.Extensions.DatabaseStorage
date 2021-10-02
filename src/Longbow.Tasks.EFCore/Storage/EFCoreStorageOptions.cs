using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Longbow.Tasks.EFCore.Storage
{
    public class EFCoreStorageOptions
    {
    }

    internal class EFCoreStorageOptionsConfigureOption<TOptions> : ConfigureFromConfigurationOptions<TOptions>
        where TOptions : class
    {
        public EFCoreStorageOptionsConfigureOption(IConfiguration configuration)
            : base(configuration.GetSection("EFCoreStorageOptions"))
        {
        }
    }
}