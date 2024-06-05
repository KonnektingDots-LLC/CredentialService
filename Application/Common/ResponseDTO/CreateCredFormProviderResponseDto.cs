using cred_system_back_end_app.Application.CRUD.CredForm.DTO;
using cred_system_back_end_app.Application.CRUD.Provider.DTO;

namespace cred_system_back_end_app.Application.Common.ResponseDTO
{
    public class CreateCredFormProviderResponseDto
    {
        public CredFormResponseDto CredFormResponse { get; set; }
        public CreatedProviderResponseDto CreatedProviderResponse { get; set; }
    }
}
