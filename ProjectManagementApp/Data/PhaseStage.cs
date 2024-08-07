﻿namespace ProjectManagementApp.Data
{
    public class PhaseStage
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string PhaseId { get; set; }
        public string StageId { get; set; }
    }
}