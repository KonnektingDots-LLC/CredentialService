using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.CRUD.CredForm;
using cred_system_back_end_app.Application.CRUD.CredForm.DTO;
using cred_system_back_end_app.Application.CRUD.Provider.DTO;

namespace cred_system_back_end_app.Application.UseCase.CredForm
{
    public class CredFormUseCase
    {
        private readonly CredFormRepository _credFormRepo;
        public CredFormUseCase(CredFormRepository credFormRepo) 
        { 
            _credFormRepo = credFormRepo;
        }

        public async Task<CredFormResponseDto> GetCredFormByEmail(string email)
        {
            return await _credFormRepo.GetCredFormByEmail(email);
        }

        //public async Task<CredFormResponseDto> CreateCredFormVersion(string email, string createdBy)
        //{
        //    return await _credFormRepo.CreateCredFormVersion(email,createdBy);
        //}

        public async Task<CreateCredFormProviderResponseDto> CreateCredFormVersion(string email,CreateProviderDto createProviderDto)
        {
            return await _credFormRepo.CreateCredFormVersion(email, createProviderDto);
        }

        public async Task SetStatus(SetCredFormStatusDto setCredFormDto)
        {
            await _credFormRepo.SetStatusAndSave(setCredFormDto);
        }
    }
}
