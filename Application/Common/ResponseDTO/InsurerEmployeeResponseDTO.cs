﻿namespace cred_system_back_end_app.Application.Common.ResponseDTO
{
    public class InsurerEmployeeResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }
    }
}
