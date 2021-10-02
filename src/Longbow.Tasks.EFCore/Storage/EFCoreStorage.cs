using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Longbow.Tasks.EFCore.Storage
{
    public class EFCoreStorage : IStorage
    {
        private readonly SchedulerDBContext _dbContext;

        public Exception? Exception { get; set; }

        public EFCoreStorage(SchedulerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

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
                        TaskServicesManager.GetOrAdd(item.SchedulerName, task, item.ITrigger);
                    }
                }
            }
            catch (Exception ex)
            {
                Exception = ex;
                throw;
            }
        }

        protected virtual ITask? CreateTaskByScheduleName(string scheduleName) => null;

        public bool Save(string schedulerName, ITrigger trigger)
        {
            var model = new Scheduler()
            {
                SchedulerName = schedulerName,
                ITrigger = trigger
            };
            _dbContext.Schedulers.Add(model);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Remove(IEnumerable<string> schedulerNames)
        {
            throw new NotImplementedException();
        }
    }
}