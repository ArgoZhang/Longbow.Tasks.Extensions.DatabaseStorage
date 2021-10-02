// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Longbow.Tasks.EFCore
{
    /// <summary>
    /// 
    /// </summary>
    public class TypeEntityConfigure : IEntityTypeConfiguration<Scheduler>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Scheduler> builder)
        {
        }
    }
}
