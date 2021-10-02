// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Longbow.Tasks.EFCore.Storage
{
    /// <summary>
    /// 
    /// </summary>
    public class EFCoreStorage : IStorage
    {
        private readonly SchedulerDBContext _dbContext;

        /// <summary>
        /// 
        /// </summary>
        public Exception? Exception { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public EFCoreStorage(SchedulerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task LoadAsync()
        {
            try
            {
                var schedulers = await _dbContext.Schedulers.AsNoTracking().ToListAsync();
                if (schedulers != null)
                {
                    foreach (var item in schedulers)
                    {
                        var task = CreateTaskByScheduleName(item.SchedulerName);
                        if (task != null)
                        {
                            TaskServicesManager.GetOrAdd(item.SchedulerName, task, item.ITrigger);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exception = ex;
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleName"></param>
        /// <returns></returns>
        protected virtual ITask? CreateTaskByScheduleName(string scheduleName) => null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedulerName"></param>
        /// <param name="trigger"></param>
        /// <returns></returns>
        public bool Save(string schedulerName, ITrigger trigger)
        {
            var model = new Scheduler()
            {
                SchedulerName = schedulerName,
                ITrigger = trigger
            };
            _dbContext.Schedulers?.Add(model);
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedulerNames"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Remove(IEnumerable<string> schedulerNames)
        {
            throw new NotImplementedException();
        }
    }
}
