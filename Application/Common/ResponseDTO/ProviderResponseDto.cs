using AutoMapper;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Application.Common.ResponseTO
{
    public class ProviderResponseDto
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }


    }
}
