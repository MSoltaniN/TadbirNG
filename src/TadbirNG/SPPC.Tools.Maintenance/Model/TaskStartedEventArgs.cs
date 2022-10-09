using System;

namespace SPPC.Tools.Maintenance
{
    public class TaskStartedEventArgs : EventArgs
    {
        public TaskStartedEventArgs(string name, TaskType task, TargetType target)
        {
            Name = name;
            TaskType = task;
            TargetType = target;
        }

        public string Name { get; }

        public TaskType TaskType { get; }

        public TargetType TargetType { get; }
    }
}
