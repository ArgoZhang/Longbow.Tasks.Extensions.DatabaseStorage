// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Longbow.Tasks.EFCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Longbow.Tasks.EFCore
{
    /// <summary>
    /// 
    /// </summary>
    public static class EFCoreStorageExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <param name="dboption"></param>
        /// <returns></returns>
        public static ITaskStorageBuilder AddDBStorage(this ITaskStorageBuilder builder,
            Action<EFCoreStorageOptions>? options = null, Action<DbContextOptionsBuilder>? dboption = null)
        {
            builder.Services.AddScoped<IStorage, EFCoreStorage>();
            builder.Services
                .AddSingleton<IOptionsChangeTokenSource<EFCoreStorageOptions>,
                    ConfigurationChangeTokenSource<EFCoreStorageOptions>>();
            builder.Services
                .AddSingleton<IConfigureOptions<EFCoreStorageOptions>,
                    EFCoreStorageOptionsConfigureOption<EFCoreStorageOptions>>();
            builder.Services.AddDbContext<SchedulerDBContext>(dboption);
            if (options != null)
            {
                builder.Services.Configure(options);
            }

            return builder;
        }
    }
}
