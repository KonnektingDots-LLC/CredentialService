using Azure.Communication.Email;
using cred_system_back_end_app.Application.Common.RequestDto;
using cred_system_back_end_app.Application.CRUD.OCSAdmin;

namespace cred_system_back_end_app.Application.UseCase.OCSAdmin
{
    public class OCSAdminUseCase
    {
        private readonly OCSAdminRepository _ocsAdminRepo;

        public OCSAdminUseCase(OCSAdminRepository ocsAdminRepo) 
        {
            _ocsAdminRepo = ocsAdminRepo;
        }
        
        public async Task<bool> ValidateOCSAdmin(string email) 
        { 
            var ocsAdmin = await _ocsAdminRepo.GetByEmailAsync(email);

            return ocsAdmin.Any();
        }

        public async Task UpdateOCSAdmin(Names names, string email)
        {
             await _ocsAdminRepo.UpdateOCSAdminAsync(names, email);
        }
    }
}
