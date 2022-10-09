using System;

namespace SPPC.Tools.Maintenance
{
    public class TaskFinishedEventArgs : EventArgs
    {
        public TaskFinishedEventArgs(
            string name, TaskType task, TargetType target, bool succeeded, Exception exception)
        {
            Name = name;
            TaskType = task;
            TargetType = target;
            Succeeded = succeeded;
            Exception = exception;
        }

        public string Name { get; }

        public TaskType TaskType { get; }

        public TargetType TargetType { get; }

        public bool Succeeded { get; }

        public Exception Exception { get; }
    }
}
