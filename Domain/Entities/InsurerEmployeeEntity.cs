﻿using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class InsurerEmployeeEntity : EntityCommon
    {
        public int Id { get; set; }
        public string InsurerCompanyId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }

        public string? SurName { get; set; }
        public string Email { get; set; }

        #region relationships

        public InsurerCompanyEntity InsurerCompany { get; set; }

        #endregion
    }
}