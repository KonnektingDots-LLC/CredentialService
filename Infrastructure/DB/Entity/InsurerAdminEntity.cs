﻿namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class InsurerAdminEntity : RecordHistory
    {
        public int Id { get; set; }
        public string? InsurerCompanyId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Surname { get; set; }
        public string Email { get; set; }

        #region relationships

        public InsurerCompanyEntity InsurerCompany { get; set; }

        #endregion
    }
}
