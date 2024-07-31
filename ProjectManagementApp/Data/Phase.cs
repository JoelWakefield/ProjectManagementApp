﻿using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.Data
{
    public class Phase
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string ProjectId { get; set; }
        [Required]
        public string StageId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public Priority Priority { get; set; }
        [Range(0,100)]
        public byte Progress { get; set; }
    }

    public enum Priority
    {
        None,
        Low,
        Medium,
        High,
        Critical
    }
}