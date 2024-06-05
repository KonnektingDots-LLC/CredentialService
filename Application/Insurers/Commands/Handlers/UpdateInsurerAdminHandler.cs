using cred_system_back_end_app.Application.DTO.Requests;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Insurers.Commands.Handlers
{
    public class UpdateInsurerAdminHandler : IRequestHandler<UpdateInsurerAdminCommand>
    {
        private readonly IInsurerAdminRepository _insurerAdminRepository;

        public UpdateInsurerAdminHandler(IInsurerAdminRepository insurerAdminRepository)
        {
            _insurerAdminRepository = insurerAdminRepository;
        }

        public async Task Handle(UpdateInsurerAdminCommand request, CancellationToken cancellationToken)
        {
            await UpdateInsurerAdmin(request.UserRegisterRequest);
        }

        public async Task UpdateInsurerAdmin(UserRegisterRequestDto userRegisterRequetsDto)
        {

            var insurerAdmin = await _insurerAdminRepository.GetByEmailAsync(userRegisterRequetsDto.Email)
                ?? throw new InsurerAdminNotFoundException("Insurer admin was not previously whitelisted.");

            insurerAdmin.Name = userRegisterRequetsDto.Name;
            insurerAdmin.LastName = userRegisterRequetsDto.LastName;
            insurerAdmin.MiddleName = userRegisterRequetsDto.MiddleName;
            insurerAdmin.Surname = userRegisterRequetsDto.Surname;

            await _insurerAdminRepository.UpdateAsync(insurerAdmin);
        }
    }
}
