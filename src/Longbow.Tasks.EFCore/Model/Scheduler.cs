namespace Longbow.Tasks.EFCore
{
    public class Scheduler
    {
        public string? SchedulerName { get; set; }

        public ITrigger ITrigger { get; set; }
    }
}