﻿using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Domain.Entities
{
    public class InsurerEntity
    {
        public int Id { get; set; }
        [Required]
        public int InsurerTypeId { get; set; }
        [Required]
        public int ProviderInsurerId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string? InsurerFullName { get; set; }

        public string? InsurerEmail { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; }
    }
}