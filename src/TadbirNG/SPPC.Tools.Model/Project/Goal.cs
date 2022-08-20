using System;

namespace SPPC.Tools.Model.Project
{
    public class Goal
    {
        public int GoalId { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
