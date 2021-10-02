using System;
using Microsoft.Extensions.DependencyInjection;
using Longbow.Tasks.EFCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Longbow.Tasks.EFCore
{
    /// <summary>
    /// 
    /// </summary>
    public static class EFCoreStorageExtension
    {
        public static ITaskStorageBuilder AddDBStorage(this ITaskStorageBuilder builder,
            Action<EFCoreStorageOptions>? options = null, Action<DbContextOptionsBuilder>? dboption = null)
        {
            if (builder == null) throw new ArgumentException($"{nameof(builder)}is not null here");
            builder.Services.AddScoped<IStorage, EFCoreStorage>();
            builder.Services
                .AddSingleton<IOptionsChangeTokenSource<EFCoreStorageOptions>,
                    ConfigurationChangeTokenSource<EFCoreStorageOptions>>();
            builder.Services
                .AddSingleton<IConfigureOptions<EFCoreStorageOptions>,
                    EFCoreStorageOptionsConfigureOption<EFCoreStorageOptions>>();
            builder.Services.AddDbContext<SchedulerDBContext>(dboption);
            if (options != null) builder.Services.Configure(options);
            return builder;
        }
    }
}