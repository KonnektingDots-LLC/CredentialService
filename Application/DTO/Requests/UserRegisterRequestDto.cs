﻿namespace cred_system_back_end_app.Application.DTO.Requests
{
    public class UserRegisterRequestDto
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
    }
}
