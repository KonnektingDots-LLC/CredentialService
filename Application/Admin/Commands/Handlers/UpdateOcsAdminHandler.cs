using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Admin.Commands.Handlers
{
    public class UpdateOcsAdminHandler : IRequestHandler<UpdateOcsAdminCommand>
    {
        private readonly IOcsAdminRepository _ocsAdminRepository;

        public UpdateOcsAdminHandler(IOcsAdminRepository ocsAdminRepository)
        {
            _ocsAdminRepository = ocsAdminRepository;
        }

        public async Task Handle(UpdateOcsAdminCommand request, CancellationToken cancellationToken)
        {
            var toUpdateAdminInfo = request.UserRegisterRequest;
            var ocsAdmin = await _ocsAdminRepository.GetByEmailAsync(toUpdateAdminInfo.Email)
                ?? throw new AggregateException($"Error updating OCS admin: no admin found by email.");

            ocsAdmin.Name = toUpdateAdminInfo.Name;
            ocsAdmin.MiddleName = toUpdateAdminInfo.MiddleName;
            ocsAdmin.LastName = toUpdateAdminInfo.LastName;
            ocsAdmin.Surname = toUpdateAdminInfo.Surname;
            ocsAdmin.Email = toUpdateAdminInfo.Email;

            await _ocsAdminRepository.UpdateAsync(ocsAdmin);
        }
    }
}
